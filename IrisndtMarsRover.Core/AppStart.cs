using System;
using System.Threading.Tasks;
using IrisndtMarsRover.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace IrisndtMarsRover.Core
{
    public class AppStart : MvxAppStart
    {
         
        public static IMvxNavigationService navigation;
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
            : base(app, mvxNavigationService)
        {
            navigation = mvxNavigationService;
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<HomeViewModel>();
        }
    }
}
