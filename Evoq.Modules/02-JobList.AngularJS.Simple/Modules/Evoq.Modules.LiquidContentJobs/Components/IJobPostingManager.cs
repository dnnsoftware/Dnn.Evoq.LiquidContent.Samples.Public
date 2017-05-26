using System.Collections.Generic;
using Evoq.Modules.LiquidContentJobs.Components.Dto;

namespace Evoq.Modules.LiquidContentJobs.Components
{
    /// <summary>
    /// Component responsible to get information of the Type Content Block
    /// </summary>
    public interface IJobPostingManager
    {
        /// <summary>
        /// Get the definition of the Content Type "Job Posting"
        /// </summary>
        /// <param name="portalId">Portal Id</param>
        /// <param name="userId">User that request the operation</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="orderAsc">order asc</param>
        /// <returns>Content block type object definition</returns>
        List<JobPosting> GetJobPosting(int portalId, int userId, int pageIndex, int pageSize, bool orderAsc);
    }
}
