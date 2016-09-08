using Prism.Modularity;
using Prism.Regions;
using ZQ.Module.Syutsou.Views;

namespace ZQ.Module.Syutsou
{
    public class SyutsouModule : IModule
    {
        IRegionManager _regionManager;

        public SyutsouModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            //_regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.MainView));

            _regionManager.Regions["MainRegion"].Add(new MainView(), typeof(MainView).FullName);
        }
    }
}