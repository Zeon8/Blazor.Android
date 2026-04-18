// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor.Android;

/// <summary>
/// A collection of <see cref="RootComponent"/> items.
/// </summary>
public class RootComponentsCollection : ObservableCollection<RootComponent>, IJSComponentConfiguration
{
    /// <inheritdoc />
    public JSComponentConfigurationStore JSComponents { get; } = new();
}
