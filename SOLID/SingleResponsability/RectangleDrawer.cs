using System.Drawing;

namespace SOLID.SingleResponsability
{
    public class RectangleDrawer : IDrawable
    {
        private readonly Rectangle _rectangle;

        public RectangleDrawer(Rectangle rectangle)
        {
            _rectangle = rectangle;
        }

        public void Draw(Graphics graphics)
        {
            //top horizontal line
            graphics.DrawLine(Pens.Black,
                _rectangle.topLeft.X, _rectangle.topLeft.Y,
                _rectangle.bottomRight.X, _rectangle.topLeft.Y
                );
            //bottom horizontal line
            graphics.DrawLine(Pens.Black,
                _rectangle.topLeft.X, _rectangle.bottomRight.Y,
                _rectangle.bottomRight.X, _rectangle.bottomRight.Y
                );
            //left vertical line
            graphics.DrawLine(Pens.Black,
                _rectangle.topLeft.X, _rectangle.topLeft.Y,
                _rectangle.topLeft.X, _rectangle.topLeft.Y - _rectangle.Heigth
                );
            //right vertical line
            graphics.DrawLine(Pens.Black,
                _rectangle.bottomRight.X, _rectangle.bottomRight.Y - _rectangle.Heigth,
                _rectangle.bottomRight.X, _rectangle.bottomRight.Y
                );
        }
    }
}
