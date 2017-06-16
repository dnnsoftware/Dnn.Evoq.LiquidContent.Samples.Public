using System;
using System.Web.Configuration;
using DotNetNuke.Framework;

namespace Evoq.Modules.LiquidContentJobs.Components
{
    public class ServiceEndPointsRepository : ServiceLocator<IServiceEndPointsRepository, ServiceEndPointsRepository>, IServiceEndPointsRepository
    {
        protected override Func<IServiceEndPointsRepository> GetFactory()
        {
            return () => new ServiceEndPointsRepository();
        }

        public string GetStructuredContentApiUrl()
        {
            return WebConfigurationManager.AppSettings[Constants.ServiceEndPointApiConfigKey];
        }
    }
}
