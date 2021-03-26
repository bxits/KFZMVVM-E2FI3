using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

using CommonTypes;
using DataAccess;

namespace BusinessLogic.Models
{
    public class KFZCollectionModel
    {
        //Event nach außen publizieren.
        public event KFZDataArrivedEventHandler KFZDataArrived;
        public event KFZChangedEventHandler KFZChanged;
        public event KFZDeletedEventHandler KFZDeleted;
        public event KFZNewEventHandler KFZNew;

        private List<KFZ> _kFZListe = new List<KFZ>();
        BackgroundWorker _bwt;

        public KFZCollectionModel()
        {
            //Tür auf! Für das Event registrieren.
            Connection.KfzListeReady += Connection_KfzListeReady;

        }

        private void Connection_KfzListeReady(List<KFZ> kfzs)
        {
            _kFZListe = kfzs;

            KFZDataArrived(_kFZListe);
        }

        public void GetAllKfz()
        {
            //Alle KFZ aus der Datenquelle abholen und in der Liste speichern:

            //jetzt über Events...
            //this.KFZListe = Connection.GetKfzList();
            Connection.GetKfzList();
        }

        public void Insert(KFZ kfz)
        {
            //Überprüfen, ob das neue Kfz korrekte Werte besitzt.
            if (kfz.Id == -1 &&
                kfz.FahrgestNr != string.Empty &&
                kfz.Kennzeichen != string.Empty &&
                kfz.Leistung > 0
                && kfz.Typ != string.Empty)
            {
                Connection.InsertKFZ(kfz);
            }
        }

        public void Update(KFZ kfz)
        {
            Connection.UpdateKFZ(kfz);
        }

        public void Delete(KFZ kfz)
        {
            //1. KFZ abmelden bei Zulassungsstelle
            //2. Fahrzeughalter benachrichtigen, dass KFZ abgemeldet wurde.
            //3. auf Bestätigung des Halters warten
            //4. usw.
            Connection.DeleteKFZ(kfz);
        }

        public void StartAutoRefreshThread()
        {
            //async / await / Task... geht auch

            //BackgroundWorker
            _bwt = new BackgroundWorker();
            _bwt.DoWork += _bwt_RefreshKFZs;
            _bwt.WorkerSupportsCancellation = true;
           
            _bwt.RunWorkerAsync();
        }

        
        //Wird vom neuen Thread (_bwt) aufgerufen.
        public void _bwt_RefreshKFZs(object sender, DoWorkEventArgs e) //Jetzt die Thread-Methode (blauer BwThread)
        {
            List<KFZ> neueListeAusDB = Connection.GetKfzList();
            List<KFZ> kfznichtmehrdrin = new List<KFZ>();

            while (true) //Endlosschleife des Threads
            {
                foreach (KFZ k in neueListeAusDB)
                {
                    if (!_kFZListe.Contains(k))
                    {
                        _kFZListe.Add(k);
                        if (KFZNew != null)
                            KFZNew(k); //Event feuern.
                    }
                }

                foreach (KFZ k in _kFZListe)
                {
                    if (!neueListeAusDB.Contains(k))
                    {
                        //KFZ aus Liste entfernen
                        int idx = _kFZListe.IndexOf(k);
                        _kFZListe.RemoveAt(idx);

                        if (KFZDeleted != null)
                            KFZDeleted(k);
                    }
                }

                foreach (KFZ k in _kFZListe)
                {
                    if (neueListeAusDB.Contains(k))
                    {
                        int i = neueListeAusDB.IndexOf(k);

                        if (k.Typ != neueListeAusDB[i].Typ)
                        {
                            if (KFZChanged != null)
                                KFZChanged(k);
                        }

                        //alle anderen Props auch checken...

                    }
                }

                System.Threading.Thread.Sleep(5000);
            }
        }

    }
}
