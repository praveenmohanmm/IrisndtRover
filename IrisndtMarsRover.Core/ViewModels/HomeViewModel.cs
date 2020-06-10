using System.Threading.Tasks;
using MvvmCross.ViewModels;
using IrisndtMarsRover.Core;
using MvvmCross.Navigation;

namespace IrisndtMarsRover.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public HomeViewModel()
        {
            
        }

        public override async Task Initialize()
        {
            await base.Initialize();

           
        }

      
    }
}
