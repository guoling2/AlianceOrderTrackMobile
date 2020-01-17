using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using XamarinSharedLibrary.Files;

namespace XamarinSharedLibrary.And.Files.PCLStorage
{
   public class AndroidFile: IFile
    {
        public string Name { get; }
        public string Path { get; }


     
        public async Task<Stream> OpenAsync(XamarinSharedLibrary.Files.FileAccess fileAccess, CancellationToken cancellationToken = default(CancellationToken))
        {


            return null;
        }

        public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task RenameAsync(string newName, NameCollisionOption collisionOption = NameCollisionOption.FailIfExists,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task MoveAsync(string newPath, NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}