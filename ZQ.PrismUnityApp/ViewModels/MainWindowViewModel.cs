using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;

namespace ZQ.PrismUnityApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
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
                        Debug.WriteLine(str);
                    }));
            }
        }


        

        public MainWindowViewModel()
        {
        }
    }
}
