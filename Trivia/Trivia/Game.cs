using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private const int GoldenCoinsForVictory = 6;
        private const string PopCategoryName = "Pop";
        private const string ScienceCategoryName = "Science";
        private const string SportsCategoryName = "Sports";
        private const string RockCategoryName = "Rock";

        private readonly List<Player> players = new List<Player>();

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

        public IEnumerable<IReadOnlyPlayer> GetPlayers()
        {
            return players.Select(x => x);
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            players.Add(player);

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
            Console.WriteLine(GetCurrentPlayer().Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            var isEven = roll % 2 == 0;

            if (GetCurrentPlayer().InPenaltyBox)
            {
                if (isEven)
                {
                    Console.WriteLine(GetCurrentPlayer().Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
                else
                {
                    Console.WriteLine(GetCurrentPlayer().Name + " is getting out of the penalty box");
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

        public Player GetCurrentPlayer()
        {
            return players[currentPlayer];
        }

        private void MovePlayer(int roll)
        {
            var player = GetCurrentPlayer();
            player.MovePlayer(roll);

            Console.WriteLine(GetCurrentPlayer().Name +
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

            if (GetCurrentPlayer().InPenaltyBox && !isGettingOutOfPenaltyBox)
            {
                IncrementCurrentPlayer();

                return true;
            }

            return HandleCorrectAnswer();
        }

        private bool HandleCorrectAnswer()
        {
            Console.WriteLine("Answer was correct!!!!");
            GetCurrentPlayer().WinGoldenCoin();
            Console.WriteLine($"{GetCurrentPlayer().Name} now has {GetCurrentPlayer().Purse} Gold Coins.");

            var isWinner = GetCurrentPlayer().Purse == GoldenCoinsForVictory;
            IncrementCurrentPlayer();

            return !isWinner;
        }

        public bool WasWronglyAnswered()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(GetCurrentPlayer().Name + " was sent to the penalty box");

            GetCurrentPlayer().MoveToPenaltyBox();
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
