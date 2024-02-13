using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Accsess;
using Task01.Model.Data;
using Task01.Infrastructure;
using System.Windows.Input;
using Task01.Commands.Base;
using System.Windows;
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Collections;
using System.Windows.Controls;
using Task01.View.Validation;
using Task01.View;

namespace Task01.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        public bool IsNew { get; }
        public Synchronizer Synchronizer { get; }

        private ExpandoObject _OriginalItem;
        public ExpandoObject OriginalItem
        {
            get => _OriginalItem;
            set => Set(ref _OriginalItem, value);
        }

        private ExpandoObject _EditableItem;

        public ExpandoObject EditableItem
        {
            get => _EditableItem;
            set => Set(ref _EditableItem, value);
        }

        public Dictionary<string, bool> IsReadOnlyDic { get; set; }
        private bool _IsEditsHystoryOpened;
        public bool IsEditsHystoryOpened
        {
            get => _IsEditsHystoryOpened;
            set => Set(ref _IsEditsHystoryOpened, value);
        }

        public ClientViewModel(Synchronizer synchronizer, ExpandoObject obj, bool isNew)
        {
            IsNew = isNew;
            Synchronizer = synchronizer;
            OriginalItem = obj;
            EditableItem = obj.Clone();
            IsEditsHystoryOpened = false;
            InitializeAccessDic(Synchronizer);
            Cancel = new RelayCommand(CancelEdits);
            Save = new RelayCommand(SaveEdits);
            EditsHistory = new RelayCommand(OpenEditsHistory, (obj) => !IsEditsHystoryOpened);

        }

        public void InitializeAccessDic(Synchronizer synchronizer)
        {
            var dic = synchronizer.Session.User.Role.AccessRules[synchronizer.Type];
            IsReadOnlyDic = [];
            foreach(var prop in dic)
            {
                if (prop.Value.Read == true)
                    IsReadOnlyDic[prop.Key] = !prop.Value.Write;
            }
        }

        public ICommand Cancel { get; }
        public ICommand Save { get; }
        public ICommand EditsHistory { get;  }

        public void SaveEdits(object window)
        {
            var source = EditableItem as IDictionary<string, object>;
            var toUpdate = OriginalItem as IDictionary<string, object>;

            if (!IsValid(source, out var errors))
            {
                MessageBox.Show("Не возможно сохранить:\n" + String.Join(". \n",[.. errors]), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var prop in source)
            {
                toUpdate[prop.Key] = prop.Value;
            }
            Synchronizer.Update(OriginalItem);
            var win = window as Window;
            win.Close();
        }

        public void CancelEdits(object window)
        {
            if (IsNew)
                Synchronizer.Delete(OriginalItem);
            var win = window as Window;
            win.Close();
        }

        public bool IsValid(IDictionary<string, object> valueDic, out List<string> errors)
        {
            var editeableProperties = valueDic
                .Where((pair) => IsReadOnlyDic.ContainsKey(pair.Key) && !IsReadOnlyDic[pair.Key])
                .ToList();
            errors = [];
            foreach (var prop in editeableProperties)
            {
                var value = prop.Value as string;
                if (string.IsNullOrEmpty(value) || value.Length < 2)
                    errors.Add("Значение поля " + prop.Key + " не может содержать менее 1 символа.");
            }
            return errors.Count == 0;
        }

        private void OpenEditsHistory(object parameter)
        {
            var win = new EditsHystoryWindow(Synchronizer, OriginalItem);
            IsEditsHystoryOpened = true;
            win.Closed += (obj, e) => IsEditsHystoryOpened = false;
            win.Show();
        }
    }
}
