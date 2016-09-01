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
            var loginView = Container.TryResolve<LoginView>();
            //登录
            if (loginView != null)
            {
                var loginVm = loginView.DataContext as LoginViewModel;
                if (loginVm != null)
                {
                    loginVm.Validate += () =>
                    {
                        var mainView = Container.TryResolve<Shell>();
                        Application.Current.MainWindow = mainView;
                        mainView.Show();

                        loginView.Close();
                    };
                }
                return loginView;
            }
            else
            {
                return Container.TryResolve<Shell>();
            }
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            #region 模块初始化（按需加载，使用时需要手动进行加载）

            var typeGuidance = typeof(Module.Guidance.GuidanceModule);
            var guidanceModule = new ModuleInfo()
            {
                ModuleName = typeGuidance.Name,
                ModuleType = typeGuidance.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            };

            var typeSyutsou = typeof(Module.Syutsou.SyutsouModule);
            var syutsouModule = new ModuleInfo()
            {
                ModuleName = typeSyutsou.Name,
                ModuleType = typeSyutsou.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            };


            var typeSettings = typeof(Module.Settings.SettingsModule);
            var settingsModule = new ModuleInfo()
            {
                ModuleName = typeSettings.Name,
                ModuleType = typeSettings.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            };

            var typeAbout = typeof(Module.About.AboutModule);
            var aboutModule = new ModuleInfo()
            {
                ModuleName = typeAbout.Name,
                ModuleType = typeAbout.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            };

            #endregion

            this.ModuleCatalog.AddModule(guidanceModule);
            this.ModuleCatalog.AddModule(syutsouModule);
            this.ModuleCatalog.AddModule(settingsModule);
            this.ModuleCatalog.AddModule(aboutModule);

            
        }
    }
}
