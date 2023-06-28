using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            new double[] { 0, 0 }, // Минимальная широта и долгота
            new double[] { 0, 0 }, // Максимальная широта и долгота
        };
        public string FilePath { get; set; }

        [JsonIgnore]
        public Bitmap Image { get; set; }
    }
    


}
