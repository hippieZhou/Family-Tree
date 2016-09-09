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
                        Menu menu;
                        if (Enum.TryParse(str, out menu))
                        {
                            if (this.CurrentMenu != menu)
                            {
                                this.CurrentMenu = menu;

                                object obj = null;

                                switch (menu)
                                {
                                    case Menu.祖训:
                                        obj = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.Guidance.Views.MainView).FullName);
                                        break;
                                    case Menu.世祖:
                                        obj = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.Syutsou.Views.MainView).FullName);
                                        break;
                                    case Menu.设置:
                                        obj = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.Settings.Views.MainView).FullName);
                                        break;
                                    case Menu.关于:
                                        obj = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.About.Views.MainView).FullName); 
                                        break;
                                    default:
                                        return;
                                }
                                if (obj != null)
                                {
                                    this.regionManager.Regions[this.MainRegion].Activate(obj);
                                    Debug.WriteLine(this.CurrentMenu);
                                }
                            }
                        }
                    }));
            }
        }

        private DelegateCommand _gobackCmd;

        public DelegateCommand GobackCmd
        {
            get
            {
                return _gobackCmd ?? (_gobackCmd = new DelegateCommand(() =>
                    {
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
