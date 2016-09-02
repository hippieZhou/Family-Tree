using Prism.Modularity;
using Prism.Regions;
using System;

namespace ZQ.Module.Settings
{
    public class SettingsModule : IModule
    {
        IRegionManager _regionManager;

        public SettingsModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.MainView));
        }
    }
}