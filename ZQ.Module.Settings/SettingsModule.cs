using Prism.Modularity;
using Prism.Regions;
using System;
using ZQ.Module.Settings.Views;

namespace ZQ.Module.Settings
{
    //[Module(ModuleName = "SettingsModule")]
    public class SettingsModule : IModule
    {
        IRegionManager _regionManager;

        public SettingsModule(IRegionManager regionManager)
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