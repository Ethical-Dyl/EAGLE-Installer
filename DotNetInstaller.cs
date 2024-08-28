using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Management;

namespace EAGLE_Installer
{
    public class DotNetInstaller
    {
        public void dotNetInstall()
        {
            // Check and install .NET Framework 3.5
            CheckAndInstallFeature("NetFx3", ".NET Framework 3.5");

            // Check and install WCF HTTP Activities
            CheckAndInstallFeature("WCF-HTTP-Activation", "WCF HTTP Activities");

            // Check and install WCF Non-HTTP Activities
            CheckAndInstallFeature("WCF-NonHTTP-Activation", "WCF Non-HTTP Activities");

            Console.WriteLine(".NET Features installed... Press any key to continue...");
            Console.ReadKey();
        }

        public static void CheckAndInstallFeature(string featureName, string displayName)
        {
            bool isInstalled = CheckFeatureInstallation(featureName);
            Console.WriteLine($"{displayName} installed: {isInstalled}");

            if (!isInstalled)
            {
                Console.WriteLine($"Attempting to install {displayName}...");
                InstallFeature(featureName);

                // Check again after installation attempt
                isInstalled = CheckFeatureInstallation(featureName);
                Console.WriteLine($"{displayName} installed after attempt: {isInstalled}");
            }
        }

        public static bool CheckFeatureInstallation(string featureName)
        {
            string query = $"SELECT * FROM Win32_OptionalFeature WHERE Name = '{featureName}'";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                ManagementObjectCollection results = searcher.Get();
                foreach (ManagementObject result in results)
                {
                    if ((uint)result["InstallState"] == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void InstallFeature(string featureName)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dism.exe",
                    Arguments = $"/online /enable-feature /featurename:{featureName} /all",
                    Verb = "runas", // This will prompt for admin rights
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"Error installing {featureName}. Exit code: {process.ExitCode}");
                    Console.WriteLine($"Error output: {error}");
                }
                else
                {
                    Console.WriteLine($"Successfully installed {featureName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while installing {featureName}: {ex.Message}");
            }
        }
    }




}
