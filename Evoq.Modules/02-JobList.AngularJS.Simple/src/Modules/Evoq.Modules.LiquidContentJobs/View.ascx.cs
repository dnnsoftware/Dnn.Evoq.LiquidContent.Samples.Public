using System;
using DotNetNuke.Entities.Modules;
using Evoq.Modules.LiquidContentJobs.Components.Authorization;

namespace Evoq.Modules.LiquidContentJobs
{
    public partial class View : PortalModuleBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ScopeWrapper.Attributes["token"] = TokenServiceImpl.Instance.ObtainToken(PortalSettings.PortalId, UserInfo.UserID);
            ScopeWrapper.Attributes["pageSize"] = ModuleContext.Settings["PageSize"] != null ? ModuleContext.Settings["PageSize"] as string : "5";
        }
    }
}