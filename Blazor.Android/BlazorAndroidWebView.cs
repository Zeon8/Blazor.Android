// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Android.Content;
using Android.Views;
using AWebView = Android.Webkit.WebView;

namespace Blazor.Android;

/// <summary>
/// A Blazor Web View implemented using <see cref="AWebView"/>.
/// </summary>
internal class BlazorAndroidWebView : AWebView
{
    internal bool BackNavigationHandled { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="BlazorAndroidWebView"/>
    /// </summary>
    /// <param name="context">The <see cref="Context"/>.</param>
    public BlazorAndroidWebView(Context context, IServiceProvider serviceProvider) : base(context)
    {
        Settings.JavaScriptEnabled = true;
        Settings.AllowFileAccess = true;
        Settings.AllowContentAccess = true;
        Settings.AllowUniversalAccessFromFileURLs = true;
        Settings.AllowFileAccessFromFileURLs = true;
    }

    public override bool OnKeyDown(Keycode keyCode, KeyEvent? e)
    {
        if (keyCode == Keycode.Back && CanGoBack() && e?.RepeatCount == 0)
        {
            GoBack();
            BackNavigationHandled = true;
            return true;
        }
        BackNavigationHandled = false;
        return false;
    }
}