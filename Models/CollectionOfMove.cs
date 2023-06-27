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

            _CollectionOfMove.Add(new Move { ID = "1", Coordinates = new double[] { 58.00711, 56.18835 }, Time = new DateTime(2023, 6, 20, 18, 30, 25) });
            _CollectionOfMove.Add(new Move { ID = "1", Coordinates = new double[] { 58.01587, 56.24571 }, Time = new DateTime(2023, 6, 20, 18, 35, 25) });

            _CollectionOfMove.Add(new Move { ID = "2", Coordinates = new double[] { 58.05427, 56.41754 }, Time = new DateTime(2023, 6, 20, 18, 30, 25) });
            _CollectionOfMove.Add( new Move { ID = "2", Coordinates = new double[] { 58.06807, 56.55899 }, Time = new DateTime(2023, 6, 20, 18, 35, 46) });

            return _CollectionOfMove;
        }
    }
}
