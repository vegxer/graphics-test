using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Ball
    {
        private PointF location;
        private float directionRadians, stepSize, radius;
        float screenWidth, screenHeight;
        private SolidBrush brush;

        public Ball(PointF location, float radius, float stepSize, float directionRadians, SolidBrush brush,
            float screenWidth, float screenHeight)
        {
            this.location = location;
            this.radius = radius;
            this.stepSize = stepSize;
            this.directionRadians = directionRadians;
            this.brush = brush;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void Move(RectangleF bounds, Graphics graphics)
        {
            bool draw = false;
            if (location.X - radius - bounds.X < stepSize)
            {
                draw = true;
                directionRadians = -directionRadians - (float)Math.PI;
                location = MoveFromTo(location, stepSize, directionRadians);
                Draw(graphics);
            }
            if (bounds.Y - (location.Y + radius) < stepSize)
            {
                draw = true;
                directionRadians = -directionRadians;
                location = MoveFromTo(location, stepSize, directionRadians);
                Draw(graphics);
            }
            if (location.Y - radius - (bounds.Y - bounds.Height) < stepSize)
            {
                draw = true;
                directionRadians = -directionRadians;
                location = MoveFromTo(location, stepSize, directionRadians);
                Draw(graphics);
            }
            if (bounds.X + bounds.Width - (location.X + radius) < stepSize)
            {
                draw = true;
                directionRadians = -directionRadians - (float)Math.PI;
                location = MoveFromTo(location, stepSize, directionRadians);
                Draw(graphics);
            }
            if (!draw)
            {
                location = MoveFromTo(location, stepSize, directionRadians);
                Draw(graphics);
            }
        }

        private PointF MoveFromTo(PointF point, float length, float angle)
        {
            return new PointF((float)(length * Math.Cos(angle) + point.X),
                (float)(length * Math.Sin(angle)) + point.Y);
        }

        private void Draw(Graphics graphics)
        {
            PointF leftUpPoint = ScreenCoords(location.X - radius, location.Y + radius);
            graphics.FillEllipse(brush, leftUpPoint.X, leftUpPoint.Y, 2 * radius, 2 * radius);
        }

        private PointF ScreenCoords(float x, float y)
        {
            return new PointF(screenWidth / 2 + x, screenHeight / 2 - y);
        }
    }
}
