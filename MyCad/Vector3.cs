﻿using System;

namespace MyCad
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0.0;
        }

        public Vector3(double x, double y, double z) : this(x, y)
        {
            Z = z;
        }

        public System.Drawing.PointF ToPointF
        {
            get
            {
                return new System.Drawing.PointF((float)X, (float)Y);
            }
        }

        public static Vector3 Zero
        {
            get { return new Vector3(0.0, 0.0, 0.0); }
        }

        public double DistanceFrom(Vector3 v)
        {
            double dx = v.X - X;
            double dy = v.Y - Y;
            double dz = v.Z - Z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}
