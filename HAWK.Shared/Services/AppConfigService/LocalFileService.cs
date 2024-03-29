﻿using Hawk.Api.Client.Library;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;

namespace HAWK.Shared.Services.AppConfigService
{
    class LocalFileService : ILocalFileService
    {
        private static readonly string HawkDataFilesPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Hawk");
        private static readonly string SettingsPath = Path.Combine(HawkDataFilesPath, "Settings.json");
        private static readonly string OrgTokenPath = Path.Combine(HawkDataFilesPath, "OrgToken.json");

        /// <summary>
        /// Reads API URL and KEY from a local json file asynchronous
        /// </summary>
        /// <returns>
        /// Setting object with basurl and apiKey Fileds
        /// </returns>
        public Settings ReadSettings()
        {
            return LoadObjectFromJsonFile<Settings>(SettingsPath);
        }

        /// <summary>
        /// Reads list of Organizations and access Tokens from a local json file asynchronous
        /// </summary>
        /// <returns>
        /// A dictionary with Organization name as a key and access token as a value
        /// </returns>
        public ICollection<OrganizationCred> ReadOrgTok()
        {
            return LoadObjectFromJsonFile<ICollection<OrganizationCred>>(OrgTokenPath);
        }

        /// <summary>
        /// Write API uri and key to the local a json file
        /// </summary>
        /// <param name="org"></param>
        /// <param name="tok"></param>
        public void SaveSettings(string baseUrl, string apikey)
        {
            SaveObjectToJsonFile(new Settings(baseUrl, apikey), SettingsPath);
        }

        /// <summary>
        /// Write Organization name and Token to the local a json file
        /// </summary>
        /// <param name="org"></param>
        /// <param name="tok"></param>
        public void SaveOrgTok(ICollection<OrganizationCred> orgTokList)
        {
            SaveObjectToJsonFile(orgTokList, OrgTokenPath);
        }

        private static void SaveObjectToJsonFile<T>(T value, string fileName)
        {
            var json = JsonConvert.SerializeObject(value);
            File.WriteAllText(fileName, json);
        }

        private static T LoadObjectFromJsonFile<T>(string fileName)
        {
            var json = File.ReadAllText(fileName);

            return string.IsNullOrEmpty(json)
                ? default
                : JsonConvert.DeserializeObject<T>(json);
        }
    }

    public class OrganizationCred
    {
        public string Name { get; set; }

        public string AccessToken { get; set; }
    }
}