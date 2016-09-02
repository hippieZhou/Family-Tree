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

        private int _moduleIndex;

        public int ModuleIndex
        {
            get { return _moduleIndex; }
            set { SetProperty(ref _moduleIndex, value); }
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
                                case Menu.祖训:
                                    this.ModuleIndex = 0;
                                    break;
                                case Menu.世祖:
                                    this.ModuleIndex = 1;
                                    break;
                                case Menu.设置:
                                    this.ModuleIndex = 2;
                                    break;
                                case Menu.关于:
                                    this.ModuleIndex = 3;
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
        public MainViewModel()
        {
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
