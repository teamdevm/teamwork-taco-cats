using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps.Models
{
    public class BPLA : INotifyPropertyChanged
    {
        private bool _IsSelected = false;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value; 
                OnPropertyChanged(nameof(IsSelected));

            }
        }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Move> CollectionOfMove { get; set; } = new ObservableCollection<Move>();

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
