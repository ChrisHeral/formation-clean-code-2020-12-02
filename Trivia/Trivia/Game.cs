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

        private LinkedList<string> GetQuestions(string categoryName)
        {
            return null;
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

            if (places[currentPlayer] % 4 == 0)
            {
                Console.WriteLine("The category is " + PopCategoryName);
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (places[currentPlayer] % 4 == 1)
            {

                Console.WriteLine("The category is " + ScienceCategoryName);
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (places[currentPlayer] % 4 == 2)
            {

                Console.WriteLine("The category is " + SportsCategoryName);
                Console.WriteLine(sportQuestions.First());
                sportQuestions.RemoveFirst();
            }
            if (places[currentPlayer] % 4 == 3)
            {

                Console.WriteLine("The category is " + RockCategoryName);
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }

        public bool WasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer] +
                        " now has " +
                        purses[currentPlayer] +
                        " Gold Coins.");

                    var winner = !(purses[currentPlayer] == 6);
                    IncrementCurrentPlayer();

                    return winner;
                }
                else
                {
                    IncrementCurrentPlayer();

                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer] +
                    " now has " +
                    purses[currentPlayer] +
                    " Gold Coins.");

                var winner = !(purses[currentPlayer] == 6);
                IncrementCurrentPlayer();

                return winner;
            }
        }

        public bool WrongAnswer()
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
