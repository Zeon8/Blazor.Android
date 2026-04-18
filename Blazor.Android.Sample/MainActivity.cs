using Blazor.Android.Sample.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Android.Sample;

[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@android:style/Theme.Light.NoTitleBar")]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        var collection = new ServiceCollection();
        collection.AddBlazorWebView();
        var provider = collection.BuildServiceProvider();

        var webView = new BlazorWebView(this);
        webView.RootComponents.Add<App>("#app");
        webView.HostPage = "wwwroot/index.html";
        webView.ServiceProvider = provider;

        SetContentView(webView);
    }
}