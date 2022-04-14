using Microsoft.Win32;
using System;
using System.DirectoryServices.AccountManagement;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace NativeMessaging
{
    /// <summary>
    /// Represent any browser derived from Google's Chromium.
    /// </summary>
    public partial class ChromiumBrowser
    {
        private readonly string regHostnameKeyLocation;

        /// <summary>
        /// The name of the browser application.
        /// </summary>
        public string BrowserName
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new <see cref="ChromiumBrowser"/> object.
        /// </summary>
        /// <param name="browserName">The name of the browser application.</param>
        /// <param name="RegKeyBaseLocation">Base location for the browser settigns in the Windows Registry.</param>
        public ChromiumBrowser(string browserName, string RegKeyBaseLocation)
        {
            BrowserName = browserName;
            regHostnameKeyLocation = RegKeyBaseLocation + "NativeMessagingHosts\\";
        }

        /// <summary>
        /// Checks if the host is registered with the browser
        /// </summary>
        /// <param name="ManifestPath">Path to the Native Messaging Host manifest file</param>
        /// <param name="Hostname">The hostname for the Native Messaging Host application</param>
        /// <returns><see langword="true"/> if the required information is present in the registry.</returns>
        public bool IsRegistered(string Hostname, string ManifestPath)
        {
            string targetKeyPath = regHostnameKeyLocation + Hostname;

            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(targetKeyPath, true);

            if (regKey != null && regKey.GetValue("").ToString() == ManifestPath)
                return true;

            return false;
        }

        /// <summary>
        /// Register the application to open with the browser.
        /// </summary>
        /// <param name="Hostname">The hostname for the Native Messaging Host application</param>
        /// <param name="ManifestPath">Path to the Native Messaging Host manifest file</param>
        public void Register(string Hostname, string ManifestPath)
        {
            string targetKeyPath = regHostnameKeyLocation + Hostname;

            RegistryKey localKey;
            if (Environment.Is64BitOperatingSystem)
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            RegistryKey regKey = localKey.OpenSubKey(targetKeyPath, true);

            if (regKey == null)
                regKey = localKey.CreateSubKey(targetKeyPath, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.Volatile);

            regKey.SetValue("", ManifestPath, RegistryValueKind.String);

            regKey.Close();

            Log.LogMessage("Registered host (" + Hostname + ") with browser " + BrowserName);
        }

        /// <summary>
        /// De-register the application to open with the browser.
        /// </summary>
        /// <param name="Hostname">The hostname for the Native Messaging Host application</param>
        public void Unregister(string Hostname)
        {
            string targetKeyPath = regHostnameKeyLocation + Hostname;

            RegistryKey localKey;
            if (Environment.Is64BitOperatingSystem)
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            RegistryKey regKey = localKey.OpenSubKey(targetKeyPath, true);

            if (regKey != null)
                regKey.DeleteSubKey("", true);
            regKey?.Close();

            Log.LogMessage("Unregistered host (" + Hostname + ") with browser " + BrowserName);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return BrowserName;
        }                   
     
    }
}
