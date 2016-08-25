using Prism.Modularity;
using Prism.Regions;
using System;

namespace ZQ.PrismModuleA
{
    public class PrismModuleAModule : IModule
    {
        IRegionManager _regionManager;

        public PrismModuleAModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}