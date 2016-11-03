using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;

namespace ZQ.PrismUnityApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        //public Action Validate; 简化版
        public delegate void Validate(object sender);
        public event Validate ValiateEvent;


        private DelegateCommand _loginCmd;
        public DelegateCommand LoginCmd
        {
            get
            {
                return _loginCmd ?? (_loginCmd = new DelegateCommand(() =>
                    {
                        if (this.ValiateEvent != null)
                        {
                            this.ValiateEvent(DateTime.Now);
                        }
                    }));
            }
        }

        private DelegateCommand _logoutCmd;
        public DelegateCommand LogoutCmd
        {
            get
            {
                return _logoutCmd ?? (_logoutCmd = new DelegateCommand(() =>
                    {
                        Application.Current.Shutdown();
                    }));
            }
        }

    }
}
