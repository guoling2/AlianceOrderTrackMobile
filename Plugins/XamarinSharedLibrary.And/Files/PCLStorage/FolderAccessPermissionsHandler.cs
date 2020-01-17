using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Plugin.Permissions;

namespace XamarinSharedLibrary.And.Files.PCLStorage
{
   public class FolderAccessPermissionsHandler
    {
        private static TaskCompletionSource<bool> requestCompletion = (TaskCompletionSource<bool>)null;

        public static Task PermissionRequestTask
        {
            get
            {
                TaskCompletionSource<bool> requestCompletion = FolderAccessPermissionsHandler.requestCompletion;
                return (requestCompletion != null ? (Task)requestCompletion.Task : (Task)null) ?? Task.CompletedTask;
            }
        }


        public static void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (FolderAccessPermissionsHandler.requestCompletion == null || FolderAccessPermissionsHandler.requestCompletion.Task.IsCompleted)
                return;
            bool result = true;
            foreach (Permission grantResult in grantResults)
            {
                if (grantResult == Permission.Denied)
                {
                    result = false;
                    break;
                }
            }
            FolderAccessPermissionsHandler.requestCompletion.TrySetResult(result);
        }
        public static void CheckPermissoions()
        {

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                var status = CrossPermissions.Current.CheckPermissionStatusAsync
                    (Plugin.Permissions.Abstractions.Permission.Storage).GetAwaiter().GetResult();
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    // Console.WriteLine("Currently does not have Location permissions, requesting permissions");

                    //var request = CrossPermissions.Current.RequestPermissionsAsync
                    //    (Plugin.Permissions.Abstractions.Permission.Location).GetAwaiter().GetResult();



                    ActivityCompat.RequestPermissions(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, new string[]
                    {
                        Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage
                    }, 1);



                }
            }
        }

    }
}