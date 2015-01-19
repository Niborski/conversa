// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Conversa.Net.Xmpp.Storage
{
    public abstract class SecureFileStorage<T>
        where T: class, new()
    {
        protected static readonly BinaryStringEncoding Encoding = BinaryStringEncoding.Utf8;

        private static StorageFolder GetFolder(StorageFolderType folderType)
        {
            try
            {
                switch (folderType)
                {
                    case StorageFolderType.Local:
                        return ApplicationData.Current.LocalFolder;

                    case StorageFolderType.Roaming:
                        return ApplicationData.Current.RoamingFolder;

                    case StorageFolderType.Temporary:
                        return ApplicationData.Current.TemporaryFolder;
                }
            }
            catch (InvalidOperationException)
            {
                Debug.WriteLine("Physical storage not available");
            }

            return null;
        }

        private StorageFolder folder;
        private string        filename;
        private string        protectionDescriptor;

        protected SecureFileStorage(StorageFolderType folderType, string filename, DataProtectionType protectionType)
        {
            this.folder   = GetFolder(folderType);
            this.filename = filename;

            if (protectionType == DataProtectionType.User)
            {
                this.protectionDescriptor = "LOCAL=user";
            }
            else
            {
                this.protectionDescriptor = "LOCAL=machine";
            }
        }

        public async Task<bool> IsEmptyAsync()
        {
            return (this.folder == null || await this.folder.GetFileAsync(this.filename) == null);
        }

        public virtual async Task<T> LoadAsync()
        {
            if (this.folder == null)
            {
                Debug.WriteLine("Physical storage not available");
                return null;
            }

            StorageFile file   = await this.folder.GetFileAsync(this.filename);
            IBuffer     buffer = await FileIO.ReadBufferAsync(file);

            return this.OnDataLoaded(await this.UnprotectAsync(buffer).ConfigureAwait(false));
        }

        public virtual async void SaveAsync(T data)
        {
            if (this.folder == null)
            {
                Debug.WriteLine("Physical storage not available");
                return;
            }

            StorageFile file = await folder.CreateFileAsync(this.filename, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteBufferAsync(file, await this.ProtectAsync(data));
        }

        protected async Task<IBuffer> ProtectAsync(T data)
        {
            // Create a DataProtectionProvider object for the specified descriptor.
            DataProtectionProvider provider = new DataProtectionProvider(this.protectionDescriptor);

            // Encode the input data to a buffer.
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(this.OnSerializeData(data), Encoding);

            // Encrypt the message.
            IBuffer buffProtected = await provider.ProtectAsync(buffer);

            // Execution of the ProtectAsync function resumes here
            // after the awaited task (Provider.ProtectAsync) completes.
            return buffProtected;
        }

        protected async Task<String> UnprotectAsync(IBuffer buffProtected)
        {
            // Create a DataProtectionProvider object.
            DataProtectionProvider provider = new DataProtectionProvider();

            // Decrypt the protected message specified on input.
            IBuffer buffer = await provider.UnprotectAsync(buffProtected);

            // Execution of the UnprotectAsync method resumes here
            // after the awaited task (Provider.UnprotectAsync) completes
            // Convert the unprotected message from an IBuffer object to a string.
            return CryptographicBuffer.ConvertBinaryToString(Encoding, buffer);
        }

        protected abstract string OnSerializeData(T data);
        protected abstract T OnDataLoaded(string data);
    }
}
