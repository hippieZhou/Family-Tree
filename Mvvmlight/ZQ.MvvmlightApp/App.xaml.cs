using System;
using System.Diagnostics;
using System.Windows;
using ZQ.MvvmlightApp.ViewModels;
using ZQ.MvvmlightApp.Views;

namespace ZQ.MvvmlightApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            var loginVm = loginView.DataContext as LoginViewModel;
            if (null != loginVm)
            {
                loginVm.ValiateEvent += (time) =>
                {
                    loginView.Close();
                    Debug.WriteLine(string.Format("当前登陆时间：{0}", time));
                };
            }

            //this.StartupUri = new Uri("Views/MainView.xaml", UriKind.RelativeOrAbsolute);
            base.OnStartup(e);
        }
    }
}
