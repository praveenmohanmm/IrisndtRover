using System.Threading.Tasks;
using MvvmCross.ViewModels;
using IrisndtMarsRover.Core;
using MvvmCross.Navigation;
using System;
using IrisndtMarsRover.Core.Models;
using MvvmCross.Commands;
using IrisndtMarsRover.Core.Service;
using System.Windows.Input;
using System.Collections.Generic;

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

       
        /// <summary>
        /// get final points from azure
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RoverFinalPoints> GetFinalPosFromAzure(RoverInput input )
        {
            RoverService service = new RoverService();
            RoverFinalPoints res = await service.GetFinalPoints(input);
            if (res != null && res.FlowPath.Length > 0)
            {
                return res;
            }
            return null;
        }

        /// <summary>
        /// save screenshots and other in-outs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SaveScreenShotData(RoverEntity input)
        {
            RoverService service = new RoverService();
            var res = await service.SaveData(input);
            return res;
        }

        /// <summary>
        /// get complete history of inouts
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<RoverEntity>> GetAllHistoryInformations()
        {
            RoverService service = new RoverService();
            var res = await service.GetAllDatas();
            return res;
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
