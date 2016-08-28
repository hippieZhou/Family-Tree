using Prism.Modularity;
using Prism.Regions;

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
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(Views.MainView));
        }
    }
}