using System;
using System.Collections.Generic;
using System.Text;

namespace HAWK.Shared.Models.ApiResponse
{
    public class PullRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public Uri Url { get; set; }
        public Uri ImageUrl { get; set; }

        public int ID { get; set; }
    }
}