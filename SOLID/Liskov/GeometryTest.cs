using Xunit;

namespace SOLID.Liskov
{
    public class GeometryTest
    {
        [Fact]
        public void Area_should_be_height_times_width_1()
        {
            area_should_be_height_times_width(new Rectangle(5,20));
        }

        [Fact]
        public void Area_should_be_height_times_width_2()
        {
            area_should_be_height_times_width(new Square(10));
        }

        private void area_should_be_height_times_width(ICanComputeArea rect)
        {
            Assert.Equal(100, rect.Area);
        }
    }
}