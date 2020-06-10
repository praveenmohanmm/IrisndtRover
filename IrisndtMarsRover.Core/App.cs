using MvvmCross;
using MvvmCross.ViewModels;
using IrisndtMarsRover.Core;
using IrisndtMarsRover.Core.ViewModels;

namespace IrisndtMarsRover.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {

            RegisterCustomAppStart<AppStart>();
        }
    }
}
