using System;
using System.Dynamic;
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
using System.Windows.Shapes;
using Task01.Model.Accsess;
using Task01.ViewModel;

namespace Task01.View
{
    /// <summary>
    /// Логика взаимодействия для EditsHystoryViewModel.xaml
    /// </summary>
    public partial class EditsHystoryWindow : Window
    {
        public EditsHystoryWindow(Synchronizer synchronizer, ExpandoObject dynamicObject)
        {
            InitializeComponent();
            DataContext = new EditsHystoryViewModel(synchronizer, dynamicObject);
        }
    }
}
