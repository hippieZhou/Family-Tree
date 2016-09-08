using Microsoft.Practices.Unity;
using Prism.Unity;
using ZQ.PrismUnityApp.Views;
using System.Windows;
using ZQ.PrismUnityApp.ViewModels;
using Prism.Modularity;
using Prism.Events;

namespace ZQ.PrismUnityApp
{
    partial class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// 获取全局的事件聚合器
        /// </summary>
        //public IEventAggregator EventAggregator
        //{
        //    get
        //    {
        //        return this.Container.TryResolve<IEventAggregator>();
        //    }
        //}

        protected override DependencyObject CreateShell()
        {
            return this.Container.TryResolve<Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = this.Shell as Window;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// 基于代码方式加载模块，只需要重写该函数即可
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            var typeGuidance = typeof(Module.Guidance.GuidanceModule);
            var typeSyutsou = typeof(Module.Syutsou.SyutsouModule);
            var typeSettings = typeof(Module.Settings.SettingsModule);
            var typeAbout = typeof(Module.About.AboutModule);

            //自动加载模块
            this.ModuleCatalog.AddModule(new ModuleInfo(typeGuidance.Name, typeGuidance.AssemblyQualifiedName) { InitializationMode = InitializationMode.WhenAvailable });
            this.ModuleCatalog.AddModule(new ModuleInfo(typeSyutsou.Name, typeSyutsou.AssemblyQualifiedName) { InitializationMode = InitializationMode.WhenAvailable });
            this.ModuleCatalog.AddModule(new ModuleInfo(typeSettings.Name, typeSettings.AssemblyQualifiedName) { InitializationMode = InitializationMode.WhenAvailable });
            this.ModuleCatalog.AddModule(new ModuleInfo(typeAbout.Name, typeAbout.AssemblyQualifiedName) { InitializationMode = InitializationMode.WhenAvailable });

            #region 手动加载模块

            //如果： InitializationMode = InitializationMode.OnDemand ，则需要在使用对应模块前加载该模块
            //this.moduleManager.LoadModule(typeof(Module.Guidance.GuidanceModule).Name);
            //this.moduleManager.LoadModule(typeof(Module.Syutsou.SyutsouModule).Name);
            //this.moduleManager.LoadModule(typeof(Module.Settings.SettingsModule).Name);
            //this.moduleManager.LoadModule(typeof(Module.About.AboutModule).Name);

            #endregion
        }


        /// <summary>
        /// 基于配置方式加载模块
        /// </summary>
        /// <returns></returns>
        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    //创建基于配置文件的模块目录
        //    //return new ConfigurationModuleCatalog();

        //    //通过目录文件的方式来加载模块
        //    //return new DirectoryModuleCatalog() { ModulePath = @"Modules" };
        //}


        /// <summary>
        /// 创建基于配置文件的依赖注入容器
        /// </summary>
        /// <returns></returns>
        protected override IUnityContainer CreateContainer()
        {
            return base.CreateContainer();
        }

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
