using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommonTypes;

namespace WpfApplication1.ViewModels
{
    public class KFZDisplay : KFZ, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public long PId { get { return Id;} set { Id = value; } }
        public string PFahrgestNr { get { return FahrgestNr;} set { } }
        public string PKennzeichen { get { return Kennzeichen;} set { } }
        public int PLeistung { get { return Leistung;} set { } }

        public string PTyp
        {
            get { return Typ; }

            set { this.Typ = value; notifyPropertyChanged("PTyp"); }
        }

        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; notifyPropertyChanged("Selected"); }
        }

        public KFZDisplay() : base() { }

        public KFZDisplay(KFZ kfz) // Kaffee für Herrn Gentner
        {
            this.FahrgestNr = kfz.FahrgestNr;
            this.Id = kfz.Id;
            this.Kennzeichen = kfz.Kennzeichen;
            this.Leistung = kfz.Leistung;
            this.Typ = kfz.Typ;
        }              
    }
}
