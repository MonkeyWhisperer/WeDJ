using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeDJ.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        public string filepath = "";
        public bool isFromCamera = false;
        public AccountPage()
        {
            InitializeComponent();
            Title = "Profile";
            var ignore = taskFinishLoading();
            BackgroundColor = Color.White;
        }

        async Task taskFinishLoading()
        {
            MainStack1.IsVisible = false;
            loading.IsVisible = true;
            loading.IsRunning = true;
            loading.IsEnabled = true;

            await Task.Run(async () =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                });
           
            });
            MainStack1.IsVisible = true;
            loading.IsVisible = false;
            loading.IsRunning = false;
            loading.IsEnabled = false;
        }



        private async void ChangeImage(object sender, EventArgs e)
        {

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync
                                                     (Plugin.Permissions.Abstractions.Permission.Camera);

                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    var result = await CrossPermissions.Current.RequestPermissionsAsync(new[] {
                                                                                  Plugin.Permissions.Abstractions.Permission.Camera });
                    status = result[Plugin.Permissions.Abstractions.Permission.Camera];
                }

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {

                    var file = new MediaFile("", null, null, null);

                    file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { DefaultCamera = CameraDevice.Front, PhotoSize = PhotoSize.Custom, CustomPhotoSize = 20, CompressionQuality = 90 });

                    if (file != null)
                    {
                        var stream = file.GetStream();
                        var bytes = new byte[stream.Length];

                        using (var memoryStream = new MemoryStream())
                        {

                            file.GetStream().CopyTo(memoryStream);

                            ProfileImage.Source = ImageSource.FromFile(file.Path);

                            var guid = Guid.NewGuid().ToString();

                            UploadImage(file.Path, "Your_CDN", "Your_CDN_Folder", false, guid, file.GetStream());

                            filepath = guid + ".jpg";
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Camera Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

            }

        }



        public string UploadImage(string localImagePath, string containerName, string containerBlocks, bool deleteAfter, string fileName, Stream source)
        {
            var imageUrl = string.Empty;
            imageUrl = UploadFile(localImagePath, containerName, containerBlocks, deleteAfter, fileName, source);
            return imageUrl;
        }

        private string UploadFile(string localImagePath, string containerName, string containerBlocks, bool deleteAfter, string fileName, Stream source)
        {
            // return string with full image path
            var imagePath = string.Empty;
            var AzureStorageConnection = "Your_CDN_url";

            // check azure connection string
            if (string.IsNullOrEmpty(AzureStorageConnection))
            {
                throw new System.Exception("Azure connection string is undefined!");
            }

            // Get a reference to the storage account
            CloudStorageAccount storage = CloudStorageAccount.Parse(AzureStorageConnection);

            // Create blob client 
            CloudBlobClient blob = storage.CreateCloudBlobClient();

            // Get a reference to the container (creating it if necesary)
            CloudBlobContainer container = blob.GetContainerReference(containerName);
            container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container, new BlobRequestOptions(), new OperationContext());

            // upload file
            imagePath = UploadBlob(container, localImagePath, deleteAfter, fileName, source, containerBlocks);

            return imagePath;
        }

        private string UploadBlob(CloudBlobContainer container, string localFilePath, bool deleteAfter, string fileName, Stream source, string containerPath = "")
        {
            // generate file name
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Guid.NewGuid().ToString("N").ToLower() + Path.GetExtension(localFilePath);
            }
            else
            {
                fileName = fileName + Path.GetExtension(localFilePath);
            }

            // getting blob
            CloudBlockBlob blob = container.GetBlockBlobReference(containerPath + @"/" + fileName);

            // uploading blob
            blob.UploadFromStreamAsync(source);

            return blob.Uri.OriginalString;
        }
    }
}