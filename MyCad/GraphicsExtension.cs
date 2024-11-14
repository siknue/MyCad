﻿namespace MyCad
{
    internal static class GraphicsExtension
    {
        private static float Height;

        public static void SetParameters(this System.Drawing.Graphics g, float height)
        {
            Height = height;
        }

        public static void SetTransform(this System.Drawing.Graphics g)
        {
            g.PageUnit = System.Drawing.GraphicsUnit.Millimeter;
            g.TranslateTransform(0, Height);
            g.ScaleTransform(1.0f, -1.0f);

        }

        public static void DrawPoint(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Point point)
        {
            g.SetTransform();
            System.Drawing.PointF p = point.Position.ToPointF;
            g.DrawEllipse(pen, p.X - 1, p.Y - 1, 2, 2);
            g.ResetTransform();
        }

        public static void DrawLine(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Line line)
        {
            g.SetTransform();
            g.DrawLine(pen, line.startPoint.ToPointF, line.endPoint.ToPointF);
            g.ResetTransform();
        }

        public static void DrawCircle(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Circle circle)
        {
            //左上隅の座標が基準
            float x = (float)(circle.vector3.X - circle.radius);
            float y = (float)(circle.vector3.Y - circle.radius);
            float w = (float)(circle.Diameter);

            g.SetTransform();
            g.DrawEllipse(pen, x, y, w, w);
            g.ResetTransform();
        }
    }
}
