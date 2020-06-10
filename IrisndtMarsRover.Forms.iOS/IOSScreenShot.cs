using System;
using System.Runtime.InteropServices;
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
                    return bytes;
                }
            }
        }
    
}
