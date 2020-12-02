using Trivia;
using Xunit;

namespace Tests
{
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
            game.WasCorrectlyAnswered();
            game.WasCorrectlyAnswered();
            game.WasCorrectlyAnswered();
            game.WasCorrectlyAnswered();
            game.WasCorrectlyAnswered();

            // Act : WasCorrectlyAnswered => victory
            var notVictory = game.WasCorrectlyAnswered();

            // Assert
            Assert.False(notVictory);
        }
    }
}
