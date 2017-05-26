using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework;
using Evoq.Microservices.Framework.Authorization;
using Evoq.Modules.LiquidContentJobs.Components.Authorization;
using Evoq.Modules.LiquidContentJobs.Components.Dto;
using Newtonsoft.Json.Linq;

namespace Evoq.Modules.LiquidContentJobs.Components
{
    public class JobPostingManager : ServiceLocator<IJobPostingManager, JobPostingManager>, IJobPostingManager
    {
        private readonly ITokenService _tokenService;

        public JobPostingManager()
        {
            _tokenService = TokenServiceImpl.Instance;
        }

        protected override Func<IJobPostingManager> GetFactory()
        {
            return () => new JobPostingManager();
        }

        public List<JobPosting> GetJobPosting(int portalId, int userId, int pageIndex, int pageSize, bool orderAsc)
        {
            var uri = GetApiUri();
            var token = _tokenService.ObtainToken(portalId, userId);
            var typeId = GetJobPostingType(portalId, userId);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync($"api/PublishedContentItems?contentTypeId={typeId}&startIndex={pageIndex}&maxItems={pageSize}&orderAsc={orderAsc}&fieldOrder=createdAt").Result;
                if (!response.IsSuccessStatusCode)
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    throw new HttpException((int)response.StatusCode, $"{response.ReasonPhrase} - {error}");
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var result = Json.Deserialize<dynamic>(json);

                var documents = (JArray)result.documents;
                var jobs = documents.Select(x => new JobPosting
                {
                    Id = (string)x["id"],
                    Title = (string)x["details"]["jobTitle"],
                    Description = (string)x["details"]["description"]
                }).ToList();

                return jobs;
            }
        }

        private string GetJobPostingType(int portalId, int userId)
        {
            var uri = GetApiUri();
            var token = _tokenService.ObtainToken(portalId, userId);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                const string type = "Job Posting";
                var response = client.GetAsync($"api/ContentTypes?searchtext={type}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    throw new HttpException((int)response.StatusCode, $"{response.ReasonPhrase} - {error}");
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var result = Json.Deserialize<dynamic>(json);

                var documents = (JArray)result.documents;
                dynamic jobPostingType = documents.SingleOrDefault(t =>
                    (bool)t["isSystem"] &&
                    (string)t["name"] == type);

                return jobPostingType == null ? "" : (string)jobPostingType.id;
            }
        }

        private static string GetApiUri()
        {
            var uri = ServiceEndPointsRepository.Instance.GetStructuredContentApiUrl();
            return uri.EndsWith("/") ? uri : uri + "/";
        }
    }
}
