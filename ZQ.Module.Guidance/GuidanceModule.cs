using Prism.Modularity;
using Prism.Regions;

namespace ZQ.Module.Guidance
{
    public class GuidanceModule : IModule
    {
        IRegionManager _regionManager;

        public GuidanceModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.MainView));
        }
    }
}