
namespace Evoq.Modules.LiquidContentJobs.Components
{
    /// <summary>
    /// Manage the settings for Form Builder
    /// </summary>
    public interface ISettingService
    {
        /// <summary>
        /// Returns true is form builder is enabled on given portal, false otherwise
        /// </summary>
        /// <param name="portalId">portal identifier</param>
        /// <returns></returns>
        bool IsFormBuilderEnabled(int portalId);

        /// <summary>
        /// Enables form builder on a given portal
        /// </summary>
        /// <param name="portalId">portal identifier</param>
        void EnableFormBuilder(int portalId);
    }
}
