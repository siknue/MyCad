using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCad.Entities
{
    public class Point
    {
        public Vector3 Position { get; set; }
        private double Thickness { get; set; }
        

        public Point()
        {
            this.Position = Vector3.Zero;
            this.Thickness = 0.0;
        }
        public Point(Vector3 position)
        {
            this.Position = position;
            this.Thickness = 0.0;
        }
    }
}
