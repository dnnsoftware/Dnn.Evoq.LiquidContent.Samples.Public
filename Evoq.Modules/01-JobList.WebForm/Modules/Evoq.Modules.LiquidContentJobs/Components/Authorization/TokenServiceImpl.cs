using System;
using DotNetNuke.Framework;
using Evoq.Microservices.Framework.Authorization;

namespace Evoq.Modules.LiquidContentJobs.Components.Authorization
{
    public class TokenServiceImpl : ServiceLocator<ITokenService, TokenServiceImpl>
    {
        protected override Func<ITokenService> GetFactory()
        {
            return () =>
            {
                var tokenService = new TokenService(Constants.ApplicationName, Constants.ApplicationId, new ScopeManager());
                return new TokenCachedService(Constants.ApplicationId, tokenService);
            };
        }

        public static void ClearCache(int portalId, int userId)
        {
            TokenCachedService.ClearCache(Constants.ApplicationId, portalId, userId);
        }
    }
}
