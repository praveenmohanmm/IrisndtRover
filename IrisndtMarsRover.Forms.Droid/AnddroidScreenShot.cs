using System;
using System.IO;
using Android.App;
using Android.Graphics;
using IrisndtMarsRover.Forms.Droid;
using IrisndtMarsRover.Forms.UI.interfaces;
using TipCalc.Forms.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidScreenShot))]
namespace TipCalc.Forms.Droid
{
  
    public class AndroidScreenShot : IScreenshotService
    {
        public static Activity _currentActivity;
        public void SetActivity(Activity activity) => _currentActivity = activity;

        public byte[] Capture()
        {
            var rootView = _currentActivity.Window.DecorView.RootView;

            using (var screenshot = Bitmap.CreateBitmap(
                                    rootView.Width,
                                    rootView.Height,
                                    Bitmap.Config.Argb8888))
            {
                var canvas = new Canvas(screenshot);
                rootView.Draw(canvas);

                using (var stream = new MemoryStream())
                {
                    screenshot.Compress(Bitmap.CompressFormat.Png, 90, stream);
                    var compressed = ResizeImageAndroid(stream.ToArray(), 50, 75);
                    return compressed;
                }
            }
        }


        public static byte[] ResizeImageAndroid(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }
    }
}
