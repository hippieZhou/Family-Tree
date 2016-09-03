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

    public class MainViewModel : BindableBase
    {


        private string _title = "Prism APP";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand<string> _chooseMenuCmd;

        public DelegateCommand<string> ChooseMenuCmd
        {
            get
            {
                return _chooseMenuCmd ?? (_chooseMenuCmd = new DelegateCommand<string>((str) =>
                    {
                        Menu menu = Menu.关于;
                        if (Enum.TryParse(str, out menu))
                        {
                            switch (menu)
                            {
                                case Menu.设置:
                                    this.regionManager.RequestNavigate("MainRegion", "MainView");
                                    break;
                                case Menu.关于:
                                    this.regionManager.RequestNavigate("MainRegion", "MainView");
                                    break;
                                default:
                                    return;
                            }
                            Debug.WriteLine(menu);
                        }
                    }));
            }
        }



        IModuleManager moduleManager;
        IRegionManager regionManager;
        public MainViewModel()
        {
            regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            this.moduleManager = ServiceLocator.Current.GetInstance<IModuleManager>();
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
