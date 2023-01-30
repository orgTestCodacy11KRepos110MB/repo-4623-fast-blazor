using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Microsoft.Fast.Components.FluentUI;


/// <summary>
/// Groups child <see cref="FluentRadio{TValue}"/> components.
/// </summary>

[CascadingTypeParameter(nameof(TValue))]
public partial class FluentRadioGroup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TValue> : FluentInputBase<TValue>
{
    private readonly string _defaultGroupName = Identifier.NewId();
    private FluentRadioContext? _context;

    private TValue? _initialValue;
    
    /// <summary>
    /// Gets or sets the orentation of the group. See <see cref="FluentUI.Orientation"/>
    /// </summary>
    [Parameter]
    public Orientation? Orientation { get; set; }

    /// <summary>
    /// Gets or sets the child content to be rendering inside the <see cref="FluentRadioGroup{TValue}"/>.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter] 
    private FluentRadioContext? CascadedContext { get; set; }

    protected override void OnInitialized()
    {
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        _initialValue = Value;

        // On the first render, we can instantiate the FluentRadioContext
        if (_context is null)
        {
            var changeEventCallback = EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString);
            _context = new FluentRadioContext(CascadedContext, changeEventCallback);
        }
        else if (_context.ParentContext != CascadedContext)
        {
            // This should never be possible in any known usage pattern, but if it happens, we want to know
            throw new InvalidOperationException("An FluentRadioGroup cannot change context after creation");
        }

        // Mutate the FluentRadioContext instance in place. Since this is a non-fixed cascading parameter, the descendant
        // FluentRadio/FluentRadioGroup components will get notified to re-render and will see the new values.
        _context.GroupName = !string.IsNullOrEmpty(Name) ? Name : _defaultGroupName;
        _context.CurrentValue = CurrentValue;
        _context.FieldClass = EditContext?.FieldCssClass(FieldIdentifier);
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
    => this.TryParseSelectableValueFromString(value, out result, out validationErrorMessage);
}