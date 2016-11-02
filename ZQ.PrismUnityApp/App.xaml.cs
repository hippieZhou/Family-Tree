using System.Windows;
using ZQ.PrismUnityApp.ViewModels;
using ZQ.PrismUnityApp.Views;

namespace ZQ.PrismUnityApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var loginView = new LoginView();
            //登录
            if (null != loginView)
            {
                loginView.Show();
                var loginVm = loginView.DataContext as LoginViewModel;
                if (null != loginVm)
                {
                    loginVm.Validate += () =>
                    {
                        var bootstrapper = new Bootstrapper();
                        bootstrapper.Run();

                        loginView.Close();
                    };
                }
            }
        }
    }
}
