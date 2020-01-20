using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using XamarinSharedLibrary.Sqllite;


namespace TmsBuinessCommonLibrary.Model
{
    [Table("BroadcastConfig")]
    public class BroadcastConfigModel: TmsSqLiteModel
    {
        [PrimaryKey]
        public string MachineId { get; set; }

        public string FileActionName { get; set; }

        public string BarCodeName { get; set; }

        public override string ToString()
        {
            return MachineId;
        }

        // public override string Id => MachineId;
    }
}
