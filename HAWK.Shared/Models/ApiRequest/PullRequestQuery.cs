using Hawk.Api.Client.Library;
using Hawk.Api.Client.Library.Models.PullRequest.Request;
using Hawk.Api.Client.Library.Models.PullRequest.Response;
using Hawk.Api.Client.Models.PullRequest.Request;
using HAWK.Shared.Services.AppConfigService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HAWK.Shared.Models.ApiRequest
{
    public class PullRequestQuery
    {
        public static async Task<ICollection<Hawk.Api.Client.Models.PullRequest.Response.Organization>> GetAllPR(ICollection<OrganizationCred> orgTokList, Settings settings)
        {
            PullRequestRequest payload = new PullRequestRequest();

            foreach (var OrgTok in orgTokList)
            {
                payload.Organizations.Add(new Organization { Name = OrgTok.Name, AccessToken = OrgTok.AccessToken });
            }

            PullRequestResponse response = await Hawk.Api.Client.Hawk.GetPullRequestsAsync(payload, settings);

            return response.Organizations;
        }
    }
}