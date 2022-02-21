using MudBlazor;

namespace BlazorPowerBI.Client.Shared.Abstraction
{
    public interface IBreadcrumbDataItems
    {
        void LoadBreadcrumb(List<BreadcrumbItem> items);
        event EventHandler OnLoad;
    }
}
