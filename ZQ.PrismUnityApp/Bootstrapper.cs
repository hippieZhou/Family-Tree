using Microsoft.Practices.Unity;
using Prism.Unity;
using ZQ.PrismUnityApp.Views;
using System.Windows;
using ZQ.PrismUnityApp.ViewModels;

namespace ZQ.PrismUnityApp
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var loginView = Container.Resolve<LoginWindow>();

            //登录
            if (loginView != null)
            {
                var loginVm = loginView.DataContext as LoginViewModel;
                if (loginVm != null)
                {
                    loginVm.Validate += () =>
                    {
                        var mainView = Container.Resolve<MainWindow>();
                        App.Current.MainWindow = mainView;
                        mainView.Show();

                        loginView.Close();

                    };
                }
                return loginView;
            }
            else
            {
                return Container.Resolve<MainWindow>();
            }
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
        protected override void ConfigureModuleCatalog()
        {
            var obj = this.ModuleCatalog.Modules;
            base.ConfigureModuleCatalog();
        }
    }
}
