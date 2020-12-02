﻿using System;
using Trivia;
using Xunit;

namespace Tests
{
    [Collection("Sequential")]
    public class GameTests
    {
        private Game game;

        public GameTests()
        {
            game = new Game();
        }

        [Fact]
        public void A_player_without_6_golden_coins_should_not_end_game()
        {
            // Arrange : start game with ???
            game.Add("Chet");
            game.Add("Toto");

            // Act : WasCorrectlyAnswered => victory
            var notVictory = game.WasCorrectlyAnswered();

            // Assert
            Assert.True(notVictory);
        }

        [Fact]
        public void A_player_with_6_golden_coins_should_end_game()
        {
            // Arrange : start game with ???
            game.Add("Chet");
            game.Add("Toto");
            game.WasCorrectlyAnswered();
            game.WasWronglyAnswered();
            game.WasCorrectlyAnswered();
            game.WasWronglyAnswered();
            game.WasCorrectlyAnswered();
            game.WasWronglyAnswered();
            game.WasCorrectlyAnswered();
            game.WasWronglyAnswered();
            game.WasCorrectlyAnswered();
            game.WasWronglyAnswered();

            // Act : WasCorrectlyAnswered => victory
            var notVictory = game.WasCorrectlyAnswered();

            // Assert
            Assert.False(notVictory);
        }

        [Fact]
        public void should_not_answer_question_if_without_player()
        {
            // Arrange : start game with ???


            // Act : WasCorrectlyAnswered => victory

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(() => game.WasCorrectlyAnswered());
            Assert.Equal("Missing player", ex.Message);
        }

        [Fact]
        public void should_not_answer_question_with_only_one_player()
        {
            // Arrange : start game with ???
            game.Add("roberto");

            // Act : WasCorrectlyAnswered => victory

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(() => game.WasCorrectlyAnswered());
            Assert.Equal("Missing player", ex.Message);
        }

        [Fact]
        public void should_be_able_to_play_with_two_players()
        {
            // Arrange : start game with ???
            game.Add("roberto");
            game.Add("jojo");

            // Act : WasCorrectlyAnswered => victory
            var exception = Record.Exception(() => game.WasCorrectlyAnswered());
            // Assert
            Assert.Null(exception);
        }
    }
}
