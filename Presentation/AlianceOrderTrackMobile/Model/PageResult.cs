using System;
using System.Collections.Generic;
using System.Text;

namespace AlianceOrderTrackMobile.Model
{
    public class PageResult<T>
    {

        public List<T> Result { get; set; }

        public int Count { get; set; }
    }
}
