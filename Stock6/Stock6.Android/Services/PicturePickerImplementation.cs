using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Stock6.Droid.Services;
using Stock6.Models;
using Stock6.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

[assembly: Dependency(typeof(PicturePickerImplementation))]
namespace Stock6.Droid.Services
{
   public class PicturePickerImplementation: IPicturePicker
    {

        public Task<List<ImageModel>> GetImageStreamAsync()
        {
            //Intent intent = new Intent();
            //intent.SetType("image/*");
            //intent.SetAction(Intent.ActionGetContent);
            //intent.PutExtra(Intent.ExtraAllowMultiple, true);
            //intent.PutExtra(Intent.ExtraChosenComponent, true);
            //intent.PutExtra(Intent.ExtraChooserTargets, true);
            //var a = intent.Data;
            //// Start the picture-picker activity (resumes in MainActivity.cs)
            //MainActivity.Instance.StartActivityForResult(
            //    Intent.CreateChooser(intent, "Select"),
            //    MainActivity.PickImageId);

            //// Save the TaskCompletionSource object as a MainActivity property
            //MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();
            return Task.Run(() =>
            {
                Android.Net.Uri mImageUri = MediaStore.Images.Media.ExternalContentUri;
                ContentResolver mContentResolver = MainActivity.Instance.ContentResolver;
                ICursor mCursor = mContentResolver.Query(mImageUri, new string[]{
                                MediaStore.Images.Media.InterfaceConsts.Data,
                                MediaStore.Images.Media.InterfaceConsts.DisplayName,
                                MediaStore.Images.Media.InterfaceConsts.DateAdded,
                                MediaStore.Images.Media.InterfaceConsts.Id,
                                MediaStore.Images.Media.InterfaceConsts.MimeType},
                            null,
                            null,
                            MediaStore.Images.Media.InterfaceConsts.DateAdded+" DESC");
                List<ImageModel> images = new List<ImageModel>();
                while (mCursor.MoveToNext())
                {
                    string path = mCursor.GetString(
                            mCursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Data));
                    string name = mCursor.GetString(
                            mCursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.DisplayName));
                    long time = mCursor.GetLong(
                            mCursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.DateAdded));
                    string mimetype = mCursor.GetString(
                           mCursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.MimeType));
                    images.Add(new ImageModel(path, time, name, mimetype));
                }
                mCursor.Close();
                // Return Task object
                return images;
            });
        }
    }
}