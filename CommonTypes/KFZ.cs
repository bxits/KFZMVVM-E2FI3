using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using System;
using System.Xml.Serialization;

namespace CommonTypes
{
    // TODO: Diese Klasse muss das Interface System.IEquatable implementieren,
    // um in Collections die generischen Funktionen nutzen zu können
    public class KFZ
    {
        public long Id;
        public string FahrgestNr;
        public string Kennzeichen;
        public int Leistung;
        public string Typ;

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Kennzeichen, this.Typ);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            KFZ objAsPart = obj as KFZ;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public override int GetHashCode()
        {
            return Kennzeichen.GetHashCode();
        }

        public bool Equals(KFZ k)
        {
            if (k.Kennzeichen != this.Kennzeichen) return false;
            return this.Kennzeichen.Equals(k.Kennzeichen);
        }
    }
}
