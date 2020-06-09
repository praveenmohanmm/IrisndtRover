using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using IrisndtMarsRover.Core;
using IrisndtMarsRover.Forms.UI;


namespace IrisndtMarsRover.Forms.Droid
{
    [Activity(
        Label = "IrisndtMarsRover.Forms.Droid",
        Icon = "@drawable/icon",
        Theme = "@style/MyTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<App, FormsApp>, App, FormsApp>
    {
        protected override void OnCreate(Bundle bundle)
        {
            
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
        }
    }
}
