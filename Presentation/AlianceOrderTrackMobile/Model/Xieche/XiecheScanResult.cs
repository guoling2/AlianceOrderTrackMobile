using System;
using System.Collections.Generic;
using System.Text;

namespace AlianceOrderTrackMobile.Model.Xieche
{
    public class XiecheScanResult
    {
        public string XieCheId {
            get;
            set;
        }
       public string XieCheCode
       {
           get;
           set;
       }
        public decimal OrderCount
        {
            get;
            set;
        }
        public int PlanStatuedId
        {
            get;
            set;
        }
        public string ActionStoreId
        {
            get;
            set;
        }
    }
}
