using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task8
{
    public class Hand
    {
        private float length, degrees;
        private PointF centerPoint;

        public Hand(float length, PointF centerPoint, float degrees)
        {
            this.length = length;
            this.centerPoint = centerPoint;
            this.degrees = degrees;
        }

        public Hand(float length) : this(length, new PointF(0, 0), (float)(Math.PI / 2)) { }

        public void TurnClockwise(float degrees)
        {
            this.degrees -= degrees;
            if (this.degrees <= -3 * Math.PI / 2)
            {
                this.degrees += (float)(2 * Math.PI);
            }
        }

        public void Draw(Pen pen, Graphics graphics, float scale, float screenWidth, float screenHeight)
        {
            graphics.DrawLine(pen, ScreenCoords(centerPoint, scale, screenWidth, screenHeight),
                ScreenCoords(RotateLine(), scale, screenWidth, screenHeight));
        }

        private PointF RotateLine()
        {
            return new PointF((float)(length * Math.Cos(degrees) + centerPoint.X),
                (float)(length * Math.Sin(degrees)) + centerPoint.Y);
        }

        private PointF ScreenCoords(PointF point, float scale, float screenWidth, float screenHeight)
        {
            return new PointF((float)(screenWidth / 2f + point.X * scale),
                (float)(screenHeight / 2f - point.Y * scale));
        }
    }
}
