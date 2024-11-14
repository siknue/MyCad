using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCad.Entities
{
    public class Circle
    {
        public Vector3 vector3 { get; set; }
        public double radius {  get; set; }
        public double thickness {  get; set; }


        public double Diameter
        {
            get { return this.radius * 2.0; }
        }

        public Circle():this(Vector3.Zero, 1.0) { }

        public Circle(Vector3 center, double radius) 
        { 
            this.vector3 = center;
            this.radius = radius;
            this.thickness = 0.0;
        
        }

    }
}
