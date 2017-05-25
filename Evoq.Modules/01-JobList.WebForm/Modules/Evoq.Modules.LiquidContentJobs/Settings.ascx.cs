using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;

namespace Evoq.Modules.LiquidContentJobs
{
    public partial class Settings : ModuleSettingsBase
    {
        public override void LoadSettings()
        {
            base.LoadSettings();

            if (ModuleSettings.ContainsKey("Days"))
            {
                txtDays.Text = ModuleSettings["Days"].ToString();
            }
            else
            {
                txtDays.Text = "5";
            }
        }

        public override void UpdateSettings()
        {
            base.UpdateSettings();

            ModuleController.Instance.UpdateModuleSetting(ModuleId, "Days", string.IsNullOrEmpty(txtDays.Text) ? "5" : txtDays.Text);
        }

    }
}