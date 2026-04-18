# Blazor.Android

Blazor.Android is a library that enables hosting Blazor applications inside an Android WebView without .NET MAUI.

## Features
- Host Blazor in Android WebView
- No MAUI dependency
- Lightweight and fast startup
- Smaller binary size

## Getting Started

### Usage

1. Create empty Android Application project in VS or using dotnet CLI
2. Change project SDK to `Microsoft.NET.Sdk.Razor`
```patch
--<Project Sdk="Microsoft.NET.Sdk">
++<Project Sdk="Microsoft.NET.Sdk.Razor">
```
3. Create Blazor Webassembly Standalone project and copy all files except Program.cs from it to the application project.
4. Update script in the `index.html`
```patch
--<script src="_framework/blazor.webassembly.js"></script>
++<script src="_framework/blazor.webview.js" autostart="false"></script>
```
5. Update class `MainActivity`
```
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
```

See [Blazor.Android.Sample](./Blazor.Android.Sample)

## Why Not MAUI Hybrid?

- Smaller binaries
- Faster application startup
- Fater compilation times

## License

[MIT](LICENSE.md)
