//using Microsoft.Data.Sqlite;
//using System.Linq;
//namespace XamarinSharedLibrary.Sqllite
//{
//    public  class TmsLocalDbContext
//    {

//       // private  SqLiteHelper database;


//        public SqLiteHelper SqLite { get; private set; }


//        public TmsLocalDbContext()
//        {
//            SqLite=CreateTmsLocalDb();
//        }


//        public T GetSingel<T>(object id,string table, string primarykey) where T:new()
//        {

//           var columns= typeof(T).GetProperties().Select(t => t.Name);


//            var objec = new T();

//            string sql = $"select * from {table} where {primarykey}= '{id}'";




//            var sqlreader= SqLite.ExecuteQuery(sql);

//            if (sqlreader.HasRows)
//            {
//                foreach (var column in columns)
//                {
//                    typeof(T).GetProperty(column)?.SetValue(sqlreader[column], objec);
//                }
//            }

//            return objec;
//        }
//        //static SQLiteAsyncConnection database2;

//        //public static SQLiteAsyncConnection CreateTmsLocalDb2()
//        //{

//        //    if (database2 == null)
//        //    {
//        //        var sdb = Xamarin.Forms.DependencyService.Get<ISqliteDbInterface>();

//        //        var pathx = sdb.GetConnection("TmsLocalDB");
//        //        database2 = new SQLiteAsyncConnection(pathx);

//        //    }
//        //    return database2;
//        //}
//        private  SqLiteHelper CreateTmsLocalDb()
//        {


//            var sdb = Xamarin.Forms.DependencyService.Get<ISqliteDbInterface>();

//            var pathx = sdb.GetConnection("TmsLocalDB");
//            if (SqLite == null)
//            {
//                SqLite = new SqLiteHelper(pathx);
//            }
//            return SqLite;

//        }
//    }
//}
