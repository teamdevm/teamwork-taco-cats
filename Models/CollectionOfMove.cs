using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps.Models
{
    public class CollectionOfMove
    {
        ObservableCollection<Move> _CollectionOfMove = new ObservableCollection<Move>();

        public ObservableCollection<Move> Create()
        {
            _CollectionOfMove.Clear();



            return _CollectionOfMove;
        }
    }
}
