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

            return _CollectionOfMap;
        }
    }
}
