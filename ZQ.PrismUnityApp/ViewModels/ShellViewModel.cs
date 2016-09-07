using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.Windows;

namespace ZQ.PrismUnityApp.ViewModels
{
    //http://www.codeproject.com/Articles/165370/Creating-View-Switching-Applications-with-Prism
    //http://stackoverflow.com/questions/24176964/wpf-prism-mvvm-changing-modules-views
    [Flags]
    public enum Menu
    {
        祖训 = 0x01,
        世祖 = 0x02,
        设置 = 0x03,
        关于 = 0x04
    };

    public class ShellViewModel : BindableBase
    {
        public string MainRegion
        {
            get { return Application.Current.Resources["MainRegion"].ToString(); }
        }
        public IModuleManager moduleManager
        {
            get { return ServiceLocator.Current.GetInstance<IModuleManager>(); }
        }
        public IRegionManager regionManager
        {
            get { return ServiceLocator.Current.GetInstance<IRegionManager>(); }
        }


        private string _title = "Prism APP";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        
        private Menu _currentMenu;
        public Menu CurrentMenu
        {
            get { return _currentMenu; }
            set { SetProperty(ref _currentMenu, value); }
        }

        private DelegateCommand<string> _chooseMenuCmd;
        public DelegateCommand<string> ChooseMenuCmd
        {
            get
            {
                return _chooseMenuCmd ?? (_chooseMenuCmd = new DelegateCommand<string>((str) =>
                    {
                        this.moduleManager.LoadModule("AboutModule");
                        var st = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.About.Views.MainView).Name);

                        Menu menu;
                        if (Enum.TryParse(str, out menu))
                        {
                            if (this.CurrentMenu != menu)
                            {
                                this.CurrentMenu = menu;

                                var obj = new object();

                                switch (menu)
                                {
                                    #region MyRegion

                                    //case Menu.祖训:
                                    //    this.moduleManager.LoadModule("GuidanceModule");
                                    //    //this.regionManager.Regions[this.MainRegion].Activate(typeof(Module.Guidance.Views.MainView));
                                    //    break;
                                    //case Menu.世祖:
                                    //    this.moduleManager.LoadModule("SyutsouModule");
                                    //    //this.regionManager.Regions[this.MainRegion].Activate(typeof(Module.Syutsou.Views.MainView));
                                    //    break;

                                    #endregion

                                    case Menu.设置:
                                        this.moduleManager.LoadModule("SettingsModule");
                                        obj = this.regionManager.Regions[this.MainRegion].GetView("MainView");
                                        this.regionManager.Regions[this.MainRegion].Activate(obj);
                                        break;
                                    case Menu.关于:
                                        this.moduleManager.LoadModule("AboutModule");
                                        obj = this.regionManager.Regions[this.MainRegion].GetView("MainView");
                                        this.regionManager.Regions[this.MainRegion].Activate(obj);
                                        break;
                                    default:
                                        return;
                                }
                                
                            }
                        }
                    }));
            }
        }

        public ShellViewModel()
        {
            this.moduleManager.ModuleDownloadProgressChanged += moduleManager_ModuleDownloadProgressChanged;
            this.moduleManager.LoadModuleCompleted += moduleManager_LoadModuleCompleted;
        }
        void moduleManager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
        }
        void moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
        }
    }
}
