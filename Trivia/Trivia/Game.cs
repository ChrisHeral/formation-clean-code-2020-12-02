﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private const int MaximumNumberOfPlayers = 6;
        private const string PopCategoryName = "Pop";
        private const string ScienceCategoryName = "Science";
        private const string SportsCategoryName = "Sports";
        private const string RockCategoryName = "Rock";

        private readonly List<string> players = new List<string>();

        private readonly int[] places = new int[MaximumNumberOfPlayers];
        private readonly int[] purses = new int[MaximumNumberOfPlayers];
        private readonly bool[] inPenaltyBox = new bool[MaximumNumberOfPlayers];

        private readonly LinkedList<string> popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> sportQuestions = new LinkedList<string>();
        private readonly LinkedList<string> rockQuestions = new LinkedList<string>();

        private int currentPlayer;
        private bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var questionNumber = 0; questionNumber < 50; questionNumber++)
            {
                popQuestions.AddLast(CreateQuestion(questionNumber, PopCategoryName));
                scienceQuestions.AddLast(CreateQuestion(questionNumber, ScienceCategoryName));
                sportQuestions.AddLast(CreateQuestion(questionNumber, SportsCategoryName));
                rockQuestions.AddLast(CreateQuestion(questionNumber, RockCategoryName));
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
            Console.WriteLine(players[currentPlayer] + " is the current player"); Console.WriteLine("They have rolled a " + roll);


            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    //User is getting out of penalty box
                    isGettingOutOfPenaltyBox = true;
                    //Write that user is getting out
                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    // add roll to place
                    places[currentPlayer] = places[currentPlayer] + roll;
                    if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                    Console.WriteLine(players[currentPlayer]
                            + "'s new location is "
                            + places[currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
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
                places[currentPlayer] = places[currentPlayer] + roll;
                if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                Console.WriteLine(players[currentPlayer]
                        + "'s new location is "
                        + places[currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == PopCategoryName)
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == ScienceCategoryName)
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == SportsCategoryName)
            {
                Console.WriteLine(sportQuestions.First());
                sportQuestions.RemoveFirst();
            }
            if (CurrentCategory() == RockCategoryName)
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            if (places[currentPlayer] == 0) return PopCategoryName;
            if (places[currentPlayer] == 4) return PopCategoryName;
            if (places[currentPlayer] == 8) return PopCategoryName;
            if (places[currentPlayer] == 1) return ScienceCategoryName;
            if (places[currentPlayer] == 5) return ScienceCategoryName;
            if (places[currentPlayer] == 9) return ScienceCategoryName;
            if (places[currentPlayer] == 2) return SportsCategoryName;
            if (places[currentPlayer] == 6) return SportsCategoryName;
            if (places[currentPlayer] == 10) return SportsCategoryName;
            return RockCategoryName;
        }



        public bool WasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    var winner = !(purses[currentPlayer] == 6);
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                var winner = !(purses[currentPlayer] == 6);
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }



    }

}
