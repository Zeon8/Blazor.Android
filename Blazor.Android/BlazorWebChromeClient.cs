// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Android.Content;
using Android.OS;
using Android.Webkit;
using Uri = Android.Net.Uri;

namespace Blazor.Android;

internal class BlazorWebChromeClient : WebChromeClient
{
    public override bool OnCreateWindow(WebView? view, bool isDialog, bool isUserGesture, Message? resultMsg)
    {
        if (view?.Context is not null)
        {
            // Intercept _blank target <a> tags to always open in device browser
            // regardless of UrlLoadingStrategy.OpenInWebview
            var requestUrl = view.GetHitTestResult().Extra;
            var intent = new Intent(Intent.ActionView, Uri.Parse(requestUrl));
            view.Context.StartActivity(intent);
        }

        // We don't actually want to create a new WebView window so we just return false 
        return false;
    }
}
