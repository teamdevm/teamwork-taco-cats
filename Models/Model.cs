using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace Maps.Models
{
    internal class Model
    {

    }

    public class Map
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double[][] Coordinates { get; set; } = new double[][]
        {
            new double[] { 0, 0 },
            new double[] { 0, 0 },
            new double[] { 0, 0 },
            new double[] { 0, 0 }
        };
        public string FilePath { get; set; }

        [JsonIgnore]
        public Bitmap Image { get; set; }
    }

   
}
