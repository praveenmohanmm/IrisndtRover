using System.Threading.Tasks;
using MvvmCross.ViewModels;
using IrisndtMarsRover.Core;
using System.Collections.Generic;
using MvvmCross.Navigation;

namespace IrisndtMarsRover.Core.ViewModels
{
    public class HistoryItem
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }

    public class HistoryViewModel : MvxViewModel
    {
        private List<HistoryItem> historydata;
        public List<HistoryItem> Historydata
        {
           get
            {
                return historydata;
            }
            set { historydata = value; RaisePropertyChanged(() => Historydata); }
        }
        public HistoryViewModel()
        {
            Historydata = new List<HistoryItem>();
            Historydata.Add(new HistoryItem() { Input = "hello", Output = "outout"});
            Historydata.Add(new HistoryItem() { Input = "hello", Output = "outout" });
            Historydata.Add(new HistoryItem() { Input = "hello", Output = "outout"});
            Historydata.Add(new HistoryItem() { Input = "hello", Output = "outout" });
            Historydata.Add(new HistoryItem() { Input = "hello", Output = "outout" });
            Historydata.Add(new HistoryItem() { Input = "hello", Output = "outout" });
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            
            



        }


    }
}
