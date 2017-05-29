using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Permissions;
using Evoq.Modules.LiquidContentJobs.Components.Authorization;

namespace Evoq.Modules.LiquidContentJobs
{
    public partial class View : PortalModuleBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string authToken;
            if (!ModulePermissionController.CanEditModuleContent(ModuleConfiguration))
            {
                authToken = TokenServiceImpl.Instance.ObtainToken(PortalSettings.PortalId, UserInfo.UserID);
                ScopeWrapper.Attributes["mode"] = "0";
            }
            else
            {
                authToken = TokenServiceImpl.Instance.ObtainToken(PortalSettings.PortalId, UserInfo.UserID,
                    new[] {Common.Constants.ContentEditorRoleName});
                ScopeWrapper.Attributes["mode"] = "1";
            }
            ScopeWrapper.Attributes["token"] = authToken;
            ScopeWrapper.Attributes["pageSize"] = ModuleContext.Settings["PageSize"] != null ? ModuleContext.Settings["PageSize"] as string : "5";
        }
    }
}