using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Task01.Model.Accsess;
using Task01.Model.Data;
using Task01.View;
using Task01.Commands.Base;
using System.Data;
using System.Dynamic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using Task01.Commands.Base;

namespace Task01.ViewModel
{
    public class ClientsViewModel : BaseViewModel
    {
        public Session Session { get; }
        public Synchronizer Synchronizer { get; }
        public Type Type { get; }

        private DynamicItemCollection _SourceList;

        public DynamicItemCollection SourceList
        {
            get => _SourceList;
            set => Set(ref _SourceList, value);
        }

        public CollectionViewSource ViewList {  get; set; }

        private ExpandoObject _SelectedItem;
        public ExpandoObject SelectedItem
        {
            get => _SelectedItem;
            set => Set(ref _SelectedItem, value);
        }

        private DynamicItemCollection _OpenedItems;
        public DynamicItemCollection OpenedItems
        {
            get => _OpenedItems;
            set => Set(ref _OpenedItems, value);
        }

        public Dictionary<string, string> ColumnHeaders { get; }

        public ClientsViewModel()
        {
            Serialization.Deserialize();
            EditItemCommand = new RelayCommand(EditItem, CanEditItem);
            NewItemCommand = new RelayCommand(NewItem, CanCreateItem);
            DeleteItemCommand = new RelayCommand(DeleteItem, CanDeleteItem);

            _SourceList = [];
            OpenedItems = [];
            
            Type = typeof(Client);
            ColumnHeaders = Type.GetHeaders();
        }

        public ClientsViewModel(Session session) : this()
        {
            Session = session;
            Synchronizer = new Synchronizer(Session, Type);
            
            SourceList = Synchronizer.Collection;
            ViewList = new CollectionViewSource();
            ViewList.Source = SourceList;

            OnPropertyChanged(nameof(SourceList));
        }

        public ICommand EditItemCommand { get; }

        private void EditItem(object parameter)
        {
            var expObj = parameter as ExpandoObject;
            var newWindow = new ExpandoObjectWindow(Synchronizer, expObj);
            HandleOpenedItems(expObj, newWindow);
            newWindow.Show();
        }

        private bool CanEditItem(object parameter)
        {
            if (parameter != null && !OpenedItems.Contains(parameter))
                return true;
            return false;
        }
        public ICommand NewItemCommand { get; }
        private void NewItem(object parameter)
        {
            var expObj = Synchronizer.CreateNew();
            var newWindow = new ExpandoObjectWindow(Synchronizer, expObj, true);
            HandleOpenedItems(expObj, newWindow);
            newWindow.Show();
        }

        private bool CanCreateItem(object parameter)
        {
            return Synchronizer.Session.User.Role.CanAddNew(Type);
        }

        private bool CanDeleteItem(object parameter)
        {
            return Synchronizer.Session.User.Role.CanAddNew(Type) && parameter != null && !OpenedItems.Contains(parameter);
        }

        public ICommand DeleteItemCommand { get; }

        private void DeleteItem(object parameter)
        {
            Synchronizer.Delete(parameter as ExpandoObject);
        }

        private void HandleOpenedItems(ExpandoObject expObj, Window win)
        {
            OpenedItems.Add(expObj);
            win.Closed += (obj, e) => OpenedItems.Remove(expObj);
        }

        public void OnWindowClosing()
        {
            Serialization.Serialize();
        }
    }
}
