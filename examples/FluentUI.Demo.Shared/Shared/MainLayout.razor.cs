using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.JSInterop;

namespace FluentUI.Demo.Shared
{
    public partial class MainLayout
    {
        [Inject] 
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        DesignTokens? DesignTokens { get; set; }

        ErrorBoundary? errorBoundary;
        
        LocalizationDirection? dir;
        float baseLayerLuminance=1;

        public async Task SwitchDirectionAsync()
        {
            dir = dir == LocalizationDirection.rtl ? LocalizationDirection.ltr : LocalizationDirection.rtl;
            await JSRuntime!.InvokeVoidAsync("switchDirection", dir.ToString());
        }
        
        public void SwitchTheme()
        {
            baseLayerLuminance = baseLayerLuminance == 0.2f ? 1 : 0.2f;
        }

        protected override void OnParametersSet()
        {
            errorBoundary?.Recover();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await DesignTokens!.BaseLayerLuminance.SetValueFor(".container", baseLayerLuminance);
            await DesignTokens!.Direction.SetValueFor(".container", dir == LocalizationDirection.ltr ? "ltr" : "rtl");
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}