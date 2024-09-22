using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace NhaHang.WebHost.Extensions
{
    public class ApiExplorerIgnores : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action.Controller.ControllerName.Contains("Home"))
                action.ApiExplorer.IsVisible = false;
        }
    }
}
