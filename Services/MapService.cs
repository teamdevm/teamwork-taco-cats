using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Maps.Models;
using Newtonsoft.Json;

namespace Maps.Services
{
    public class MapService
    {
        readonly string _workingDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        public async Task<IEnumerable<Map>> GetMap(int pageIndex)
        {

            var folderPath = $"{_workingDirectory}\\Maps\\Region{pageIndex}";

            //var folderPath = $"{_workingDirectory}\\Maps\\Region{pageIndex}";
            var dataFile = $"page{pageIndex}.json";
            var imageFolder = Path.Combine(folderPath, "Images");
            List<Map> items;

            //read data
            using (var r = new StreamReader(Path.Combine(folderPath, dataFile)))
            {
                var json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Map>>(json);
            }

            //load images
            foreach (var item in items)
            {
                var imagePath = Path.Combine(imageFolder, $"{item.Name}.png");
                item.Image = await GetPoster(imagePath);
            }

            return items;
        }

        private Task<Bitmap> GetPoster(string posterUrl)
        {
            return Task.Run(() =>
            {
                using var fileStream = new FileStream(posterUrl, FileMode.Open, FileAccess.Read) { Position = 0 };
                var bitmap = new Bitmap(fileStream);
                return bitmap;
            });
        }
    }
}
