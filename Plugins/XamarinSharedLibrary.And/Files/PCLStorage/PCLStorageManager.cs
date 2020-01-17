using Android.Content;

namespace XamarinSharedLibrary.And.Files.PCLStorage
{
   public class PCLStorageManager
    {
        public static void Init(Context context)
        {
            //Implementation= new AMapLocationClient(context);

            FolderAccessPermissionsHandler.CheckPermissoions();

          //  Xamarin.Forms.DependencyService.Register<IAMapLocationService, MapLocationService>();
        }
    }
}