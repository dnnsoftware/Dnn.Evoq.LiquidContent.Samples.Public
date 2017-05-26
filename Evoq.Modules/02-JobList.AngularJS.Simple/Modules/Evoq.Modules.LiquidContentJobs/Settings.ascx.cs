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

            if (ModuleSettings.ContainsKey("PageSize"))
            {
                txtPageSize.Text = ModuleSettings["PageSize"].ToString();
            }
            else
            {
                txtPageSize.Text = "5";
            }
        }

        public override void UpdateSettings()
        {
            base.UpdateSettings();

            ModuleController.Instance.UpdateModuleSetting(ModuleId, "PageSize", string.IsNullOrEmpty(txtPageSize.Text) ? "5" : txtPageSize.Text);
        }

    }
}