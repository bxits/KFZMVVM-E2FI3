using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommonTypes;
using BusinessLogic.Models;
using CommandHelper;

namespace WpfApplication1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand _saveCommand;
        private ICommand _deleteSelectedCommand;
        private ICommand _newCommand;
        private ICommand _datenHolenCommand;
        private ICommand _refreshCommand;

        private void notifyPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        private KFZCollectionModel _kfzm;
        public ObservableCollection<KFZDisplay> KFZObservableCollection { get; private set; } = new ObservableCollection<KFZDisplay>();

        #region Construction



        public MainViewModel()
        {
            _kfzm = new KFZCollectionModel();

            _kfzm.KFZDataArrived += _kfzm_KFZDataArrived;
            _kfzm.KFZChanged += _kfzm_KFZChanged;
            _kfzm.KFZDeleted += _kfzm_KFZDeleted;
            _kfzm.KFZNew += _kfzm_KFZNew;
        }

        private void _kfzm_KFZNew(KFZ kfz)
        {
            throw new NotImplementedException();
        }

        private void _kfzm_KFZDeleted(KFZ kfz)
        {
            throw new NotImplementedException();
        }

        private void _kfzm_KFZChanged(KFZ kfz)
        {
            throw new NotImplementedException();
        }

        private void _kfzm_KFZDataArrived(List<KFZ> kfzs)
        {
            //Wenn Daten angekommen sind, dann erstmal die vorhandne Liste leeren...
            KFZObservableCollection.Clear();

            foreach (KFZ kfz in kfzs)
            {
                KFZDisplay kfzvm = new KFZDisplay(kfz);
                KFZObservableCollection.Add(kfzvm);
            }
        }
        #endregion

        #region Properties

        private KFZDisplay _selectedKFZ;
        public KFZDisplay SelectedKFZ
        {
            get { return _selectedKFZ; }
            set
            {
                _selectedKFZ = value;
                notifyPropertyChanged("SelectedKFZ");
            }
        }

        #endregion

        #region Commands


        //Relais-Station
        public ICommand AktualisierenCommand
        {
            get
            {
                return _saveCommand = new RelayCommand(c => SaveKfz());
            }
        }

        public ICommand NewKfzCommand
        {
            get
            {
                return _newCommand = new RelayCommand(c => NewKfz());
            }
        }

        private void NewKfz()
        {
            KFZDisplay kd = new KFZDisplay() { PId = -1, Selected = true };
            KFZObservableCollection.Add(kd);
            SelectedKFZ = kd;


        }

        public ICommand DeleteSelectedCommand
        {
            get
            {
                return _deleteSelectedCommand = new RelayCommand(c => DeleteKfz());
            }
        }

        //Wird von Oberfläche aufgerufen
        private void SaveKfz()
        {
            foreach (KFZDisplay kfzvm in KFZObservableCollection)
            {
                if (kfzvm.Selected)
                {
                    if (kfzvm.PId == -1)
                    {
                        _kfzm.Insert(kfzvm);
                    }
                    else
                    {
                        _kfzm.Update(kfzvm);
                    }
                }
            }
        }

        private void DeleteKfz()
        {
            foreach (KFZDisplay kfzvm in KFZObservableCollection)
            {
                if (kfzvm.Selected)
                {
                    _kfzm.Delete(kfzvm);
                }
            }
        }

        //...an Command binden...
        private void ShowKfzDetails()
        {

        }

        public ICommand DatenHolenCommand
        {
            get
            {
                return _datenHolenCommand = new RelayCommand(c => HoleDaten());
            }
        }

        private void HoleDaten()
        {
            _kfzm.GetAllKfz();
        }

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand = new RelayCommand(c => RefreshKFZData());
            }
        }

        private void RefreshKFZData()
        {
            _kfzm.RefreshKFZs();
        }

        #endregion

    }


}
