using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCad
{
    public partial class GraphicForm : Form
    {
        private Vector3 currentPosition;

        private List<Entities.Point> points = new List<Entities.Point>();
        private int DrawIndex = -1;
        private bool active_drawing = false;

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
            return new Vector3(Pixel_to_Mm(point.X), Pixel_to_Mm(drawing.Height-point.Y));
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
                    switch (DrawIndex)
                    {
                        case 0:
                            points.Add(new Entities.Point(currentPosition));
                            break;
                    }

                    drawing.Refresh();
                    
                    
                }
            }
        }

        private void drawing_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(Pixel_to_Mm(drawing.Height));

            if(points.Count > 0)
            {
                foreach (Entities.Point p in points)
                {
                    e.Graphics.DrawPoint(new Pen(Color.Red, 0), p);
                }
            }
        }

        private void pointBtn_Click(object sender, EventArgs e)
        {
            DrawIndex = 0;
            active_drawing = true;
            drawing.Cursor = Cursors.Cross;
        }
    }
}
