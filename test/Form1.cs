using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        Timer timer;
        SolidBrush brush = new SolidBrush(Color.Black);
        float width;
        float height;
        float step = 5f;
        Ball ball;
        RectangleF ballArea;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            width = ClientSize.Width;
            height = ClientSize.Height;
            ballArea = new RectangleF(-width / 2 + width / 20, height / 2 - height / 10, width - width / 10, height - height / 5);
            Random rand = new Random();
            float radius = width * height / 25000;
            ball = new Ball(new PointF((float)(rand.NextDouble() * ((ballArea.X + ballArea.Width - (2 * radius)) - (ballArea.X + 2 * radius)) + (ballArea.X + 2 * radius)),
                    (float)(rand.NextDouble() * ((ballArea.Y - (2 * radius)) - (ballArea.Y - ballArea.Height + 2 * radius)) + (ballArea.Y - ballArea.Height + 2 * radius))),
                    radius, step, (float)(rand.NextDouble() * 2 * Math.PI), new SolidBrush(Color.White), width, height);
            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 10;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DrawBoard(e.Graphics);
            ball.Move(ballArea, e.Graphics);
        }

        private void DrawBoard(Graphics graphics)
        {
            brush.Color = Color.Black;
            graphics.FillRectangle(brush, 0, 0, width, height);
            brush.Color = Color.Green;
            graphics.FillRectangle(brush, width / 20, height / 10, width - width / 10, height - height / 5);
        }
    }
}
