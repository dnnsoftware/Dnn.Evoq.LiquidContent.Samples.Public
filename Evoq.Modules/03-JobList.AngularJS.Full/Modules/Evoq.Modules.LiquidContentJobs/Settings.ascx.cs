using DotNetNuke.Entities.Modules;

namespace Evoq.Modules.LiquidContentJobs
{
    public partial class Settings : ModuleSettingsBase
    {
        public override void LoadSettings()
        {
            base.LoadSettings();
            txtPageSize.Text = ModuleSettings.ContainsKey("PageSize") ? ModuleSettings["PageSize"].ToString() : "5";
        }

        public override void UpdateSettings()
        {
            base.UpdateSettings();
            ModuleController.Instance.UpdateModuleSetting(ModuleId, "PageSize", string.IsNullOrEmpty(txtPageSize.Text) ? "5" : txtPageSize.Text);
        }

    }
}