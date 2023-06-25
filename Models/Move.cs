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
        /// <summary>
        /// Широта и долгота
        /// </summary>
        public double[] Coordinates { get; set; } = new double[] { 0, 0 }; 
        public DateTime Time { get; set; }
    }
}
