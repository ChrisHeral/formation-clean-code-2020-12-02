using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Trivia;
using Xunit;


namespace Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class GameTest_GoldenMaster
    {
        private Game game;

        public GameTest_GoldenMaster()
        {
            game = new Game();
        }

        [Fact]
        public void Test1()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            game.Add("Cedric");
            game.Roll(12);
            game.WrongAnswer();
            game.Roll(2);
            game.Roll(13);
            game.WasCorrectlyAnswered();
            game.Roll(13);
            Approvals.Verify(fakeconsole.ToString());
        }
        
        [Fact]
        public void Test2()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            game.Add("Cedric");
            game.Add("Eloïse");
            game.Roll(1);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            Approvals.Verify(fakeconsole.ToString());
        }
        
        [Fact]
        public void Test3()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            game.Add("Cedric");
            game.Add("Eloïse");
            game.Roll(1);
            game.WrongAnswer();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            Approvals.Verify(fakeconsole.ToString());
        }
    }
}