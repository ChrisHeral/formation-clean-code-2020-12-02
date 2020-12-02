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

        private readonly List<string> players = new List<string>();

        private readonly int[] places = new int[MaximumNumberOfPlayers];
        private readonly int[] purses = new int[MaximumNumberOfPlayers];
        private readonly bool[] inPenaltyBox = new bool[MaximumNumberOfPlayers];

        private readonly Dictionary<string, LinkedList<string>> questionsByCategory = new Dictionary<string, LinkedList<string>>
        {
            { PopCategoryName, new LinkedList<string>() },
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

        public bool Add(string playerName)
        {
            players.Add(playerName);
            places[HowManyPlayers()] = 0;
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
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    //User is getting out of penalty box
                    isGettingOutOfPenaltyBox = true;
                    //Write that user is getting out
                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    // add roll to place
                    MovePlayer(roll);
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                MovePlayer(roll);
                AskQuestion();
            }
        }

        private void MovePlayer(int roll)
        {
            const int maxPlaceSize = 12;
            places[currentPlayer] = (places[currentPlayer] + roll) % maxPlaceSize;

            Console.WriteLine(players[currentPlayer] +
                "'s new location is " +
                places[currentPlayer]);
        }

        private void AskQuestion()
        {
            var categoryName = (places[currentPlayer] % 4) switch
            {
                0 => PopCategoryName,
                1 => ScienceCategoryName,
                2 => SportsCategoryName,
                3 => RockCategoryName,
                _ => throw new NotImplementedException()
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
            Console.WriteLine($"{players[currentPlayer]} now has {purses[currentPlayer]} Gold Coins.");

            var isWinner = purses[currentPlayer] == GoldenCoinsForVictory;
            IncrementCurrentPlayer();

            return !isWinner;
        }

        public bool WasWronglyAnswered()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
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
