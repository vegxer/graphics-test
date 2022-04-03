using System;
using System.Drawing;
using System.Windows.Forms;

namespace task8
{
    public partial class Form1 : Form
    {
        private Pen hoursPen = new Pen(Color.Black, 12),
            minutesPen = new Pen(Color.Black, 6),
            secondsPen = new Pen(Color.Red, 3);
        private float scale, step = 6 * (float)Math.PI / 180;
        private Hand hoursHand, minutesHand, secondsHand;
        private float clockRadius;

        //Для циферблата
        double degree, innerRadius, numbersRadius, shortLinesRadius;
        PointF centerPoint, numbersCenterPoint;
        Font numbersFont;
        SolidBrush brush = new SolidBrush(Color.Black);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int minSize = Math.Min(ClientSize.Width, ClientSize.Height);
            scale = minSize / 54;
            clockRadius = minSize / 2 - 10;
            hoursHand = new Hand(minSize / (147f / 25f) / scale);
            minutesHand = new Hand(minSize / (21f / 5f) / scale);
            secondsHand = new Hand(minSize / 3f / scale);

            degree = -Math.PI / 2;
            innerRadius = clockRadius / 1.2d;
            numbersRadius = clockRadius / 1.3d;
            shortLinesRadius = clockRadius / 1.075f;
            centerPoint = new PointF(ClientSize.Width / 2f, ClientSize.Height / 2f);
            numbersCenterPoint = new PointF(ClientSize.Width / 2f - minSize / 28, ClientSize.Height / 2f - minSize / 28);
            numbersFont = new Font("Arial", scale * 3);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            secondsHand.TurnClockwise(step);
            minutesHand.TurnClockwise(step / 60);
            hoursHand.TurnClockwise(step / (60 * 12));
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (timer.Enabled)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                DrawClock(e.Graphics);
                secondsHand.Draw(secondsPen, e.Graphics, scale, ClientSize.Width, ClientSize.Height);
                minutesHand.Draw(minutesPen, e.Graphics, scale, ClientSize.Width, ClientSize.Height);
                hoursHand.Draw(hoursPen, e.Graphics, scale, ClientSize.Width, ClientSize.Height);
            }
        }

        private void DrawClock(Graphics graphics)
        {
            graphics.DrawEllipse(minutesPen, new RectangleF(ClientSize.Width / 2f - clockRadius, ClientSize.Height / 2f - clockRadius,
                2 * clockRadius, 2 * clockRadius));
            for (int i = 0; i < 60; ++i)
            {
                double currDegree = degree + i * Math.PI / 30;
                if (i % 5 == 0)
                {
                    graphics.DrawLine(minutesPen, RotateLine(centerPoint, innerRadius, currDegree),
                        RotateLine(centerPoint, clockRadius, currDegree));
                    graphics.DrawString((i == 0 ? 12 : i / 5).ToString(), numbersFont, brush,
                        RotateLine(numbersCenterPoint, numbersRadius, currDegree));
                }
                else
                {
                    graphics.DrawLine(minutesPen, RotateLine(centerPoint, shortLinesRadius, currDegree),
                        RotateLine(centerPoint, clockRadius, currDegree));
                }
            }
        }

        private PointF RotateLine(PointF startPoint, double radius, double angle)
        {
            return new PointF((float)(radius * Math.Cos(angle) + startPoint.X),
                (float)(radius * Math.Sin(angle)) + startPoint.Y);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Старт")
            {
                int speed;
                if (int.TryParse(speedTextBox.Text, out speed) && speed > 0 && speed < 101)
                {
                    startButton.Text = "Стоп";
                    speedTextBox.ReadOnly = true;
                    timer.Interval = 1000 / speed;
                    timer.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Необходимо натуральное число от 1 до 100", "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                timer.Enabled = false;
                speedTextBox.ReadOnly = false;
                startButton.Text = "Старт";
            }
        }
    }
}
