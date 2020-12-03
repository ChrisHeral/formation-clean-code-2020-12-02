namespace Trivia
{
    public interface IReadOnlyPlayer
    {
        string Name { get; }
        int Place { get; }
        int Purse { get; }
        bool InPenaltyBox { get; }
    }

    public class Player : IReadOnlyPlayer
    {
        public string Name { get; }
        public int Place { get; private set; }

        public int Purse { get; private set; }

        public bool InPenaltyBox { get; private set; }

        public Player(string name)
        {
            Name = name;
        }

        public void MovePlayer(int roll)
        {
            const int maxPlaceSize = 12;
            Place = (Place + roll) % maxPlaceSize;
        }

        public void WinGoldenCoin()
        {
            Purse++;
        }

        public void MoveToPenaltyBox()
        {
            InPenaltyBox = true;
        }
        public void LeavePenaltyBox()
        {
            InPenaltyBox = false;
        }

    }
}
