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
        private List<Entities.Circle> circles = new List<Entities.Circle>();

        //locations
        private Vector3 currentPosition;
        private Vector3 firstPoint;

        //flags
        private int DrawingToolIndex = -1;
        private bool IsDrawingToolActive = false;
        private bool isUnderDrawing = false;



        public GraphicForm()
        {
            InitializeComponent();
        }

        private void drawing_MouseMove(object sender, MouseEventArgs e)
        {
            currentPosition = PointToCartesian(e.Location);
            label1.Text = string.Format("{0,0:F3},{1,0:F3}", currentPosition.X, currentPosition.Y);
            drawing.Refresh();
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
                if (IsDrawingToolActive)
                {
                    //描く対象の形状によって分岐
                    //0:点
                    //1:ライン(+端点)
                    //2:円
                    switch (DrawingToolIndex)
                    {
                        case 0:
                            points.Add(new Entities.Point(currentPosition));
                            break;

                        case 1:
                            if (isUnderDrawing)
                            {
                                lines.Add(new Entities.Line(firstPoint, currentPosition));
                                points.Add(new Entities.Point(currentPosition));
                                firstPoint = currentPosition;
                                
                            }
                            else
                            {
                                firstPoint = currentPosition;
                                points.Add(new Entities.Point(currentPosition));
                                isUnderDrawing = true;
                            }
                            break;

                        case 2:
                            if (isUnderDrawing)
                            {
                                double r = firstPoint.DistanceFrom(currentPosition);
                                circles.Add(new Entities.Circle(firstPoint, r));
                                isUnderDrawing = false;
                            }
                            else
                            {
                                firstPoint = currentPosition;
                                isUnderDrawing = true;
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
            Pen extpen = new Pen(Color.Gray, 0.1f);

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

            // Draw all circles
            if (circles.Count > 0)
            {
                foreach (Entities.Circle circle in circles)
                {
                    e.Graphics.DrawCircle(pen, circle);
                }
            }

            //draw line の最中に線を描画する
            switch (DrawingToolIndex)
            {
                //線の描画モード中
                case 1:
                    if (isUnderDrawing)
                    {
                        Entities.Line line = new Entities.Line(firstPoint, currentPosition);
                        e.Graphics.DrawLine(extpen, line);
                    }
                    break;

                case 2:
                    if (isUnderDrawing)
                    {
                        double r = firstPoint.DistanceFrom(currentPosition);
                        Entities.Circle circle = new Entities.Circle(firstPoint, r);
                        e.Graphics.DrawCircle(extpen, circle);

                        Entities.Line line = new Entities.Line(firstPoint, currentPosition);
                        e.Graphics.DrawLine(extpen, line);
                    }
                    break;
            }
            if (lines.Count > 0)
            {
                for (int i=0 ; i < lines.Count; i++)
                {
                    for(int j = i; j<lines.Count;j++)
                    {
                        var line1 = lines[i];
                        var line2 = lines[j];
                        Vector3 v = Methods.Methods.LineLineIntersection(line1, line2, extended:false);
                        Entities.Point p = new Entities.Point(v);
                        e.Graphics.DrawPoint(new Pen(Color.Red, 0), p);
                    }
                }
            }
        }


        private void pointBtn_Click(object sender, EventArgs e)
        {
            DrawingToolIndex = 0;
            IsDrawingToolActive = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawingToolIndex = 1;
            IsDrawingToolActive = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void CircleBtn_Click(object sender, EventArgs e)
        {
            DrawingToolIndex = 2;
            IsDrawingToolActive = true;
            drawing.Cursor = Cursors.Cross;
        }

        private void CancelAll()
        {
            DrawingToolIndex = -1;
            IsDrawingToolActive = false;
            drawing.Cursor = Cursors.Default;
            isUnderDrawing = false;

        }
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelAll();
        }

        private void GraphicForm_Load(object sender, EventArgs e)
        {

        }
    }
}
