using System;
using System.Collections.Generic;
using System.Text;

namespace HAWK.Shared.Models.ApiResponse
{
    public class Organization : BaseCollectionModel<Repository>
    {
        public string OrganizationName { get; set; }
        public bool Success { get; set; }
        public string Id { get; set; }
    }
}