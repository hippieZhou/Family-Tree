using Microsoft.Practices.Unity;
using Prism.Unity;
using ZQ.PrismUnityApp.Views;
using System.Windows;
using ZQ.PrismUnityApp.ViewModels;
using Prism.Modularity;
using Prism.Events;

namespace ZQ.PrismUnityApp
{
    class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// 获取全局的事件聚合器
        /// </summary>
        public IEventAggregator EventAggregator
        {
            get
            {
                return this.Container.TryResolve<IEventAggregator>();
            }
        }

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

        #region MyRegion

         //protected override void ConfigureModuleCatalog()
        //{
        //    #region 基于代码方式的模块加载方法
        //    var typeGuidance = typeof(Module.Guidance.GuidanceModule);
        //    var guidanceModule = new ModuleInfo()
        //    {
        //        ModuleName = typeGuidance.Name,
        //        ModuleType = typeGuidance.AssemblyQualifiedName,
        //        InitializationMode = InitializationMode.WhenAvailable
        //    };

        //    var typeSyutsou = typeof(Module.Syutsou.SyutsouModule);
        //    var syutsouModule = new ModuleInfo()
        //    {
        //        ModuleName = typeSyutsou.Name,
        //        ModuleType = typeSyutsou.AssemblyQualifiedName,
        //        InitializationMode = InitializationMode.WhenAvailable
        //    };

        //    var typeSettings = typeof(Module.Settings.SettingsModule);
        //    var settingsModule = new ModuleInfo()
        //    {
        //        ModuleName = typeSettings.Name,
        //        ModuleType = typeSettings.AssemblyQualifiedName,
        //        InitializationMode = InitializationMode.WhenAvailable
        //    };

        //    var typeAbout = typeof(Module.About.AboutModule);
        //    var aboutModule = new ModuleInfo()
        //    {
        //        ModuleName = typeAbout.Name,
        //        ModuleType = typeAbout.AssemblyQualifiedName,
        //        InitializationMode = InitializationMode.WhenAvailable
        //    };

        //    this.ModuleCatalog.AddModule(guidanceModule);
        //    this.ModuleCatalog.AddModule(syutsouModule);
        //    this.ModuleCatalog.AddModule(settingsModule);
        //    this.ModuleCatalog.AddModule(aboutModule);

        //    #endregion
        //}

	    #endregion

      
        protected override IModuleCatalog CreateModuleCatalog()
        {
            //创建基于配置文件的模块目录
            return new ConfigurationModuleCatalog();

            //通过目录文件的方式来加载模块
            //return new DirectoryModuleCatalog() { ModulePath = @"Modules" };
        }


        /// <summary>
        /// 创建基于配置文件的依赖注入容器
        /// </summary>
        /// <returns></returns>
        //protected override IUnityContainer CreateContainer()
        //{
        //    return base.CreateContainer();
        //}

        /// <summary>
        /// 获取全局的一个日志处理对象
        /// </summary>
        /// <returns></returns>
        protected override Prism.Logging.ILoggerFacade CreateLogger()
        {
            return base.CreateLogger();
        }

    }
}
