using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Evoq.Modules.LiquidContentJobs.Components;

namespace Evoq.Modules.LiquidContentJobs.Services
{
    [DnnAuthorize]
    [DnnExceptionFilter]
    public class JobPostingController : DnnApiController
    {
        [HttpGet]
        public HttpResponseMessage GetJobPosting(int pageIndex, int pageSize, bool orderAsc = false)
        {
            var jobs = JobPostingManager.Instance.GetJobPosting(PortalSettings.PortalId, UserInfo.UserID, pageIndex, pageSize, orderAsc);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                jobPosting = jobs.Select(j => new
                {
                    j.Id,
                    j.Title,
                    j.Description
                })
            });
        }
    }
}
