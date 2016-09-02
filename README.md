# Family-Tree(Prism Tips)

## 1. 框架搭建
### 1.1 安装 prism 包（通过NuGet Packages 管理器来安装prism6 程序包）
### 1.2 App->Bootstrapper->Shell
App修改如下：
```xaml
<Application x:Class="ZQ.PrismUnityApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:ZQ.PrismUnityApp">
</Application>
```
```C#
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var bootstrapper = new Bootstrapper();
        bootstrapper.Run();
    }
}
```
创建Bootstrapper类
```C#
class Bootstrapper : UnityBootstrapper
{
    protected override DependencyObject CreateShell()
    {
        return Container.TryResolve<Shell>();
    }

    protected override void InitializeShell()
    {
        Application.Current.MainWindow.Show();
    }

    protected override void ConfigureModuleCatalog()
    {
        #region 基于代码方式的模块加载方法
        var typeGuidance = typeof(Module.Guidance.GuidanceModule);
        var guidanceModule = new ModuleInfo()
        {
            ModuleName = typeGuidance.Name,
            ModuleType = typeGuidance.AssemblyQualifiedName
        };

        var typeSyutsou = typeof(Module.Syutsou.SyutsouModule);
        var syutsouModule = new ModuleInfo()
        {
            ModuleName = typeSyutsou.Name,
            ModuleType = typeSyutsou.AssemblyQualifiedName
        };

        var typeSettings = typeof(Module.Settings.SettingsModule);
        var settingsModule = new ModuleInfo()
        {
            ModuleName = typeSettings.Name,
            ModuleType = typeSettings.AssemblyQualifiedName
        };

        var typeAbout = typeof(Module.About.AboutModule);
        var aboutModule = new ModuleInfo()
        {
            ModuleName = typeAbout.Name,
            ModuleType = typeAbout.AssemblyQualifiedName
        };

        this.ModuleCatalog.AddModule(guidanceModule);
        this.ModuleCatalog.AddModule(syutsouModule);
        this.ModuleCatalog.AddModule(settingsModule);
        this.ModuleCatalog.AddModule(aboutModule);

        #endregion
    }
}
```
创建主Shell
```xaml
 <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="120"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>
        <Grid Grid.Column="0" Background="AliceBlue">
            <StackPanel VerticalAlignment="Bottom">
                <RadioButton IsChecked="True" GroupName="rb_Menu" Content="祖训" Command="{Binding ChooseMenuCmd}" CommandParameter="{Binding Content, RelativeSource={RelativeSource Self}}"/>
                <RadioButton GroupName="rb_Menu" Content="世祖" Command="{Binding ChooseMenuCmd}" CommandParameter="{Binding Content, RelativeSource={RelativeSource Self}}"/>
                <RadioButton GroupName="rb_Menu" Content="设置" Command="{Binding ChooseMenuCmd}" CommandParameter="{Binding Content, RelativeSource={RelativeSource Self}}"/>
                <RadioButton GroupName="rb_Menu" Content="关于" Command="{Binding ChooseMenuCmd}" CommandParameter="{Binding Content, RelativeSource={RelativeSource Self}}"/>
            </StackPanel>
        </Grid>
        <TabControl SelectedIndex="{Binding ModuleIndex}" Grid.Column="1" prism:RegionManager.RegionName="{StaticResource MainRegion}" />
    </Grid>
```
创建主Shell对应的ViewModel
```C#
public enum Menu
{
    祖训 = 0x01,
    世祖 = 0x02,
    设置 = 0x03,
    关于 = 0x04
};

public class MainViewModel : BindableBase
{
    private string _title = "Prism APP";
    public string Title
    {
        get { return _title; }
        set { SetProperty(ref _title, value); }
    }

    private int _moduleIndex;
    public int ModuleIndex
    {
        get { return _moduleIndex; }
        set { SetProperty(ref _moduleIndex, value); }
    }

    private DelegateCommand<string> _chooseMenuCmd;
    public DelegateCommand<string> ChooseMenuCmd
    {
        get
        {
            return _chooseMenuCmd ?? (_chooseMenuCmd = new DelegateCommand<string>((str) =>
                {
                    Menu menu = Menu.关于;
                    if (Enum.TryParse(str, out menu))
                    {
                        switch (menu)
                        {
                            case Menu.祖训:
                                this.ModuleIndex = 0;
                                break;
                            case Menu.世祖:
                                this.ModuleIndex = 1;
                                break;
                            case Menu.设置:
                                this.ModuleIndex = 2;
                                break;
                            case Menu.关于:
                                this.ModuleIndex = 3;
                                break;
                            default:
                                return;
                        }
                        Debug.WriteLine(menu);
                    }
                }));
        }
    }
}
```
各个子模块进行注册
由于每个模块的注册方法都类似，代码也基本相同，这里简单列出设置模块的示例代码：
```C#
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
```

## 2. 模块注册
模块注册有多种方式，典型的注册方式有两种（从目前个人接触的来看，：））
### 2.1 基于配置文件的注册方式
### 2.2 基于代码的注册方式
## 3. 模块切换
## 4. 事件通知
