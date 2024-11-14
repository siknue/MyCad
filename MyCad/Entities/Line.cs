using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyCad;

namespace MyCad.Entities
{
    public class Line
    {
        public Vector3 startPoint { get; set; }
        public Vector3 endPoint { get; set; }
        public double Thickness { get; set; }


        public Line(Vector3 start, Vector3 end)
        {
            this.startPoint = start;
            this.endPoint = end;
            this.Thickness = 0.0;
        }

        public Line() : this(Vector3.Zero, Vector3.Zero) { }
    }
}
