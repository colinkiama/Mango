using Mango.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Mango.App
{
    public sealed class appVersionChecker
    {
        static PackageVersion pv = Package.Current.Id.Version;
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;



        public static appVersionStatus getAppVersionStatus()
        {
            

            var currentVersionStatus = appVersionStatus.Current;
            string applicationVersion = $"{pv.Major}.{pv.Minor}.{pv.Build}.{pv.Revision}";
            object currentAppVersion = localSettings.Values["currentAppVersion"];

            if (currentAppVersion == null)
            {
                localSettings.Values["currentAppVersion"] = applicationVersion;
                currentVersionStatus = appVersionStatus.FirstTime;

            }

            else if (currentAppVersion.ToString() != applicationVersion)
            {
                localSettings.Values["currentAppVersion"] = applicationVersion;
                currentVersionStatus = appVersionStatus.Old;
            }

            return currentVersionStatus;
        }



    }
    
}
