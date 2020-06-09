using System.Threading.Tasks;
using MvvmCross.ViewModels;
using IrisndtMarsRover.Core;
using System.Collections.Generic;

namespace IrisndtMarsRover.Core.ViewModels
{
    public class HistoryViewModel : MvxViewModel
    {
        private List<string> historydata;
        public List<string> Historydata
        {
           get
            {
                return historydata;
            }
            set { historydata = value; RaisePropertyChanged(() => Historydata); }
        }
        public HistoryViewModel()
        {

        }

        public override async Task Initialize()
        {
            await base.Initialize();
            
            Historydata.Add("helllow");
            Historydata.Add("helllow");
            Historydata.Add("helllow");
            Historydata.Add("helllow");
            Historydata.Add("helllow");
            Historydata.Add("helllow");



        }


    }
}
