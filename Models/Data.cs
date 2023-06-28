using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maps.ViewModels;
using Nito.AsyncEx;

namespace Maps.Models
{
    public class Data : ViewModelBase
    {


        public Data()
        {
            InitializationNotifier = NotifyTaskCompletion.Create(InitializeAsync());
        }
        public INotifyTaskCompletion InitializationNotifier { get; private set; }

        private async Task InitializeAsync()
        {

        }
        #region Collection

        private ObservableCollection<Map> _maps = new ObservableCollection<Map>();

        public virtual ObservableCollection<Map> Maps
        {
            get => _maps;
            set
            {
                _maps = value;
                OnPropertyChanged();
            }
        }

        public virtual ObservableCollection<Move> Moves { get; set; } = new ObservableCollection<Move>();


        private ObservableCollection<BPLA> _bpla = new ObservableCollection<BPLA>();

        public virtual ObservableCollection<BPLA> BPLAs
        {
            get => _bpla;
            set
            {
                _bpla = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region SelectedObject

        private int _SelectedIndexMap = 0;
        public virtual int SelectedIndexMap
        {
            get => _SelectedIndexMap;
            set
            {
                _SelectedIndexMap = value;
                ChangeSelectedMap();
                OnPropertyChanged();
            }
        }

        private Map _map;
        public virtual Map SelectedMap { get { return _map; } set { _map = value; OnPropertyChanged();} }


        private BPLA _BPLA;
        public BPLA SelectedBPLA { get { return _BPLA; } set { _BPLA = value; OnPropertyChanged();} }

        #endregion

        #region Methods

        public void FillTestData()
        {
            var maps = new CollectionOfMap();
            Maps = maps.Create();
            ChangeSelectedMap();

            var move = new CollectionOfMove();
            Moves = move.Create();

            CreateBPLACollection();
        }
        public async Task FillTestDataAsync()
        {
            var maps = new CollectionOfMap();
            Maps = new ObservableCollection<Map>(await maps.CreateAsync());
            ChangeSelectedMap();

            var move = new CollectionOfMove();
            Moves = move.Create();

            CreateBPLACollection();

            return;
        }
        private void CreateBPLACollection()
        {
            if (BPLAs == null)
            {
                BPLAs = new ObservableCollection<BPLA>();
            }


            if (Moves != null && Moves.Any())
            {
                foreach (var move in Moves)
                {
                    var tempBPLA = BPLAs.FirstOrDefault(o => o.ID == move.ID);
                    if (tempBPLA != null)
                    {
                        tempBPLA.CollectionOfMove.Add(move);
                    }
                    else
                    {
                        var newBPLA = new BPLA()
                        {
                            ID = move.ID,
                            Description = "",
                            Name = move.ID,
                            CollectionOfMove = new ObservableCollection<Move>(){move}
                        };
                        BPLAs.Add(newBPLA);
                    }
                }
            }
        }

        private void ChangeSelectedMap()
        {
            if (Maps.Any() && SelectedIndexMap <= Maps.Count )
                SelectedMap = Maps[SelectedIndexMap];
        }

        private bool IsPointOnMap(Move point, Map map)
        {
            double pointLatitude = point.Coordinates[0], pointLongitude = point.Coordinates[1];
            double[] lt, lb, rt, rb; // lt = лево верх, lb = лево низ, rt = право верх, rb = право низ
            rt = map.Coordinates[0];
            lt = map.Coordinates[1];
            lb = map.Coordinates[2];
            rb = map.Coordinates[3];

            double areaTriangle1, areaTriangle2, areaTriangle3, areaTriangle4;
            // area = 1/2 *( (x2-x1)*(y3-y1) - (x3-x1)*(y2-y1) ), где x3,y3 - координаты точки
            areaTriangle1 = Math.Abs((rt[0] * rb[1] - rt[1] * rb[0]) + (rb[0] * pointLongitude - rb[1] * pointLatitude) +
                pointLatitude * rt[1] - pointLongitude * rt[0]) / 2;
            areaTriangle2 = Math.Abs((rb[0] * lb[1] - rb[1] * lb[0]) + (lb[0] * pointLongitude - lb[1] * pointLatitude) +
                pointLatitude * rb[1] - pointLongitude * rb[0]) / 2;
            areaTriangle3 = Math.Abs((lb[0] * lt[1] - lb[1] * lt[0]) + (lt[0] * pointLongitude - lt[1] * pointLatitude) +
                pointLatitude * lb[1] - pointLongitude * lb[0]) / 2;
            areaTriangle4 = Math.Abs((lt[0] * rt[1] - lt[1] * rt[0]) + (rt[0] * pointLongitude - rt[1] * pointLatitude) +
                pointLatitude * lt[1] - pointLongitude * lt[0]) / 2;

            // area = 1/2 *( (x1-x2)*(y1+y2) + (x2-x3)*(y2+y3) + (x3-x4)*(y3+y4) + (x4-x1)*(y4+y1) )
            double areaQuadrangle = Math.Abs((rt[0] * rb[1] - rt[1] * rb[0]) + (rb[0] * lb[1] - rb[1] * lb[0]) +
                (lb[0] * lt[1] - lb[1] * lt[0]) + (lt[0] * rt[1] - lt[1] * rt[0])) / 2;

            return ((areaQuadrangle - (areaTriangle1 + areaTriangle2 + areaTriangle3 + areaTriangle4)) < 1e-20);
        }
        #endregion
    }
}
