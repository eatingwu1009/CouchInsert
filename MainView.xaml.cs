using System.Windows;
using System.Windows.Controls;

namespace CouchInsert
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void marker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expander = (Expander)sender;
            expander.Header = "Click to collapse";
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander expander = (Expander)sender;
            expander.Header = "Click to expand";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
