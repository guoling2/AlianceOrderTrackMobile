using System;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Runtime;

namespace XamarinSharedLibrary.And.Files.PCLStorage
{
    class TmsLocalSQLiteOpenHelper: SQLiteOpenHelper
    {
        public TmsLocalSQLiteOpenHelper(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public TmsLocalSQLiteOpenHelper(Context context, string name, SQLiteDatabase.ICursorFactory factory, int version) : base(context, name, factory, version)
        {
        }

        public TmsLocalSQLiteOpenHelper(Context context, string name, SQLiteDatabase.ICursorFactory factory, int version, IDatabaseErrorHandler errorHandler) : base(context, name, factory, version, errorHandler)
        {
        }


        public override void OnCreate(SQLiteDatabase db)
        {
            throw new NotImplementedException();
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}