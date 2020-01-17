using System;
using System.Threading;
using System.Threading.Tasks;
using XamarinSharedLibrary.Files;

namespace XamarinSharedLibrary.And.Files.PCLStorage
{
   public class AndroidFileSystem  : IFileSystem
    {
        public IFolder LocalStorage { get; }
        public IFolder RoamingStorage { get; }
        public Task<IFile> GetFileFromPathAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IFolder> GetFolderFromPathAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}