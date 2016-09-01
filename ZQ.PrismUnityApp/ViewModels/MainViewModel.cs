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
        private string _title = "族谱 APP";
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
                        //var obj = ServiceLocator.Current.GetInstance<IModuleCatalog>();
                        //var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                        //regionManager.RequestNavigate("ContentRegion");
                        //regionManager.Regions["SyutsouModule"].Add(Views.LoginView);
                        //var moduleManager = ServiceLocator.Current.GetInstance<IModuleManager>();

                        var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
                        var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                       

                        Menu menu = Menu.祖训;
                        if (Enum.TryParse(str, out menu))
                        {
                            switch (menu)
                            {
                                case Menu.祖训:
                                    regionManager.Regions["ContentRegion"].Activate(typeof(Module.Guidance.Views.MainView));
                                    break;
                                case Menu.世祖:
                                    regionManager.Regions["ContentRegion"].Activate(typeof(Module.Syutsou.Views.MainView));
                                    break;
                                case Menu.设置:
                                    regionManager.Regions["ContentRegion"].Activate(typeof(Module.Settings.Views.MainView));
                                    break;
                                case Menu.关于:
                                    regionManager.Regions["ContentRegion"].Activate(typeof(Module.About.Views.MainView));
                                    break;
                                default:
                                    return;
                            }
                            Debug.WriteLine(menu);
                        }
                        else
                        {
                            return;
                        }
                    }));
            }
        }


        

        public MainViewModel()
        {
        }
    }
}
