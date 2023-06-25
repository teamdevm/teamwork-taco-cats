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

    public class CollectionMap
    {
        ObservableCollection<Map> CollectionOfMap = new ObservableCollection<Map>();
        public void Create()
        {
            string _workingDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var folderPath = $"{_workingDirectory}\\Assets\\Maps";
            using var fileStream = new FileStream(Path.Combine(folderPath, "TestImage.png"), FileMode.Open, FileAccess.Read) { Position = 0 };
            var bitmap = new Bitmap(fileStream);


            CollectionOfMap.Add(new Map
            {
                ID = "0",
                Name = "Test Map",
                Description = "Test Map",
                Coordinates = new double[][]
                {
                    new double[] { 58.074246, 54.664838 },
                    new double[] { 57.629111, 54.645612 },
                },
                FilePath = Path.Combine(folderPath, "TestImage.png"),
                Image = bitmap
            });
        }
    }


}
