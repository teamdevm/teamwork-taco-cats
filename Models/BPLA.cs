using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps.Models
{
    public class BPLA
    {
        public bool IsSelected { get; set; } = false;
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Move> CollectionOfMove { get; set; }
    }
}
