﻿using Prism.Modularity;
using Prism.Regions;

namespace ZQ.Module.About
{
    public class AboutModule : IModule
    {
        IRegionManager _regionManager;

        public AboutModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(Views.MainView));
        }
    }
}