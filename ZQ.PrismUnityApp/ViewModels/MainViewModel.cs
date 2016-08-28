using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;

namespace ZQ.PrismUnityApp.ViewModels
{
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
                        Menu menu = Menu.世祖;
                        if (Enum.TryParse(str, out menu))
                        {
                            switch (menu)
                            {
                                case Menu.祖训:
                                    break;
                                case Menu.世祖:
                                    break;
                                case Menu.设置:
                                    break;
                                case Menu.关于:
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
