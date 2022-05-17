using Microsoft.AspNetCore.Components;

namespace Microsoft.Fast.Components.FluentUI;

public partial class FluentFlipper : FluentComponentBase
{
    /// <summary>
    /// Gets or set if the element is disabled
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <summary>
    /// Gets or sets the direction. See <see cref="FluentUI.Direction"/>
    /// </summary>

    [Parameter]
    public Direction? Direction { get; set; }
}