using Microsoft.Practices.ServiceLocation;
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
        

        private DelegateCommand<string> _chooseMenuCmd;
        public DelegateCommand<string> ChooseMenuCmd
        {
            get
            {
                return _chooseMenuCmd ?? (_chooseMenuCmd = new DelegateCommand<string>((str) =>
                    {
                        if (this.Title != str)
                        {
                            Menu menu;
                            if (Enum.TryParse(str, out menu))
                            {
                                object obj = FindViewByMenu(menu);
                                this.regionManager.Regions[this.MainRegion].Activate(obj);

                                this.Title = str;
                                Debug.WriteLine(str);
                            }
                        }
                    }));
            }
        }

        private object FindViewByMenu(Menu menu)
        {
            object obj = null;
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
                    break;
            }
            return obj;
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