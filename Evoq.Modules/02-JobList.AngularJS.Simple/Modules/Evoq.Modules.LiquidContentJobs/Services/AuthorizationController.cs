using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Evoq.Modules.LiquidContentJobs.Components.Authorization;
using Evoq.Microservices.Framework.Authorization;

namespace Evoq.Modules.LiquidContentJobs.Services
{
    [DnnAuthorize(StaticRoles = "Administrators")]
    [DnnExceptionFilter]
    public class AuthorizationController : DnnApiController
    {
        private readonly ITokenService _tokenService;
        
        public AuthorizationController()
        {
            _tokenService = TokenServiceImpl.Instance;
        }

        [HttpGet]
        public HttpResponseMessage GetAuthorizationToken()
        {
            var portalId = PortalSettings.PortalId;
            var userId = UserInfo.UserID;

            var response = new
            {
                token = _tokenService.ObtainToken(portalId, userId)
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}