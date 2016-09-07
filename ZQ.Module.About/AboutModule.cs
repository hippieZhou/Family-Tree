using Prism.Modularity;
using Prism.Regions;

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
            #region 跳转方式
            /*                          
             * this.moduleManager.LoadModule("AboutModule");
             * obj = this.regionManager.Regions[this.MainRegion].GetView("MainView");
             * this.regionManager.Regions[this.MainRegion].Activate(obj);
             */
            // _regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.MainView));
            #endregion

            #region 跳转方式
            //_regionManager.Regions["MainView"].Add(new MainView(), "MyView1");

            // 注册View
            //RegionManager.RegisterViewWithRegion("MainView", typeof(View));
            //RegionManager.Regions["MainView"].Add(new MyView(), "MyView1");

            #endregion
        }
    }
}