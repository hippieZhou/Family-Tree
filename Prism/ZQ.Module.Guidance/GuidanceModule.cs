using Prism.Modularity;
using Prism.Regions;
using ZQ.Module.Guidance.Views;

namespace ZQ.Module.Guidance
{
    public class GuidanceModule : IModule
    {
        IRegionManager _regionManager;

        public GuidanceModule(IRegionManager regionManager)
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