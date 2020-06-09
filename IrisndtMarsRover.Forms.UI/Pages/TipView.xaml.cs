using MvvmCross.Forms.Views;
using IrisndtMarsRover.Core.ViewModels;
using IrisndtMarsRover.Forms.UI.Controls;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using IrisndtMarsRover.Core;
using System.Collections.Generic;
using System;
using System.Linq;

namespace IrisndtMarsRover.Forms.UI.Pages
{
    public partial class TipView : MvxContentPage<TipViewModel>
    {
        int rows, cols;
        string commands;
        string outputpath;
        bool drawPath;
        Point startingPos;
        string startingDirection;
        public TipView()
        {
            InitializeComponent();
            drawPath = false;
            SizeEntry.Text = "5 5";
          string val = @"1 2 N
LMLMLMLMM";
            CommandEditor.Text = val;

        }



        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
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


          
            float widthDensity = Convert.ToSingle(canvasView.CanvasSize.Width / cols);

            float heightDensity = Convert.ToSingle(canvasView.CanvasSize.Height / rows);

            canvas.Clear();


            SetupCanvas(args);


            if(drawPath)
            {
                Position position = new Position()
                {
                    X = (int)startingPos.X,
                    Y = (int)startingPos.Y,
                    Direction = Directions.N
                };

                var maxPoints = new List<int>() { rows, cols };


                position.StartMoving(maxPoints, commands);

                var actualOutput = $"{position.X} {position.Y} {position.Direction.ToString()}";
                outputpath = position.X.ToString() + " " + position.Y.ToString() + " " + position.Direction.ToString();

                SKPath path = new SKPath();

                path.MoveTo(2 * widthDensity, Convert.ToSingle(canvasView.CanvasSize.Height -( 2 * heightDensity)));

                for (int index = 0; index < position.FlowPath.Count; index++)
                {
                    path.LineTo((position.FlowPath[index].XPos * widthDensity), Convert.ToSingle(canvasView.CanvasSize.Height - (position.FlowPath[index].YPos * heightDensity)));
                }
                canvas.DrawPath(path, paint);


                SKPaint circlePaint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Red,
                    StrokeWidth = 5,
                    StrokeCap = SKStrokeCap.Round,
                    StrokeJoin = SKStrokeJoin.Round
                };


                using (var textPaint = new SKPaint())
                {
                    textPaint.TextSize = 30;
                    textPaint.IsAntialias = true;
                    textPaint.Color = SKColors.Red;
                    textPaint.IsStroke = true;
                    textPaint.StrokeWidth = 3;
                    textPaint.TextAlign = SKTextAlign.Center;
                    canvas.DrawText(outputpath, new SKPoint(canvasView.CanvasSize.Width / 2, canvasView.CanvasSize.Height / 2), textPaint);
                }

                canvas.DrawCircle(new SKPoint(position.X * widthDensity, canvasView.CanvasSize.Height - position.Y * heightDensity), 15, circlePaint);
               
            }

            
        }


        void SetupCanvas(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;


            SKPaint graphPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Gray,
                StrokeWidth = 1,
                StrokeCap = SKStrokeCap.Square,
                StrokeJoin = SKStrokeJoin.Round
            };


            float widthDensity = Convert.ToSingle(canvasView.CanvasSize.Width / rows);
            float heightDensity = Convert.ToSingle(canvasView.CanvasSize.Height / rows);

            canvas.Clear();
            int radious = 5;
            for (int index = 0; index < rows + 1; index++)
            {
                canvas.DrawCircle(new SKPoint(index * widthDensity, 0), radious, graphPaint);
                canvas.DrawCircle(new SKPoint(index * widthDensity, canvasView.CanvasSize.Height), radious, graphPaint);
                canvas.DrawCircle(new SKPoint(0, index * heightDensity), radious, graphPaint);
                canvas.DrawCircle(new SKPoint(0, index * heightDensity), radious, graphPaint);
                canvas.DrawLine( index * widthDensity, 0, index * widthDensity, canvasView.CanvasSize.Height, graphPaint);
                canvas.DrawLine(0, index * heightDensity, canvasView.CanvasSize.Width, index * heightDensity, graphPaint);
            }

        }

        async void OnExecuteCommand(System.Object sender, System.EventArgs e)
        {
            try
            {
                drawPath = true;
                var commandsText = CommandEditor.Text.Trim().Split('\n').ToList();
                var startpos = commandsText[0].Split(' ').ToList();
                startingPos.X = Convert.ToDouble(startpos[0]);
                startingPos.Y = Convert.ToDouble(startpos[1]);
                startingDirection = startpos[2].ToString();
                commands = commandsText[1];


                canvasView.InvalidateSurface();


                await MasterView.ScrollToAsync(MasterView, ScrollToPosition.End, true);
            }
            catch(Exception ex)
            {
                DisplayAlert("Error", "String is not in corecvt format", "Cancel");
            }
           

        }

        void OnCreateplateau(System.Object sender, System.EventArgs e)
        {
            drawPath = false;
            var maxPoints = SizeEntry.Text.Trim().Split(' ').Select(int.Parse).ToList();
          

            rows = maxPoints[0];
            cols = maxPoints[1];
           

            canvasView.InvalidateSurface();
            MasterView.ScrollToAsync(MasterView, ScrollToPosition.End, true);


        }
    }
}