using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task01.Infrastructure;
using Task01.Model.Accsess;
using Task01.Model.Data;
using Task01.Model.Data;
using Task01.ViewModel;

namespace Task01.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Session session)
        {
            DataContext = new ClientsViewModel(session);
            InitializeComponent();
            Closing += MainWindow_Closing;
        }

        private void SetDataGridHeaders(object sender, EventArgs e)
        {
            var grid = sender as DataGrid;
            foreach (var col in grid.Columns)
            {
                var property = col.Header.ToString();
                col.SortMemberPath = property;
                var button = new Button();
                button.Content = (DataContext as ClientsViewModel).ColumnHeaders[property];
                button.Tag = property;
                button.Click += OnSortButtonClick;
                col.Header = button;
            }
        }

        private Dictionary<string, ListSortDirection> sortDirections = [];

        private void OnSortButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var sortProperty = button.Tag.ToString();

            if (!sortDirections.ContainsKey(sortProperty))
            {
                sortDirections = [];
                sortDirections[sortProperty] = ListSortDirection.Ascending;
            }
            else
            {
                if (sortDirections[sortProperty] == ListSortDirection.Ascending)
                    sortDirections[sortProperty] = ListSortDirection.Descending;
                else
                    sortDirections[sortProperty] = ListSortDirection.Ascending;
            }

            var collectionView = CollectionViewSource.GetDefaultView(dynamicDataGrid.ItemsSource);
            collectionView.SortDescriptions.Clear();
            collectionView.SortDescriptions.Add(new SortDescription(sortProperty, sortDirections[sortProperty]));
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            (this.DataContext as ClientsViewModel)?.OnWindowClosing();
        }
    }
}