//-----------------------------------------------------------------------
// <copyright file="UpdateNotification.cs" company="Guillaume Puyal">
//  The Importer Hub for CRYENGINE is free for any use and open source
//  under Creative Common license (CC BY 4.0).
//
//  In short, you are free to do whatever you want
//  as long as you leave the credits.
//
//  This tool uses the Crytek's Resource Compiler and the Magick.NET
//  library with the following license: http:// magick.codeplex.com/license
//  You can access to the full source code on GitHub.
// </copyright>
//-----------------------------------------------------------------------
namespace CRYENGINE_ImportHub
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;

    /// <summary>
    /// Class to check for program updates.
    /// </summary>
    public class UpdateNotification
    {
        /// <summary>
        /// URL of the update checker.
        /// </summary>
        private const string UpdateUrl = "http://www.guillaume-puyal.com/systems/CEI_check_update.php";

        /// <summary>
        /// Saves the current version of the application.
        /// </summary>
        private string version;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNotification"/> class.
        /// </summary>
        /// <param name="version">Current version of the application in format {major}.{minor}</param>
        public UpdateNotification(string version)
        {
            this.version = version;
            Framework.Log("Beginning update check");

            var values = this.RetrieveNewVersionInfo();
            if (string.IsNullOrEmpty(values))
            {
                Framework.Log("No update available");
                return;
            }

            var valueArray = values.Split(';');
            var newUpdateUrl = valueArray[1];
            var newUpdateVersion = valueArray[0];

            Framework.Log("New Version available {0}", newUpdateVersion);
            this.ShowDialog(newUpdateVersion, newUpdateUrl);
        }

        /// <summary>
        /// Retrieve information about the newest build available.
        /// </summary>
        /// <returns>A string with version information in the format {major}.{minor};{download url}</returns>
        /// <remarks>Returns <see cref="String.Empy"/> in case no new version is available.</remarks>
        private string RetrieveNewVersionInfo()
        {
            var requestUrl = UpdateNotification.UpdateUrl + "?v=" + this.version;
            var webRequest = WebRequest.Create(requestUrl);
            webRequest.Proxy = WebRequest.GetSystemWebProxy();
            webRequest.Timeout = 6000;

            try
            {
                var objStream = webRequest.GetResponse().GetResponseStream();
                using (var objReader = new StreamReader(objStream))
                {
                    return objReader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Framework.Log("ERROR: Unable to access the online update system. {0}", ex.Message);
            }

            return string.Empty;
        }

        /// <summary>
        /// Prompt the user to download the new version.
        /// </summary>
        /// <param name="updateVersion">New version available.</param>
        /// <param name="updateURL">URL to download the new version.</param>
        private void ShowDialog(string updateVersion, string updateURL)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to access to the download page of the " + updateVersion + " update?", "New update available!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(updateURL);
            }
        }
    }
}