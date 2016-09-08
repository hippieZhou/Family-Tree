using Prism.Modularity;
using Prism.Regions;
using ZQ.Module.About.Views;

namespace ZQ.Module.About
{
    public class AboutModule : IModule
    {
        IRegionManager _regionManager;

        public AboutModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            #region 跳转方式1
            // this.moduleManager.LoadModule("AboutModule");
            // obj = this.regionManager.Regions[this.MainRegion].GetView("MainView");
            // this.regionManager.Regions[this.MainRegion].Activate(obj);
            // _regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.MainView));
            #endregion

            #region 跳转方式2
            //手动注册模块对应的页面
            _regionManager.Regions["MainRegion"].Add(new MainView(), typeof(MainView).FullName);
            #endregion
        }
    }
}