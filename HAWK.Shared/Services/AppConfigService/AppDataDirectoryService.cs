using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HAWK.Shared.Services.AppConfigService
{
    public class AppDataDirectoryService : IAppDataDirectoryService
    {
        readonly StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public void CreateDataFiles()
        {
            var hawkDirectoryPath = Path.Combine(localFolder.Path, "Hawk");

            if (!Directory.Exists(hawkDirectoryPath))
            {
                Directory.CreateDirectory(hawkDirectoryPath);
            }

            CreateFiles(hawkDirectoryPath, new string[] { "Settings.json", "OrgToken.json" });
        }

        private void CreateFiles(string directory, string[] fileNames)
        {
            foreach (var fileName in fileNames)
            {
                var filePath = Path.Combine(directory, fileName);
                using FileStream fs1 = new FileStream(filePath, FileMode.OpenOrCreate);
            }
        }
    }
}