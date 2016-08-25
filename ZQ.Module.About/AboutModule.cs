using Prism.Modularity;
using Prism.Regions;
using System;

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
            throw new NotImplementedException();
        }
    }
}