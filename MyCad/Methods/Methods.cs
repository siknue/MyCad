using MyCad.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCad.Methods
{
    public static class Methods
    {
        public static double LineAngle(Vector3 start, Vector3 end)
        {
            
            double angle = Math.Atan2(end.Y-start.Y, end.X-start.X)*180.0/Math.PI;
            
            if (angle < 0.0)
            {
                angle += 360;
            }
            
            return angle;
        }

        public static Vector3 LineLineIntersection(Entities.Line line1, Entities.Line line2, bool extended)
        {
            Vector3 result;
            Vector3 p1 = line1.startPoint;
            Vector3 p2 = line1.endPoint;
            Vector3 p3 = line2.startPoint;
            Vector3 p4 = line2.endPoint;


            double dx12 = p2.X - p1.X;
            double dy12 = p2.Y - p1.Y;
            double dx34 = p4.X - p3.X;
            double dy34 = p4.Y - p3.Y;

            double denominator = (dy12 * dx34 - dx12 * dy34);
            double k1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;


            if (double.IsInfinity(k1))
            {
                return new Vector3(double.NaN, double.NaN);
            }
            result = new Vector3(p1.X + dx12 * k1, p1.Y + dy12 * k1);

            if (extended)
            {
                return result;
            }
            else
            {
                if(IsPointOnLine(line1, result)&&IsPointOnLine(line2, result))
                {
                    return result;
                }
                else
                {
                    return new Vector3(double.NaN, double.NaN);
                }
            }
        }

        private static bool IsPointOnLine(Line line1, Vector3 point)
        {
            double start2point = line1.startPoint.DistanceFrom(point);
            double point2end = line1.endPoint.DistanceFrom(point);
            double THRESH = 1e-3;
            return Math.Abs(line1.Length - start2point - point2end) < THRESH;
        }
    }
}
