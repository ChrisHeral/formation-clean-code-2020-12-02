namespace Trivia
{
    public class Player
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
