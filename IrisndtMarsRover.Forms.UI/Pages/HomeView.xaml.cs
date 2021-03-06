﻿using MvvmCross.Forms.Views;
using IrisndtMarsRover.Core.ViewModels;
using IrisndtMarsRover.Forms.UI.Controls;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using IrisndtMarsRover.Core;
using System.Collections.Generic;
using System;

using System.Linq;
using IrisndtMarsRover.Forms.UI.interfaces;
using IrisndtMarsRover.Core.Models;
using IrisndtMarsRover.Core.Service;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace IrisndtMarsRover.Forms.UI.Pages
{
    public partial class HomeView : MvxContentPage<HomeViewModel>
    {
        #region input variables
        int rows, cols;
        string commands;
        string outputpath;
        bool drawPath;
        Point startingPos;
        string startingDirection;
        #endregion


        #region output variables
        List<FlowPath> flowPath;
        float finalXPos;
        float finalYPos;
        string finalFirection;
        #endregion

        public HomeView()
        {
            InitializeComponent();
            drawPath = false;
            SizeEntry.Text = "5 5";
            NavigationPage.SetHasNavigationBar(this, false);
            flowPath = new List<FlowPath>();

        }


        /// <summary>
        /// skia's rendering function all draw calls must be inside this function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {

            // canvas creation
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 10,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round
            };


            // canvas is divived in to graph coordinates each coorinates size is respect to the screen width / height
            float widthDensity = Convert.ToSingle(canvasView.CanvasSize.Width / cols);
            float heightDensity = Convert.ToSingle(canvasView.CanvasSize.Height / rows);

            canvas.Clear();

            // draw grid in the canvas
            SetupCanvas(args);

            try
            {
                // draw line path only when the path is ready
                // path will be getting from server
                if (drawPath && flowPath.Count > 0 )
                {
                    SKPath path = new SKPath();

                    path.MoveTo(Convert.ToSingle(startingPos.X * widthDensity), Convert.ToSingle(canvasView.CanvasSize.Height - (startingPos.Y * heightDensity)));

                    foreach(var item in flowPath)
                    {
                        path.LineTo((item.XPos * widthDensity), Convert.ToSingle(canvasView.CanvasSize.Height - (item.YPos * heightDensity)));
                    }
                    canvas.DrawPath(path, paint);

                    // end point circle paint
                    SKPaint circleRedPaint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = SKColors.Red,
                        StrokeWidth = 5,
                        StrokeCap = SKStrokeCap.Round,
                        StrokeJoin = SKStrokeJoin.Round
                    };

                    // start point circle green paint
                    SKPaint circleGreenPaint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = SKColors.Green,
                        StrokeWidth = 5,
                        StrokeCap = SKStrokeCap.Round,
                        StrokeJoin = SKStrokeJoin.Round
                    };

                    // text paint for displaying the output text
                    using (var textPaint = new SKPaint())
                    {
                        textPaint.TextSize = 45;
                        textPaint.IsAntialias = true;
                        textPaint.Color = SKColors.DarkRed;
                        textPaint.IsStroke = true;
                        textPaint.StrokeWidth = 5;
                        textPaint.TextAlign = SKTextAlign.Center;
                        canvas.DrawText(outputpath, new SKPoint(canvasView.CanvasSize.Width / 2, canvasView.CanvasSize.Height / 2), textPaint);
                    }

                    // end pos
                    canvas.DrawCircle(new SKPoint(finalXPos * widthDensity, canvasView.CanvasSize.Height - finalYPos * heightDensity), 15, circleRedPaint);

                    // start pos
                    canvas.DrawCircle(new SKPoint(Convert.ToSingle(startingPos.X * widthDensity), Convert.ToSingle((canvasView.CanvasSize.Height - (startingPos.Y * heightDensity)))), 15, circleGreenPaint);

                }
            }
            catch (Exception ex)
            {
                DisplayAlert(Constants.Error, Constants.OutofCordinates, Constants.Ok);
            }

        }

        /// <summary>
        /// draw grid lines in the canvas
        /// </summary>
        /// <param name="args"></param>
        void SetupCanvas(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            // gray color line paint
            SKPaint graphPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Gray,
                StrokeWidth = 1,
                StrokeCap = SKStrokeCap.Square,
                StrokeJoin = SKStrokeJoin.Round
            };

            // canvas is divived in to graph coordinates each coorinates size is respect to the screen width / height
            float widthDensity = Convert.ToSingle(canvasView.CanvasSize.Width / rows);
            float heightDensity = Convert.ToSingle(canvasView.CanvasSize.Height / rows);

            canvas.Clear();
            int radious = 5;
            // draw grid lines vertically and horizontally
            for (int index = 0; index < rows + 1; index++)
            {
                canvas.DrawCircle(new SKPoint(index * widthDensity, 0), radious, graphPaint);
                canvas.DrawCircle(new SKPoint(index * widthDensity, canvasView.CanvasSize.Height), radious, graphPaint);
                canvas.DrawCircle(new SKPoint(0, index * heightDensity), radious, graphPaint);
                canvas.DrawCircle(new SKPoint(0, index * heightDensity), radious, graphPaint);
                canvas.DrawLine(index * widthDensity, 0, index * widthDensity, canvasView.CanvasSize.Height, graphPaint);
                canvas.DrawLine(0, index * heightDensity, canvasView.CanvasSize.Width, index * heightDensity, graphPaint);
            }
        }

        /// <summary>
        /// display progress bar
        /// </summary>
        /// <param name="val"></param>
        private void ShowActivityControl(bool val)
        {
            LoadingIndicator.IsRunning = val;
            LoadingIndicator.IsVisible = val;
        }


        /// <summary>
        /// for executing the rover com,mand in the text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnExecuteCommand(System.Object sender, System.EventArgs e)
        {
            try
            {
                // max line is two, first line is postion and next is commands
                int numLines = CommandEditor.Text.Split('\n').Length;
                if( numLines != 2 )
                {
                    await DisplayAlert(Constants.Error, Constants.CommandInvalid, Constants.Ok);
                    return;
                }

                ShowActivityControl(true);

                // parse input from the text box
                drawPath = true;
                var commandsText = CommandEditor.Text.Trim().Split('\n').ToList();
                var startpos = commandsText[0].Split(' ').ToList();
                startingPos.X = Convert.ToDouble(startpos[0]);
                startingPos.Y = Convert.ToDouble(startpos[1]);
                startingDirection = startpos[2].ToString();
                commands = commandsText[1];

                // create input for web service from the commands and first pos
                RoverInput input = new RoverInput()
                {
                    commands = commands,
                    startXPos = startingPos.X,
                    startYPos = startingPos.Y,
                    max = rows,
                    startDirection = (int)(RoverDirection)Enum.Parse(typeof(RoverDirection), startingDirection)

                };

                // invoke web service, whuch will return final postion of the rover and flow paths
                // services are developer and hosteed in azure portal using azure functions
                HomeViewModel model = (HomeViewModel)this.BindingContext.DataContext;
                var res = await model.GetFinalPosFromAzure(input);
                if( res != null )
                {
                    flowPath = res.FlowPath.ToList();
                    finalXPos = float.Parse(res.FinalXPos);
                    finalYPos = float.Parse(res.FinalYPos);
                    finalFirection = res.FinalDirection;
                    outputpath = "Final Points : " + res.FinalXPos + " , " + res.FinalYPos + " Direction : " + finalFirection;
                    canvasView.InvalidateSurface();
                }


                ShowActivityControl(false);

            }
            catch (Exception ex)
            {
                await DisplayAlert(Constants.Error, Constants.OutofCordinates, Constants.Ok);
                ShowActivityControl(false);
            }

            ShowActivityControl(false);
        }

        /// <summary>
        /// Create surface for rendering all skia works
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnCreateplateau(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (SizeEntry.Text.Length == 0 )
                {
                    await DisplayAlert(Constants.Error, Constants.InvalidInput, Constants.Ok);
                    return;
                }
                    

                ShowActivityControl(true);
                drawPath = false;

                // parse inputs
                var maxPoints = SizeEntry.Text.Replace(" ", "").ToList();
                rows = Int32.Parse(maxPoints[0].ToString());
                cols = Int32.Parse(maxPoints[1].ToString());

                if (rows != cols)
                {
                    await DisplayAlert(Constants.Error, Constants.RowColError, Constants.Ok);
                }
                else if( (rows > 9 || cols > 9 ) || 0 == rows || 0 == cols)
                {
                    await DisplayAlert(Constants.Error, Constants.RowMax, Constants.Ok);
                }
                else
                {
                    canvasView.InvalidateSurface();
                }
            }
            catch(Exception ex)
            {
                ShowActivityControl(false);
                await DisplayAlert(Constants.Error, Constants.OutofCordinates, Constants.Ok);
            }
         
            ShowActivityControl(false);
        }

        /// <summary>
        /// save current screenshot and input commands and output position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnSave(System.Object sender, System.EventArgs e)
        {
            if(outputpath == null || outputpath.Length == 0)
            {
                await DisplayAlert(Constants.Error, Constants.InvalidInput, Constants.Ok);
                return;
            }

            ShowActivityControl(true);
            var screnshot = DependencyService.Get<IScreenshotService>().Capture();

            RoverEntity input = new RoverEntity()
            {
                input = "input : " + CommandEditor.Text,
                output = outputpath

            };

          
            // add screenshot data to model
            input.image = Encoding.UTF8.GetString(screnshot, 0, screnshot.Length);

            // invoke save web service, return true if success
            HomeViewModel model = (HomeViewModel)this.BindingContext.DataContext;
            var res = await model.SaveScreenShotData(input);
            if (res)
            {
                await DisplayAlert("Success", Constants.SavedToServer, Constants.Ok);
            }
            ShowActivityControl(false);
        }


        /// <summary>
        /// for navigating to history page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnHistory(System.Object sender, System.EventArgs e)
        {
            ShowActivityControl(true);

            HomeViewModel model = (HomeViewModel)this.BindingContext.DataContext;
            var res = await model.GetAllHistoryInformations();
            if (res != null)
            {
                await AppStart.navigation.Navigate<HistoryViewModel, List<RoverEntity>>(res);
            }
            ShowActivityControl(false);
           
        }
    }
}