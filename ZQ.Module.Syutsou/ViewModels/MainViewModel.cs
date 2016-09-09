using Prism.Mvvm;
using Prism.Regions;

namespace ZQ.Module.Syutsou.ViewModels
{
    public class MainViewModel:BindableBase,INavigationAware
    {
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
