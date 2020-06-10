using System.Threading.Tasks;
using MvvmCross.ViewModels;
using IrisndtMarsRover.Core;
using MvvmCross.Navigation;
using System;

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
            get
            {
                return rowscolsEntry;
            }
            set { RowscolsEntry = value; RaisePropertyChanged(() => RowscolsEntry); }
        }
        public string CommandsEntry
        {
            get
            {
                return commandsEntry;
            }
            set { CommandsEntry = value; RaisePropertyChanged(() => CommandsEntry); }
        }
        #endregion
        public HomeViewModel()
        {

           
           

        }

        public override async Task Initialize()
        {
            await base.Initialize();

            try
            {
               // RowscolsEntry = "5 5";
        
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
            }


        }

      
    }
}
