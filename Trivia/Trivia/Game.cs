using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private const int MaximumNumberOfPlayers = 6;
        private const int GoldenCoinsForVictory = 6;
        private const string PopCategoryName = "Pop";
        private const string ScienceCategoryName = "Science";
        private const string SportsCategoryName = "Sports";
        private const string RockCategoryName = "Rock";

        private readonly List<Player> players = new List<Player>();

        private readonly int[] places = new int[MaximumNumberOfPlayers];
        private readonly int[] purses = new int[MaximumNumberOfPlayers];
        private readonly bool[] inPenaltyBox = new bool[MaximumNumberOfPlayers];

        private readonly Dictionary<string, LinkedList<string>> questionsByCategory = new Dictionary<string, LinkedList<string>>
        { { PopCategoryName, new LinkedList<string>() },
            { ScienceCategoryName, new LinkedList<string>() },
            { SportsCategoryName, new LinkedList<string>() },
            { RockCategoryName, new LinkedList<string>() },
        };

        private int currentPlayer;
        private bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var questionNumber = 0; questionNumber < 50; questionNumber++)
            {
                questionsByCategory[PopCategoryName].AddLast(CreateQuestion(questionNumber, PopCategoryName));
                questionsByCategory[ScienceCategoryName].AddLast(CreateQuestion(questionNumber, ScienceCategoryName));
                questionsByCategory[SportsCategoryName].AddLast(CreateQuestion(questionNumber, SportsCategoryName));
                questionsByCategory[RockCategoryName].AddLast(CreateQuestion(questionNumber, RockCategoryName));
            }
        }

        private string CreateQuestion(int questionNumber, string questionType)
        {
            return questionType + " Question " + questionNumber;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            players.Add(player);
            purses[HowManyPlayers()] = 0;
            inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(GetCurrentPlayerName() + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            var isEven = roll % 2 == 0;

            if (inPenaltyBox[currentPlayer])
            {
                if (isEven)
                {
                    Console.WriteLine(GetCurrentPlayerName() + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
                else
                {
                    Console.WriteLine(GetCurrentPlayerName() + " is getting out of the penalty box");
                    isGettingOutOfPenaltyBox = true;

                    MovePlayer(roll);
                    AskQuestion();
                }
            }
            else
            {
                MovePlayer(roll);
                AskQuestion();
            }
        }

        private Player GetCurrentPlayer()
        {
            return players[currentPlayer];
        }

        private string GetCurrentPlayerName()
        {
            return GetCurrentPlayer().Name;
        }

        private void MovePlayer(int roll)
        {
            var player = GetCurrentPlayer();
            player.MovePlayer(roll);

            Console.WriteLine(GetCurrentPlayerName() +
                "'s new location is " +
                player.Place);
        }

        private void AskQuestion()
        {
            var categoryName = (GetCurrentPlayer().Place % 4) switch
            {
                0 => PopCategoryName,
                1 => ScienceCategoryName,
                2 => SportsCategoryName,
                3 => RockCategoryName,
                _ =>
                throw new NotImplementedException()
            };
            HandleQuestionByCategory(categoryName);
        }

        private void HandleQuestionByCategory(string categoryName)
        {
            Console.WriteLine("The category is " + categoryName);
            Console.WriteLine(questionsByCategory[categoryName].First());
            questionsByCategory[categoryName].RemoveFirst();
        }

        public bool WasCorrectlyAnswered()
        {
            if (!IsPlayable())
            {
                throw new InvalidOperationException("Missing player");
            }

            if (inPenaltyBox[currentPlayer] && !isGettingOutOfPenaltyBox)
            {
                IncrementCurrentPlayer();

                return true;
            }

            return HandleCorrectAnswer();
        }

        private bool HandleCorrectAnswer()
        {
            Console.WriteLine("Answer was correct!!!!");
            purses[currentPlayer]++;
            Console.WriteLine($"{GetCurrentPlayerName()} now has {purses[currentPlayer]} Gold Coins.");

            var isWinner = purses[currentPlayer] == GoldenCoinsForVictory;
            IncrementCurrentPlayer();

            return !isWinner;
        }

        public bool WasWronglyAnswered()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(GetCurrentPlayerName() + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;
            IncrementCurrentPlayer();

            return true;
        }

        private void IncrementCurrentPlayer()
        {
            currentPlayer++;

            if (currentPlayer == players.Count)
            {
                currentPlayer = 0;
            }
        }
    }

}
