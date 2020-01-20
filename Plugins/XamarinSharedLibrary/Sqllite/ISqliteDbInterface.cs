using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSharedLibrary.Sqllite
{
    public interface ISqliteDbInterface
    {

        string GetConnection(string dbname);
    }
}
