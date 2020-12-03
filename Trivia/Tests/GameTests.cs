using System;
using System.Linq;
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
            game.AddPlayer("Chet");
            game.AddPlayer("Toto");

            // Act : WasCorrectlyAnswered => victory
            var notVictory = game.WasCorrectlyAnswered();

            // Assert
            Assert.True(notVictory);
        }

        [Fact]
        public void A_player_with_6_golden_coins_should_end_game()
        {
            // Arrange : start game with ???
            game.AddPlayer("Chet");
            game.AddPlayer("Toto");
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
            game.AddPlayer("roberto");

            // Act : WasCorrectlyAnswered => victory

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(() => game.WasCorrectlyAnswered());
            Assert.Equal("Missing player", ex.Message);
        }

        [Fact]
        public void should_be_able_to_play_with_two_players()
        {
            // Arrange : start game with ???
            game.AddPlayer("roberto");
            game.AddPlayer("jojo");

            // Act : WasCorrectlyAnswered => victory
            var exception = Record.Exception(() => game.WasCorrectlyAnswered());
            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void should_not_be_able_to_move_if_in_penalty_box_and_roll_even()
        {
            // Arrange : start game with ???
            game.AddPlayer("roberto");
            game.AddPlayer("jojo");
            game.Roll(4);
            game.WasWronglyAnswered();
            game.Roll(6);
            game.WasCorrectlyAnswered();
            game.Roll(4);

            // Act
            IReadOnlyPlayer readOnlyPlayer = game.GetPlayers().First();

            // Assert
            Assert.True(readOnlyPlayer.InPenaltyBox);
        }

        [Fact]
        public void should_be_able_to_move_if_in_penalty_box_and_roll_odd()
        {
            // Arrange : start game with ???
            game.AddPlayer("roberto");
            game.AddPlayer("jojo");
            game.Roll(4);
            game.WasWronglyAnswered();
            game.Roll(6);
            game.WasCorrectlyAnswered();
            game.Roll(3);

            // Act
            IReadOnlyPlayer readOnlyPlayer = game.GetPlayers().First();

            // Assert
            Assert.False(readOnlyPlayer.InPenaltyBox);
        }

        [Fact]
        public void should_not_win_a_golden_coin_if_in_penalty_box_and_roll_even()
        {
            // Arrange : start game with ???
            game.AddPlayer("roberto");
            game.AddPlayer("jojo");
            game.Roll(4);
            game.WasWronglyAnswered();
            game.Roll(6);
            game.WasCorrectlyAnswered();

            // Act
            game.Roll(4);
            game.WasCorrectlyAnswered();
            IReadOnlyPlayer readOnlyPlayer = game.GetPlayers().First();

            // Assert
            Assert.Equal(0, readOnlyPlayer.Purse);
        }

        [Fact]
        public void should_win_a_golden_coin_if_in_penalty_box_and_roll_odd()
        {
            // Arrange : start game with ???
            game.AddPlayer("roberto");
            game.AddPlayer("jojo");
            game.Roll(4);
            game.WasWronglyAnswered();
            game.Roll(6);
            game.WasCorrectlyAnswered();

            // Act
            game.Roll(3);
            game.WasCorrectlyAnswered();
            IReadOnlyPlayer readOnlyPlayer = game.GetPlayers().First();

            // Assert
            Assert.Equal(1, readOnlyPlayer.Purse);
        }

        [Fact]
        public void should_win_a_golden_coin_if_not_in_penalty_box_and_roll_and_correct_answer()
        {
            // Arrange : start game with ???
            game.AddPlayer("roberto");
            game.AddPlayer("jojo");

            // Act
            game.Roll(4);
            game.WasCorrectlyAnswered();
            IReadOnlyPlayer readOnlyPlayer = game.GetPlayers().First();

            // Assert
            Assert.Equal(1, readOnlyPlayer.Purse);
        }
    }
}
