using Maps.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps.Models
{
    internal class CreateJson
    {
        public void Create()
        {
            var map = new Map
            {
                ID = "0",
                Name = "Map 1",
                Description = "Test Map",
                Coordinates = new double[][] {
                    new double[] { 58.074246, 54.664838 },
                    new double[] { 57.629111, 54.645612 },
                    new double[] { 57.629111, 55.549237 },
                    new double[] { 58.071335, 55.565717 }
                },
                FilePath = "maps/TestImage.png"
            };

            // Сериализация объекта в JSON
            string json = JsonConvert.SerializeObject(map, Formatting.Indented);

            // Сохранение JSON в файл
            File.WriteAllText("testmap.json", json);
        }
    }
}
