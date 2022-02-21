using BlazorPowerBI.Client.Shared.Abstraction;
using MudBlazor;

namespace BlazorPowerBI.Client.Shared
{
    public class BreadcrumbDataItems : IBreadcrumbDataItems
    {
        public void LoadBreadcrumb(List<BreadcrumbItem> items)
        {
            OnLoad?.Invoke(items, new EventArgs());
        }

        public event EventHandler OnLoad;
    }
}
