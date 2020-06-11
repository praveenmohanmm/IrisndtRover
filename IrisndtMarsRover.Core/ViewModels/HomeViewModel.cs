using System.Threading.Tasks;
using MvvmCross.ViewModels;
using IrisndtMarsRover.Core;
using MvvmCross.Navigation;
using System;
using IrisndtMarsRover.Core.Models;
using MvvmCross.Commands;

namespace IrisndtMarsRover.Core.ViewModels
{
   
    public class HomeViewModel : MvxViewModel
    {
        #region private
        private string rowscolsEntry;
        private string commandsEntry;
        #endregion

        #region Public properties
        public string RowscolsEntry
        {
            get { return rowscolsEntry; }
            set { SetProperty(ref rowscolsEntry, value); }
        }

        public string CommandsEntry
        {
            get { return commandsEntry; }
            set { SetProperty(ref commandsEntry, value); }
        }

        public string StartingHeading
        {
            get;set;
        }

        public string EndHeading
        {
            get; set;
        }

        public string PlateauHeading
        {
            get; set;
        }

        public string GridSizeEntryHeading
        {
            get; set;
        }

        public string ExecuteCommandHeading
        {
            get;set;
        }

        public string SaveDataHeading
        {
            get;set;
        }

        public string ViewHistoryHeading
        {
            get;set;
        }
        #endregion

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization, YEY!

            return base.Initialize();
        }

        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);

        private void ResetText()
        {
           
        }

   
     
        public  HomeViewModel()
        {
            try
            {
                StartingHeading = "Starting Pos";
                EndHeading = "Ending Pos";
                GridSizeEntryHeading = "Enter the grid size in X X format like 3 3";
                PlateauHeading = "Create plateau";
                ExecuteCommandHeading = "Execute Commands";
                SaveDataHeading = "Save Data";
                ViewHistoryHeading = "View History";
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
            }
        }

      
    }
}
