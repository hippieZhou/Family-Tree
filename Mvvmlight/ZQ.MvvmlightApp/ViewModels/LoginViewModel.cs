using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;

namespace ZQ.MvvmlightApp.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        //public Action Validate; 简化版
        public delegate void Validate(object sender);
        public event Validate ValiateEvent;

        private RelayCommand _loginCmd;
        public RelayCommand LoginCmd
        {
            get
            {
                return _loginCmd ?? (_loginCmd = new RelayCommand(() =>
                {
                    this.ValiateEvent?.Invoke(DateTime.Now);
                }));
            }
        }

        private RelayCommand _logoutCmd;
        public RelayCommand LogoutCmd
        {
            get
            {
                return _logoutCmd ?? (_logoutCmd = new RelayCommand(() =>
                {
                    Application.Current.Shutdown();
                }));
            }
        }
    }
}
