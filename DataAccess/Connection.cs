using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Mk.DBConnector;
using CommonTypes;

namespace DataAccess
{
    public static class Connection
    {
        public static event KFZListeReadyEventHandler KfzListeReady;

        internal static DBAdapter Adapter;
        static Connection()
        {
            // Testcomment
            try
            {
                Connection.Adapter = new DBAdapter(DatabaseType.MySql, Instance.NewInstance, "localhost", 3306, "kfzka", "ukfz", "ukfz", "logdatei.log");
                Connection.Adapter.Adapter.LogFile = true;
                IsConnected = true;
            }
            catch (Exception)
            {

                IsConnected = false;
            }

        }

        public static bool IsConnected { get; set; }

        //TODO: Liefert die Liste der KFZ's aus der Datenbank
        public static List<KFZ> GetKfzList()
        {
            List<KFZ> KFZListe = new List<KFZ>();

            string sql = "SELECT * FROM kfz;";

            DataTable t = Connection.Adapter.Adapter.GetDataTable(sql); //DB-Zugriff (kann dauern)

            foreach (DataRow r in t.Rows)
            {
                KFZListe.Add(new DA_KFZ(r));
            }

            //

            return KFZListe;
            
        }

        //TODO: Einfügen eines KFZ in die Datenbank
        public static void InsertKFZ(KFZ kfz)
        {
            //DA_KFZ dakfz = new DA_KFZ(kfz);
            //dakfz.Insert();
            string sqlInsertStatement = $@"INSERT INTO `kfz` (`idkfz`, `FahrgestellNr`, `Kennzeichen`, `Leistung`, `Typ`)
                VALUES (NULL, '{kfz.FahrgestNr}', '{kfz.Kennzeichen}', '{kfz.Leistung}', '{kfz.Typ}');";

            Connection.Adapter.Adapter.Insert(sqlInsertStatement);
        }

        //TODO: Ändern eines KFZ in der Datenbank
        public static void UpdateKFZ(KFZ kfz)
        {
            string sqlUpdateStatement = $@"UPDATE kfz SET FahrgestellNr='{kfz.FahrgestNr}', Kennzeichen='{kfz.Kennzeichen}', Leistung='{kfz.Leistung}', Typ='{kfz.Typ}' WHERE idkfz='{kfz.Id}';";
            Connection.Adapter.Adapter.ExecuteSQL(sqlUpdateStatement);
        }

        //TODO: Löschen eines KFZ in der Datenbank
        public static void DeleteKFZ(KFZ kfz)
        {
            string sqlDeleteStatement = $@"DELETE FROM kfz WHERE idkfz = {kfz.Id}";
            Connection.Adapter.Adapter.ExecuteSQL(sqlDeleteStatement);
        }
    }
}
