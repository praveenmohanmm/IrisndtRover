using System;
using System.Drawing;
using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;
using IrisndtMarsRover.Forms.UI.interfaces;
using TipCalc.Forms.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSScreenShot))]
namespace TipCalc.Forms.iOS
{
 
        public class IOSScreenShot : IScreenshotService
        {
            public byte[] Capture()
            {
                var capture = UIScreen.MainScreen.Capture();
                using (NSData data = capture.AsPNG())
                {
                    var bytes = new byte[data.Length];
                    Marshal.Copy(data.Bytes, bytes, 0, Convert.ToInt32(data.Length));
                    var compressed = ResizeImageIOS(bytes, 50, 75);
                    return compressed;
                }
            }


        public static byte[] ResizeImageIOS(byte[] imageData, float width, float height)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
            UIImageOrientation orientation = originalImage.Orientation;

            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

                RectangleF imageRect = new RectangleF(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
    
}
