using Hawk.Api.Client.Library;
using System.Collections.Generic;

namespace HAWK.Shared.Services.AppConfigService
{
    public interface ILocalFileService
    {
        Settings ReadSettings();

        void SaveSettings(string api, string key);

        void SaveOrgTok(ICollection<OrganizationCred> orgTekList);

        public ICollection<OrganizationCred> ReadOrgTok();
    }
}