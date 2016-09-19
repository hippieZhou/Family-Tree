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
        Guidance = 0x00,
        Syutsou = 0x01,
        Settings = 0x02,
        About = 0x03
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
            get
            {
                if (_vmCollection == null)
                {
                    _vmCollection = new ConcurrentQueue<Menu>();
                    _vmCollection.Enqueue(Menu.Guidance);
                }
                return _vmCollection;
            }
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
                                    Debug.WriteLine(menu);

                                    this.VmCollection.Enqueue(menu);
                                    this.regionManager.Regions[this.MainRegion].Activate(obj);
                                    this.OnPropertyChanged(() => this.VmCollection);
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
                            object obj = null;
                            FindViewByMenu(menu, out obj);
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
            Debug.WriteLine(string.Format("当前加载进度：{0}", e.BytesReceived.ToString()));
        }
        void moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            Debug.WriteLine("模块加载完毕！");
        }
    }
}