using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using CommonTypes;

namespace DataAccess
{
    public class DA_KFZ : KFZ
    {
        public DA_KFZ() : base() {}

        public DA_KFZ(KFZ kfz) : base()
        {
            this.Id = kfz.Id;
            this.FahrgestNr = kfz.FahrgestNr;
            this.Kennzeichen = kfz.Kennzeichen;
            this.Leistung = kfz.Leistung;
            this.Typ = kfz.Typ;
        }

        internal DA_KFZ(DataRow r)
        {
            this.Id = long.Parse(r[0].ToString());
            this.FahrgestNr = r[1].ToString();
            this.Kennzeichen = r[2].ToString();
            this.Leistung = int.Parse(r[3].ToString());

            this.Typ = r[4].ToString();
        }

        internal void Insert()
        {
            // TODO: Insert-SQL zum Einfügen der Instanz in die Datenbank

            //this.Id = Connection.Adapter.Adapter.Insert(sql);
        }

        internal void Update()
        {
            // TODO: Update-SQL zum Ändern der Instanz in der Datenbank
            
            //Connection.Adapter.Adapter.ExecuteSQL(sql);
        }

        internal void Delete()
        {
            // TODO: Insert-SQL zum Löschen der Instanz aus der Datenbank
            
            //Connection.Adapter.Adapter.ExecuteSQL(sql);
        }
    }
}
