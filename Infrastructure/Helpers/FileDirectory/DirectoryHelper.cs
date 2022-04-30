using NTierIA.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Infrastructure.Helpers.FileDirectory
{
    public static class DirectoryHelper
    {
        public static string CreateFolder(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                //use current path 
                throw new ArgumentNullException("strFolderPath" + " The FolderPath cannot be null or empty. ");
            }

            //If directory exists, the delete it with all of its sub-folders 
            if (!Directory.Exists(folderPath))
            {
                //Directory.Delete(strFolderPath, true); 
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        public static string GetAppKeyPath(string appKey)
        {
            try
            {
                using (var configHandler = new ConfigurationHandler())
                {
                    string AppValueByKey = configHandler.GetAppSettingsValueByKey(appKey);
                    string AppRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppValueByKey);
                    return AppRootPath;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DirectoryHelper::GetAppKeyRootPath:Error occured.", ex);
            }
        }
        public static string GetAppKeyValue(string appKey)
        {
            using (var configHandler = new ConfigurationHandler())
                return configHandler.GetAppSettingsValueByKey(appKey);
        }
    }
}