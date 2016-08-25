using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZQ.PrismUnityApp.ViewModels
{
    public class LoginViewModel:BindableBase
    {
        public Action Validate;
        public Action Close;

        private DelegateCommand _loginCmd;
        public DelegateCommand LoginCmd
        {
            get
            {
                return _loginCmd ?? (_loginCmd = new DelegateCommand(() =>
                    {
                        if (this.Validate != null)
                        {
                            this.Validate();
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
                        App.Current.Shutdown();
                    }));
            }
        }

    }
}
