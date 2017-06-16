using System;

namespace Evoq.Modules.LiquidContentJobs.Components.Dto
{
    /// <summary>
    /// This class represents the metadata of the Content Block type defined in Structured Content
    /// </summary>
    [Serializable]
    public class JobPosting
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title field name
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Body field name
        /// </summary>
        public string Description { get; set; }
    }
}
