using System.Collections.Generic;
using System.Linq;
using Evoq.Microservices.Framework.Authorization;

namespace Evoq.Modules.LiquidContentJobs.Components.Authorization
{
    public class ScopeManager : IScopeManager
    {
        private static readonly string[] DefaultScopes =
        {
            GetScopeIdentifier(Constants.ContentTypeResourceName, Constants.ReadAction),
            GetScopeIdentifier(Constants.ContentItemResourceName, Constants.ReadAction)
        };

        private static string GetScopeIdentifier(string resourceName, string action)
        {
            return ScopeFormatHelper.FormatScopeIdentifier(resourceName, action);
        }

        public string GetScopeByRoles(string[] roles)
        {
            var scopes = new HashSet<string>();
            foreach (var scope in DefaultScopes)
            {
                scopes.Add(scope);
            }

            return ScopeFormatHelper.JoinScopes(scopes.ToArray());
        }
    }
}
