using DotNetNuke.Web.Api;
using Evoq.Modules.LiquidContentJobs.Components;

namespace Evoq.Modules.LiquidContentJobs.Services
{
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(Constants.PackageName, 
                "default", 
                "{controller}/{action}", 
                new[] { GetType().Namespace });
        }
    }
}