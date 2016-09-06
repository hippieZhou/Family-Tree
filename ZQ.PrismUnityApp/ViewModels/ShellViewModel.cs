using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Diagnostics;

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
        public IRegionManager regionManager 
        {
            get { return ServiceLocator.Current.GetInstance<IRegionManager>(); }
        }
        public IModuleManager moduleManager
        {
            get { return ServiceLocator.Current.GetInstance<IModuleManager>(); }
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
                                Debug.WriteLine(menu);
                                switch (menu)
                                {
                                    case Menu.祖训:
                                        ((ModuleManager)moduleManager).LoadModule("ZQ.Module.Guidance");
                                        break;
                                    case Menu.世祖:
                                         ((ModuleManager)moduleManager).LoadModule("ZQ.Module.Syutsou");
                                        break;
                                    case Menu.设置:
                                         ((ModuleManager)moduleManager).LoadModule("ZQ.Module.Settings");
                                        break;
                                    case Menu.关于:
                                         ((ModuleManager)moduleManager).LoadModule("ZQ.Module.About");
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
