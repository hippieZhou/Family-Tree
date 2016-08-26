using Microsoft.Practices.Unity;
using Prism.Unity;
using ZQ.PrismUnityApp.Views;
using System.Windows;
using ZQ.PrismUnityApp.ViewModels;
using Prism.Modularity;

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
                        var mainView = Container.Resolve<Shell>();
                        App.Current.MainWindow = mainView;
                        mainView.Show();

                        loginView.Close();
                    };
                }
                return loginView;
            }
            else
            {
                return Container.Resolve<Shell>();
            }
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var typeUser = typeof(Module.User.UserModule);
            this.ModuleCatalog.AddModule(new ModuleInfo(typeUser.Name, typeUser.AssemblyQualifiedName));

            var typeAbout = typeof(Module.About.AboutModule);
            this.ModuleCatalog.AddModule(new ModuleInfo(typeAbout.Name, typeAbout.AssemblyQualifiedName));
        }
    }
}
