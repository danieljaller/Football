using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserApp
{
    /// <summary>
    /// Interaction logic for TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        public TablePage()
        {
            InitializeComponent();
            GenerateGridRowsAndSetRowColor();
        }

        private void GenerateGridRowsAndSetRowColor()
        {
            string[] testArray = new string[16];

            foreach (var position in testArray)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto, MinHeight = 20 });
            }

            for (int i = 2; i < grid.RowDefinitions.Count(); i += 2)
            {
                Rectangle rect = new Rectangle();
                rect.Fill = new SolidColorBrush(Colors.LightGray);
                grid.Children.Add(rect);
                Grid.SetColumnSpan(rect, 9);
                Grid.SetRow(rect, i);
            }
        }
    }
}
