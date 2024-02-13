using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task01.Commands.Base;
using Task01.Model.Accsess;
using Task01.Model.Data;
using Task01.View;

namespace Task01.ViewModel
{
    public class AuthorizationViewModel : BaseViewModel
    {
        public Dictionary<string, User> UserDic { get; }
        private string _SelectedUser;
        public string SelectedUser
        {
            get => _SelectedUser;
            set => Set(ref _SelectedUser, value);
        }

        public ICommand Login {get; }
        public ICommand Cancel { get; }

        public AuthorizationViewModel()
        {
            UserDic = UserSets.GetUserList();
            Cancel = new RelayCommand((obj) => Application.Current.Shutdown());
            Login = new RelayCommand(OnLogin, (obj) => SelectedUser != null);
        }

        private void OnLogin(object window)
        {
            var session = new Session(UserDic[SelectedUser], Repository.CurrentRepository);
            var mainWindow = new MainWindow(session);
            var win = window as Window;
            win.Close();
            mainWindow.Show();
        }
    }
}
