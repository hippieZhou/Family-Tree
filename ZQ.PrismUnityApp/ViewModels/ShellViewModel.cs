using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows;

namespace ZQ.PrismUnityApp.ViewModels
{
    //http://www.codeproject.com/Articles/165370/Creating-View-Switching-Applications-with-Prism
    //http://stackoverflow.com/questions/24176964/wpf-prism-mvvm-changing-modules-views
    [Flags]
    public enum Menu
    {
        Guidance = 0x01,
        Syutsou = 0x02,
        Settings = 0x03,
        About = 0x04
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

        private ConcurrentQueue<Menu> _vmCollection;
        public ConcurrentQueue<Menu> VmCollection
        {
            get { return _vmCollection ?? (_vmCollection = new ConcurrentQueue<object>()); }
            set { SetProperty(ref _vmCollection, value); }
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
                            Menu temp;
                            this.VmCollection.TryPeek(out temp);
                            if (temp != menu)
                            {
                                object obj;
                                FindViewByMenu(menu, out obj);
                                if (obj != null)
                                {
                                    this.VmCollection.Enqueue(menu);
                                    this.regionManager.Regions[this.MainRegion].Activate(obj);
                                }
                            }
                        }
                    }));
            }
        }

        private void FindViewByMenu(Menu menu, out object obj)
        {
            obj = null;
            switch (menu)
            {
                case Menu.Guidance:
                    obj = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.Guidance.Views.MainView).FullName);
                    break;
                case Menu.Syutsou:
                    obj = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.Syutsou.Views.MainView).FullName);
                    break;
                case Menu.Settings:
                    obj = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.Settings.Views.MainView).FullName);
                    break;
                case Menu.About:
                    obj = this.regionManager.Regions[this.MainRegion].GetView(typeof(Module.About.Views.MainView).FullName);
                    break;
                default:
                    return;
            }
        }

        private DelegateCommand _gobackCmd;
        public DelegateCommand GobackCmd
        {
            get
            {
                return _gobackCmd ?? (_gobackCmd = new DelegateCommand(() =>
                    {
                        Menu menu;
                        if (this.VmCollection.TryDequeue(out menu))
                        {
                            this.regionManager.RequestNavigate(this.MainRegion, obj.ToString());
                            this.OnPropertyChanged(() => this.VmCollection);
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
