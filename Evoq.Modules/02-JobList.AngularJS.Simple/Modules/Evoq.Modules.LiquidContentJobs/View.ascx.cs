using System;
using System.Text;
using DotNetNuke.Entities.Modules;
using Evoq.Modules.LiquidContentJobs.Components;

namespace Evoq.Modules.LiquidContentJobs
{
    public partial class View : PortalModuleBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ScopeWrapper.InnerHtml = RenderContent();
        }

        private string RenderContent()
        {
            var jobs = JobPostingManager.Instance.GetJobPosting(PortalSettings.PortalId, UserInfo.UserID);
            var content = new StringBuilder();
            foreach (var job in jobs)
            {
                content.AppendLine($"<b>JOB TITLE</b><br/>{job.Title}<br/><b>DESCRIPTION</b>{job.Description}");
            }
            return content.ToString();
        }
    }
}