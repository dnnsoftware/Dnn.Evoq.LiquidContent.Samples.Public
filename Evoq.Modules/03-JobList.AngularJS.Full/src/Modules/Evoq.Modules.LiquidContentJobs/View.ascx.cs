using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Security.Roles;
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
                ScopeWrapper.Attributes["mode"] = "view";
            }
            else
            {
                var isAdmin = UserInfo.IsInRole(RoleController.Instance.GetRoleById(PortalId, PortalSettings.AdministratorRoleId).RoleName);
                authToken = TokenServiceImpl.Instance.ObtainToken(PortalSettings.PortalId, UserInfo.UserID,
                    new[] { isAdmin? Common.Constants.AdminsRoleName : Common.Constants.ContentEditorRoleName});
                ScopeWrapper.Attributes["mode"] = "edit";
            }
            ScopeWrapper.Attributes["token"] = authToken;
            ScopeWrapper.Attributes["pageSize"] = ModuleContext.Settings["PageSize"] != null ? ModuleContext.Settings["PageSize"] as string : "5";
        }
    }
}