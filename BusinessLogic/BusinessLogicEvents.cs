using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    //public enum KFZStatus { NewKFZ = 0, RemovedKFZ = 1, ChangedKFZ }
    //public class KFZStatusEventArgs : EventArgs
    //{
    //    public KFZ KFZ { get; private set; }

    //    public KFZStatus Status { get; private set; }

    //    public KFZStatusEventArgs(KFZStatus status, KFZ newKFZ)
    //    {
    //        Status = status;
    //        this.KFZ = newKFZ;
    //    }
    //}

    //public delegate void KFZModelChangedEventHandler(object sender, KFZStatusEventArgs e);
    public delegate void KFZDataArrivedEventHandler(List<KFZ> kfzs);
    public delegate void KFZChangedEventHandler(KFZ kfz);
    public delegate void KFZDeletedEventHandler(KFZ kfz);
    public delegate void KFZNewEventHandler(KFZ kfz);
}
