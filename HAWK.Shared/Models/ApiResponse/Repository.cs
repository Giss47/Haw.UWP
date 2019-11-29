using System;
using System.Collections.Generic;
using System.Text;

namespace HAWK.Shared.Models.ApiResponse
{
    public class Repository : BaseCollectionModel<PullRequest>
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}