using Prism.Modularity;
using Prism.Regions;
using System;

namespace ZQ.Module.Settings
{
    [Module(ModuleName = "SettingsModule")]
    public class SettingsModule : IModule
    {
        IRegionManager _regionManager;

        public SettingsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.MainView));
        }
    }
}