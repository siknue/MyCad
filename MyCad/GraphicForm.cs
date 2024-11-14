using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyCad
{
    public partial class GraphicForm : Form
    {
        //geometry lists
        private List<Entities.Point> points = new List<Entities.Point>();
        private List<Entities.Line> lines = new List<Entities.Line>();

        //locations
        private Vector3 currentPosition;
        private Vector3 firstPoint;

        //flags
        private int DrawIndex = -1;
        private bool active_drawing = false;
        private int ClickNum = 1;


        public GraphicForm()
        {
            InitializeComponent();
        }

        private void drawing_MouseMove(object sender, MouseEventArgs e)
        {
            currentPosition = PointToCartesian(e.Location);
            label1.Text = string.Format("{0,0:F3},{1,0:F3}", currentPosition.X, currentPosition.Y);
        }

        //get screen dpi
        private float DPI
        {
            get
            {
                using (var g = CreateGraphics())
                    return g.DpiX;
            }
        }
        //convert system point to world point
        private Vector3 PointToCartesian(Point point)
        {
            return new Vector3(Pixel_to_Mm(point.X), Pixel_to_Mm(drawing.Height - point.Y));
        }
        // convert pixel to mm
        private float Pixel_to_Mm(float pixel)
        {
            return pixel * 25.4f / DPI;
        }

        private void drawing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (active_drawing)
                {
                    //描く対象の形状によって分岐
                    //0:点
                    //1:ライン(+端点)
                    switch (DrawIndex)
                    {
                        case 0:
                            points.Add(new Entities.Point(currentPosition));
                            break;

                        case 1:
                            switch (ClickNum)
                            {
                                case 1:
                                    firstPoint = currentPosition;
                                    points.Add(new Entities.Point(currentPosition));
                                    ClickNum++;
                                    break;

                                case 2:
                                    lines.Add(new Entities.Line(firstPoint, currentPosition));
                                    points.Add(new Entities.Point(currentPosition));
                                    firstPoint = currentPosition;
                                    //ClickNum = 1;
                                    break;
                            }
                            break;

                    }

                    drawing.Refresh();


                }
            }
        }

        private void drawing_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(Pixel_to_Mm(drawing.Height));
            Pen pen = new Pen(Color.Blue, 0.1f);

            // draw all points
            if (points.Count > 0)
            {
                foreach (Entities.Point p in points)
                {
                    e.Graphics.DrawPoint(new Pen(Color.Red, 0), p);
                }
            }

            // Draw all lines
            if (lines.Count > 0)
            {
                foreach (Entities.Line line in lines)
                {
                    e.Graphics.DrawLine(pen, line);
                }
            }
        }


        private void pointBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 0;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawIndex = 1;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }

    }
}
