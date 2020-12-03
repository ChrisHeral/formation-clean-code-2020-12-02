namespace SOLID.Liskov
{
    public class Square : ICanComputeArea
    {
        private readonly int side;

        public Square(int side)
        {
            this.side = side;
        }

        public int Area => side * side;
    }
}