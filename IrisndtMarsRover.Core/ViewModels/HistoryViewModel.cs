using System.Threading.Tasks;
using MvvmCross.ViewModels;
using IrisndtMarsRover.Core;
using System.Collections.Generic;
using MvvmCross.Navigation;
using IrisndtMarsRover.Core.Models;
using System.Collections.ObjectModel;

namespace IrisndtMarsRover.Core.ViewModels
{
    public class HistoryItem
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }

    public class HistoryViewModel : MvxViewModel<List<RoverEntity>>
    {
        private ObservableCollection<HistoryItem> historydata;
        public ObservableCollection<HistoryItem> Historydata
        {
           get
            {
                return historydata;
            }
            set { historydata = value; RaisePropertyChanged(() => Historydata); }
        }
        public HistoryViewModel()
        {
            Historydata = new ObservableCollection<HistoryItem>();
        }


        public override void Prepare(List<RoverEntity> parameter)
        {
            Historydata.Clear();
            foreach(var item in parameter)
            {
                Historydata.Add( new HistoryItem() {  Input = item.input, Output = item.output } );
            }
           

        }


        public override async Task Initialize()
        {
            await base.Initialize();
            
            



        }


    }
}
