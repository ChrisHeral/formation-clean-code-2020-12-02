namespace SOLID.Liskov
{
    public class Rectangle : ICanComputeArea
    {
        private readonly int width;
        private readonly int height;

        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Area => height * width;
    }
}