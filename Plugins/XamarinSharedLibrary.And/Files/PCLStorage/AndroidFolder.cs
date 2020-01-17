using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSharedLibrary.And.Files.PCLStorage;
using XamarinSharedLibrary.Files;

[assembly: Dependency(typeof(AndroidFolder))]
namespace XamarinSharedLibrary.And.Files.PCLStorage
{
    public class AndroidFolder    : IFolder
    {


        public string AppRootFolderGet()
        {

            return Android.OS.Environment.ExternalStorageDirectory.Path;
        }

        public async Task<string> CreateFolderAsync(string desiredName, CreationCollisionOption option,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            
            if (string.IsNullOrWhiteSpace(desiredName))
            {

                throw new Exception("没有找到初始化路径");
            }


            //if (!string.IsNullOrWhiteSpace(desiredName))
            //{
            //    mainpath += "/" + desiredName;
            //}

         //  var p= Android.OS.Environment.ExternalStorageDirectory.Path + "/Sign";
            Java.IO.File file = new Java.IO.File(desiredName);

            if (!file.Exists())
            {
                var maresult = file.Mkdirs();
            }

            if (file.Exists())
            {
                return desiredName;
            }
            else
            {
                return "";
            }
        }

        public Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<ExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}