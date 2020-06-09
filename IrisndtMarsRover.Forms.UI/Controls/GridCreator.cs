using System;

using Xamarin.Forms;

namespace IrisndtMarsRover.Forms.UI.Controls
{
    public class GridCreator : ContentView
    {
        public GridCreator(int rows, int cols)
        {
            Grid grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                RowSpacing = 1,
                ColumnSpacing =1,
                Margin = new Thickness(10,10,10,20),
                Padding = new Thickness(1,1,1,1)
            };
            

            RowDefinitionCollection rowsList = new RowDefinitionCollection();
            for (int index = 0; index < rows; index++)
            {
                rowsList.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            grid.RowDefinitions = rowsList;


            ColumnDefinitionCollection colsList = new ColumnDefinitionCollection();
            for (int index = 0; index < cols; index++)
            {
                colsList.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            grid.ColumnDefinitions = colsList;

            Color testColor = Color.Red;
      

                    for (int colIndex = 0; colIndex < cols; colIndex++)
                    {
                        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
                        {

                            grid.Children.Add(new Label
                            {
                                TextColor = Color.Red,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                HorizontalTextAlignment = TextAlignment.Center,
                                VerticalTextAlignment = TextAlignment.Center,
                                Text = rowIndex.ToString() + " , " + colIndex.ToString()
                            }, rowIndex, colIndex);
                        }
                     }
          

            // Row 0
            // The BoxView and Label are in row 0 and column 0, and so only needs to be added to the
            // Grid.Children collection to get default row and column settings.
            //grid.Children.Add(new BoxView
            //{
            //    Color = Color.Green
            //},);
            //grid.Children.Add(new Label
            //{
            //    Text = "Row 0, Column 0",
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //});

            //// This BoxView and Label are in row 0 and column 1, which are specified as arguments
            //// to the Add method.
            //grid.Children.Add(new BoxView
            //{
            //    Color = Color.Blue
            //}, 1, 0);
            //grid.Children.Add(new Label
            //{
            //    Text = "Row 0, Column 1",
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center
            //}, 1, 0);

            //// Row 1
            //// This BoxView and Label are in row 1 and column 0, which are specified as arguments
            //// to the Add method overload.
            //grid.Children.Add(new BoxView
            //{
            //    Color = Color.Teal
            //}, 0, 1, 1, 2);
            //grid.Children.Add(new Label
            //{
            //    Text = "Row 1, Column 0",
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center
            //}, 0, 1, 1, 2); // These arguments indicate that that the child element goes in the column starting at 0 but ending before 1.
            //                // They also indicate that the child element goes in the row starting at 1 but ending before 2.

            //grid.Children.Add(new BoxView
            //{
            //    Color = Color.Purple
            //}, 1, 2, 1, 2);
            //grid.Children.Add(new Label
            //{
            //    Text = "Row1, Column 1",
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center
            //}, 1, 2, 1, 2);

            //// Row 2
            //// Alternatively, the BoxView and Label can be positioned in cells with the Grid.SetRow
            //// and Grid.SetColumn methods.
            //BoxView boxView = new BoxView { Color = Color.Red };
            //Grid.SetRow(boxView, 2);
            //Grid.SetColumnSpan(boxView, 2);
            //Label label = new Label
            //{
            //    Text = "Row 2, Column 0 and 1",
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center
            //};
            //Grid.SetRow(label, 2);
            //Grid.SetColumnSpan(label, 2);

            //grid.Children.Add(boxView);
            //grid.Children.Add(label);

            Content = grid;
        }
    }
        
}

