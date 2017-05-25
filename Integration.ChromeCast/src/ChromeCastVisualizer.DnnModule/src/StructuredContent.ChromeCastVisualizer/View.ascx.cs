using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using DotNetNuke.Web.Client.ClientResourceManagement;

namespace Evoq.StructuredContent.ChromeCastVisualizer
{
    /// <summary>
    /// Module view user control
    /// </summary>
    public partial class View : PortalModuleBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Globals.IsEditMode())
            {
                Skin.AddModuleMessage(this, LocalizeString("EditModeInfo"),
                    ModuleMessage.ModuleMessageType.BlueInfo);
            }

            // Register chrome cast sender script
            ClientResourceManager.RegisterScript(Page, "https://www.gstatic.com/cv/js/sender/v1/cast_sender.js");

            // Register custom cast sender script
            ClientResourceManager.RegisterScript(Page, "~/DesktopModules/DNNCorp/StructuredContentChromeCastVisualizer/ClientScripts/castSender.js");

            // Register custom cast sender script
            ClientResourceManager.RegisterStyleSheet(Page, "~/DesktopModules/DNNCorp/StructuredContentChromeCastVisualizer/module.css");
        }
    }
}
