namespace Evoq.Modules.LiquidContentJobs.Components.Authorization
{
    /// <summary>
    /// It generates a key for a token to be used in the UI for 
    /// its client side storage (i.e.: session storage key, local storage key)
    /// </summary>
    public class TokenKeyGenerator
    {
        /// <summary>
        /// Get a token key hash to be used in the UI for client side storage
        /// (i.e.: session storage key, local storage key)
        /// </summary>
        /// <param name="portalId">portal identifier</param>
        /// <param name="userId">user idenfier</param>
        /// <returns>token key hash</returns>
        public static string GetTokenKeyHash(int portalId, int userId)
        {
            return
                Microservices.Library.Components.Authorization.TokenKeyGenerator.GetTokenKeyHash(
                    Constants.ApplicationId, portalId, userId);
        }
    }
}
