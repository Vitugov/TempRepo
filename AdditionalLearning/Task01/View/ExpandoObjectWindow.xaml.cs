using System;
using System.Collections.Generic;
using System.Dynamic;
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
using Task01.Model.Accsess;
using Task01.View.Validation;
using Task01.ViewModel;

namespace Task01.View
{
    /// <summary>
    /// Логика взаимодействия для ExpandoObjectWindow.xaml
    /// </summary>
    public partial class ExpandoObjectWindow : Window
    {        
        public ExpandoObjectWindow(Synchronizer synchronizer, ExpandoObject dynamicObject, bool isNew = false)
        {
            InitializeComponent();
            DataContext = new ClientViewModel(synchronizer, dynamicObject, isNew);
            GenerateDynamicFields();
        }

        private void GenerateDynamicFields()
        {
            var vm = DataContext as ClientViewModel;
            var dic = vm.EditableItem as IDictionary<string, object>;
            List<BindingExpression> bindingExpressions = [];
            foreach (var property in dic)
            {
                var isReadOnly = vm.IsReadOnlyDic[property.Key];
                var label = new Label { Content = property.Key };
                var textBox = new TextBox
                {
                    Margin = new Thickness(0, 0, 0, 5),
                    IsReadOnly = isReadOnly
                };

                var binding = new Binding(property.Key)
                {
                    Source = vm.EditableItem,
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                
                if (!isReadOnly)
                    binding.ValidationRules.Add(new NotEmptyValidationRule());

                textBox.SetBinding(TextBox.TextProperty, binding);

                DynamicContentPanel.Children.Add(label);
                DynamicContentPanel.Children.Add(textBox);

                if (!isReadOnly)
                    bindingExpressions.Add(textBox.GetBindingExpression(TextBox.TextProperty));
            }

            bindingExpressions.ForEach((be) => be?.UpdateSource());
        }
    }
}
