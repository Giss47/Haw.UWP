using Hawk.Api.Client.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAWK.Shared.Services.AppConfigService
{
    public interface ILocalFileService
    {
        Settings ReadSettings();

        void SaveSettings(string api, string key);

        void SaveOrgTok(Dictionary<string, string> orgTekList);

        Dictionary<string, string> ReadOrgTok();
    }
}