using snake.Directions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace snake
{
    public partial class Form1 : Form
    {
        Pen pen = new Pen(Color.Black, 2);
        Timer timer;
        SolidBrush brush = new SolidBrush(Color.Black);
        float scale = 20, width, height;
        Snake snake;
        Direction snakeDirection = new DirectionRight();
        List<GraphicsPath> lets;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            width = ClientSize.Width;
            height = ClientSize.Height;
            snake = new Snake(10, 0.5f, scale, width, height, brush);

            lets = new List<GraphicsPath>();
            GraphicsPath let = new GraphicsPath();
            let.AddLine(new PointF(width / 10, height / 10), new PointF(width / 4, height / 10));
            let.AddLine(new PointF(width / 4, height / 10), new PointF(width / 4, height / 5));
            let.AddLine(new PointF(width / 4, height / 5), new PointF(width / 6, height / 5));
            let.AddLine(new PointF(width / 6, height / 5), new PointF(width / 6, height / 3));
            let.AddLine(new PointF(width / 6, height / 3), new PointF(width / 10, height / 3));
            let.AddLine(new PointF(width / 10, height / 3), new PointF(width / 10, height / 10));
            lets.Add(let);
            GraphicsPath let1 = new GraphicsPath();
            let1.AddRectangle(new RectangleF(width / 9, height / 1.5f, width / 10, height / 6));
            lets.Add(let1);
            GraphicsPath let2 = new GraphicsPath();
            let2.AddRectangle(new RectangleF(width / 2, height / 4, width / 10, height / 3));
            lets.Add(let2);
            GraphicsPath let3 = new GraphicsPath();
            let3.AddLine(new PointF(width / 1.2f, height / 1.7f), new PointF(width / 1.2f, height / 1.1f));
            let3.AddLine(new PointF(width / 1.2f, height / 1.1f), new PointF(width / 1.35f, height / 1.1f));
            let3.AddLine(new PointF(width / 1.35f, height / 1.1f), new PointF(width / 1.35f, height / 1.2f));
            let3.AddLine(new PointF(width / 1.35f, height / 1.2f), new PointF(width / 1.3f, height / 1.2f));
            let3.AddLine(new PointF(width / 1.3f, height / 1.2f), new PointF(width / 1.3f, height / 1.7f));
            let3.AddLine(new PointF(width / 1.3f, height / 1.7f), new PointF(width / 1.2f, height / 1.7f));
            lets.Add(let3);

            GraphicsPath let4 = new GraphicsPath();
            let4.AddLine(new PointF(width / 3.2f, height / 1.7f), new PointF(width / 2.7f, height / 1.7f));
            let4.AddLine(new PointF(width / 2.7f, height / 1.7f), new PointF(width / 2.7f, height / 1.2f));
            let4.AddLine(new PointF(width / 2.7f, height / 1.2f), new PointF(width / 2.3f, height / 1.2f));
            let4.AddLine(new PointF(width / 2.3f, height / 1.2f), new PointF(width / 2.3f, height / 1.1f));
            let4.AddLine(new PointF(width / 2.3f, height / 1.1f), new PointF(width / 3.2f, height / 1.1f));
            let4.AddLine(new PointF(width / 3.2f, height / 1.1f), new PointF(width / 3.2f, height / 1.7f));
            lets.Add(let4);
            GraphicsPath let5 = new GraphicsPath();
            let5.AddRectangle(new RectangleF(width / 1.4f, height / 10, width / 4, height / 10));
            lets.Add(let5);

            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 100;
            timer.Enabled = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                snakeDirection = new DirectionLeft();
                snake.NextDirection = new DirectionLeft();
            }
            else if (e.KeyCode == Keys.Right)
            {
                snakeDirection = new DirectionRight();
                snake.NextDirection = new DirectionRight();
            }
            else if (e.KeyCode == Keys.Down)
            {
                snakeDirection = new DirectionDown();
                snake.NextDirection = new DirectionDown();
            }
            else if (e.KeyCode == Keys.Up)
            {
                snakeDirection = new DirectionUp();
                snake.NextDirection = new DirectionUp();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            foreach (GraphicsPath let in lets)
            {
                e.Graphics.DrawPath(pen, let);
            }
            snakeDirection = snake.MoveTo(snakeDirection, lets, e.Graphics);
        }
    }
}
