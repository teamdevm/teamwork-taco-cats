using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace Maps.Models
{
    public class CollectionOfMap
    {
        private ObservableCollection<Map> _CollectionOfMap = new ObservableCollection<Map>();

        public ObservableCollection<Map> Create()
        {
            
            _CollectionOfMap.Clear();

            string _workingDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var folderPath = $"{_workingDirectory}\\Assets\\Maps";

            using var fileStream = new FileStream(Path.Combine(folderPath, "TestImage.png"), FileMode.Open, FileAccess.Read) { Position = 0 };
            var bitmap = new Bitmap(fileStream);


            _CollectionOfMap.Add(new Map
            {
                ID = "0",
                Name = "Test Image",
                Description = "Test Image",
                Coordinates = new double[][]
                {
                    new double[] { 58.074246, 54.664838 },
                    new double[] { 57.629111, 54.645612 },
                },
                FilePath = Path.Combine(folderPath, "TestImage.png"),
                Image = bitmap
            });


            using var fileStream1 = new FileStream(Path.Combine(folderPath, "TestMap1.png"), FileMode.Open, FileAccess.Read) { Position = 0 };
            bitmap = new Bitmap(fileStream1);
            _CollectionOfMap.Add(new Map
            {
                ID = "1",
                Name = "Test Map 1",
                Description = "Test Map 1",
                Coordinates = new double[][]
                {
                    new double[]{ 58.068127, 56.559066 },
                    new double[] { 58.080616, 56.416245 },
                    new double[] {58.033572, 56.416417},
                    new double[] { 58.033663, 56.584301 }
                },
                FilePath = Path.Combine(folderPath, "TestMap1.png"),
                Image = bitmap
            });

            using var fileStream2 = new FileStream(Path.Combine(folderPath, "TestMap2.png"), FileMode.Open, FileAccess.Read) { Position = 0 };
            bitmap = new Bitmap(fileStream2);
            _CollectionOfMap.Add(new Map
            {
                ID = "2",
                Name = "Test Map 2",
                Description = "Test Map",
                Coordinates = new double[][]
                {
                    new double[]{ 58.023742,56.258050 },
                    new double[] { 58.022937,56.177369 },
                    new double[] {57.99855,56.1771110},
                    new double[] { 57.999051, 56.258479 }
                },
                FilePath = Path.Combine(folderPath, "TestMap2.png"),
                Image = bitmap
            });

            return _CollectionOfMap;
        }
    }
}
