using HAWK.Shared.Models.ApiRequest;
using HAWK.Shared.Models.ApiResponse;
using HAWK.Shared.Services.AppConfigService;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace HAWK.Shared.ViewModels
{
    public class PullRequestQueryViewModel : ReactiveObject
    {
        private readonly ILocalFileService _localFileService;
        public ReactiveCommand<Unit, Unit> RequestPullRequests { get; }

        public PullRequestResponse PullReqeustResponse { get; set; }

        public PullRequestQueryViewModel()
        {
        }

        /// <summary>
        /// Cast Response to Model
        /// </summary>
        /// <returns></returns>
        //private async Task<ICollection<Organization>> GetPullRequests()
        //{
        //    var payLoad = _localFileService.ReadOrgTok();
        //    var settings = _localFileService.ReadSettings();
        //    var response = await PullRequestQuery.GetAllPR(payLoad, settings);

        //    var OrganizationCollection = new List<Organization>();
        //    foreach (var org in response)
        //    {
        //        var repo = new List<Repository>();
        //        foreach (var rep in response)

        //    }
        //}
    }
}