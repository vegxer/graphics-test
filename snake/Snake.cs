using snake.Directions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    public class Snake
    {
        private class SnakeBall
        {
            public PointF CenterCoords;
            public float Radius;
            private Direction lastMove;

            public SnakeBall(PointF centerCoords, float radius)
            {
                CenterCoords = centerCoords;
                Radius = radius;
            }

            public void MoveTo(Direction direction)
            {
                if (direction is DirectionRight)
                {
                    CenterCoords.X += 2 * Radius;
                }
                else if (direction is DirectionLeft)
                {
                    CenterCoords.X -= 2 * Radius;
                }
                else if (direction is DirectionUp)
                {
                    CenterCoords.Y += 2 * Radius;
                }
                else if (direction is DirectionDown)
                {
                    CenterCoords.Y -= 2 * Radius;
                }

                lastMove = direction.Turn180().Turn180();
            }

            public void MoveBack()
            {
                if (lastMove is DirectionRight)
                {
                    CenterCoords.X -= 2 * Radius;
                }
                else if (lastMove is DirectionLeft)
                {
                    CenterCoords.X += 2 * Radius;
                }
                else if (lastMove is DirectionUp)
                {
                    CenterCoords.Y -= 2 * Radius;
                }
                else if (lastMove is DirectionDown)
                {
                    CenterCoords.Y += 2 * Radius;
                }
            }
        }


        private List<SnakeBall> balls;
        private float scale, screenWidth, screenHeight;
        private SolidBrush brush;
        public Direction NextDirection = new DirectionRight();
        private bool isNearWall = false;
        private PointF detourStartPoint;

        public Snake(int size, float ballRadius, float scale, float screenWidth, float screenHeight, SolidBrush brush)
        {
            balls = new List<SnakeBall>();
            for (int i = 0; i < size; ++i)
            {
                balls.Add(new SnakeBall(new PointF(((i + 1) * ballRadius * 2 * scale - screenWidth / 2) / scale,
                        (2 * ballRadius * scale - screenHeight / 2) / -scale),
                    ballRadius));
            }
            this.scale = scale;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.brush = brush;
        }

        private PointF ScreenCoords(double x, double y)
        {
            return new PointF((float)(screenWidth / 2 + x * scale), (float)(screenHeight / 2 - y * scale));
        }

        public Direction MoveTo(Direction direction, List<GraphicsPath> figures, Graphics graphics)
        {
            if (balls.Count == 0)
            {
                return new DirectionRight();
            }

            for (int i = 0; i < balls.Count - 1; ++i)
            {
                balls[i] = new SnakeBall(balls[i + 1].CenterCoords, balls[i + 1].Radius);
            }

            int j = balls.Count - 1;

            if (isNearWall)
            {
                balls[j].MoveTo(NextDirection);
                if (Math.Abs(balls[j].CenterCoords.X - detourStartPoint.X) < balls[j].Radius &&
                    Math.Abs(balls[j].CenterCoords.Y - detourStartPoint.Y) < balls[j].Radius)
                {
                    isNearWall = false;
                    balls[j].MoveBack();
                    return direction;
                }
                if (!HitTheWall(balls[j], figures))
                {
                    direction = NextDirection.Turn180().Turn180();
                    SnakeBall testBall = new SnakeBall(balls[j].CenterCoords, balls[j].Radius);
                    testBall.MoveTo(direction);
                    testBall.MoveTo(direction.TurnClockwise());
                    if (HitTheWall(testBall, figures))
                    {
                        NextDirection = direction.TurnClockwise();
                    }
                    else
                    {
                        testBall.MoveBack();
                        testBall.MoveTo(direction.TurnCounterClockwise());
                        if (HitTheWall(testBall, figures))
                        {
                            NextDirection = direction.TurnCounterClockwise();
                        }
                        else
                        {
                            NextDirection = direction.Turn180().Turn180();
                        }
                    }
                }
                balls[j].MoveBack();
            }
            balls[j].MoveTo(direction);
            if (HitTheWall(balls[j], figures))
            {
                if (!isNearWall)
                {
                    detourStartPoint = new PointF(balls[j].CenterCoords.X, balls[j].CenterCoords.Y);
                    isNearWall = true;
                }
                NextDirection = direction.Turn180().Turn180();
                balls[j].MoveBack();
                direction = direction.TurnClockwise();
                balls[j].MoveTo(direction);
                if (HitTheWall(balls[j], figures))
                {
                    NextDirection = direction.Turn180().Turn180();
                    balls[j].MoveBack();
                    direction = direction.Turn180();
                    balls[j].MoveTo(direction);
                    if (HitTheWall(balls[j], figures))
                    {
                        NextDirection = direction.TurnClockwise();
                        balls[j].MoveBack();
                        direction = direction.TurnCounterClockwise();
                        balls[j].MoveTo(direction);
                    }
                }
            }

            Draw(graphics);
            return direction;
        }

        private bool HitTheWall(SnakeBall ball, List<GraphicsPath> figures)
        {
            float x = ball.CenterCoords.X, y = ball.CenterCoords.Y, r = ball.Radius;

            if (ScreenCoords(x + r, y).X >= screenWidth || ScreenCoords(x - r, y).X <= 0
                || ScreenCoords(x, y + r).Y <= 0 || ScreenCoords(x, y - r).Y >= screenHeight)
            {
                return true;
            }

            for (int i = 0; i < figures.Count; ++i)
            {
                if (figures[i].IsVisible(ScreenCoords(x + r, y)) || figures[i].IsVisible(ScreenCoords(x - r, y))
                    || figures[i].IsVisible(ScreenCoords(x, y + r)) || figures[i].IsVisible(ScreenCoords(x, y - r)))
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(Graphics graphics)
        {
            brush.Color = Color.Red;
            PointF leftUpPoint = ScreenCoords(balls[balls.Count - 1].CenterCoords.X - balls[balls.Count - 1].Radius,
                balls[balls.Count - 1].CenterCoords.Y + balls[balls.Count - 1].Radius);
            graphics.FillEllipse(brush, leftUpPoint.X, leftUpPoint.Y, 2 * balls[balls.Count - 1].Radius * scale,
                2 * balls[balls.Count - 1].Radius * scale);

            brush.Color = Color.Black;
            for (int i = 0; i < balls.Count - 1; ++i)
            {
                leftUpPoint = ScreenCoords(balls[i].CenterCoords.X - balls[i].Radius, balls[i].CenterCoords.Y + balls[i].Radius);
                graphics.FillEllipse(brush, leftUpPoint.X, leftUpPoint.Y, 2 * balls[i].Radius * scale, 2 * balls[i].Radius * scale);
            }
        }
    }
}
