using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maps.ViewModels;

namespace Maps.Models
{
    public class Data : ViewModelBase
    {
        #region Collection

        public ObservableCollection<Map> Maps { get; set; }
        public ObservableCollection<Move> Moves { get; set; }
        public ObservableCollection<BPLA> BPLAs { get; set; }

        #endregion

        #region SelectedObject

        private Map _map;
        public Map Map { get { return _map; } set { _map = value; OnPropertyChanged();} }


        private BPLA _BPLA;
        public BPLA BPLA { get { return _BPLA; } set { _BPLA = value; OnPropertyChanged();} }

        #endregion

        #region Methods

        public void FillTestData()
        {
            var maps = new CollectionOfMap();
            Maps = maps.Create();

            var move = new CollectionOfMove();
            Moves = move.Create();
        }

        private void CreateBPLA()
        {
            if (Moves != null && Moves.Any())
            {
                
            }
        }
        #endregion
    }
}
