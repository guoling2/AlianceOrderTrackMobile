using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace XamarinSharedLibrary.Sqllite
{
    public abstract class TmsSqLiteModel 
    {

        //public abstract string Id { get; }

        //[Ignore]
        //public abstract string Id { get; }
        // public abstract string GetId();
    }


    public interface ITmsSqLiteModelId
    {

          string GetId();
    }
}
