using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps.Models
{
    class Move
    {
        public string ID { get; set; }
        public double[] Coordinates { get; set; } = new double[]
        {
            new double[] { 0, 0 }, // Широта и долгота
        };
        public DateTime Time { get; set; }
    }
}
