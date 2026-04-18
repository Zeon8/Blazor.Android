using Android.Content;
using Android.Runtime;
using Android.Util;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blazor.Android;

public class BlazorWebView : FrameLayout
{
    public IServiceProvider ServiceProvider { get; set; }

    public string HostPage { get; set; }

    public string StartPath { get; set; } = "/";

    public RootComponentsCollection RootComponents { get; } = new();

    public BlazorWebView(Context? context) : base(context)
    {
    }

    public BlazorWebView(Context? context, IAttributeSet? attrs) : base(context, attrs)
    {
    }

    public BlazorWebView(Context? context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
    {
    }

    protected BlazorWebView(nint javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    protected override void OnAttachedToWindow() => Initialize();

    private void Initialize()
    {
        if (ServiceProvider is null)
            throw new InvalidOperationException($"{nameof(ServiceProvider)} is not set.");
        if (HostPage is null)
            throw new InvalidOperationException($"{nameof(HostPage)} is not set.");

        var contentRootDir = Path.GetDirectoryName(HostPage!) ?? string.Empty;
        var hostPageRelativePath = Path.GetRelativePath(contentRootDir, HostPage!);

        var dispatcher = new AndroidWebViewDispatcher();
        var jsComponentConfigurationStore = new JSComponentConfigurationStore();
        var logger = ServiceProvider.GetRequiredService<ILogger<BlazorWebView>>();
        var fileProvider = new AndroidFileProvider(Context!.Assets!, contentRootDir);

        var webView = new BlazorAndroidWebView(Context!, ServiceProvider);
        
        var manager = new AndroidWebKitWebViewManager(
            webView,
            ServiceProvider,
            dispatcher,
            fileProvider,
            jsComponentConfigurationStore,
            contentRootDir,
            hostPageRelativePath,
            logger);

        StaticContentHotReloadManager.AttachToWebViewManagerIfEnabled(manager);

        webView.SetWebViewClient(new WebKitWebViewClient(Context, manager));
        webView.SetWebChromeClient(new BlazorWebChromeClient());

        foreach (RootComponent rootComponent in RootComponents)
            rootComponent.AddToWebViewManagerAsync(manager);

        manager.Navigate(StartPath);
        AddView(webView);
    }
}
