using Prism.Modularity;
using Prism.Regions;
using ZQ.Module.Other.Views;

namespace ZQ.Module.Other
{
    public class OtherModule : IModule
    {
        IRegionManager _regionManager;
        public OtherModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.Regions["OtherRegion"].Add(new MainView(), typeof(MainView).FullName);
        }
    }
}
