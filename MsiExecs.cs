using System;
using System.Diagnostics;
using System.Security.Principal;

namespace EAGLE_Installer
{
    public class MsiExecs
    {
        public static void ExecuteMSIs()
        {
            if (!IsAdministrator())
            {
                // Restart the application with administrator privileges
                RestartAsAdministrator();
                return;
            }

            string[] commands = new string[]
            {
                @"msiexec /i C:\Windows\System32\odbcconnector\x86\mysql-connector-odbc-5.1.13-win32.msi /passive /norestart",
                @"msiexec /i C:\Windows\System32\odbcconnector\x86\mysql-workbench-community-6.3.4-win32.msi /passive /norestart",
                @"msiexec /i C:\Windows\System32\odbcconnector\x64\mysqlodbc.msi /passive /norestart"
            };

            foreach (string command in commands)
            {
                ExecuteCommand(command);
            }

            Console.WriteLine("All installations completed.");
        }

        private static void ExecuteCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
            {
                Arguments = $"/c {command}",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
                Console.WriteLine($"Command executed: {command}");
                Console.WriteLine($"Exit Code: {process.ExitCode}");
            }
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void RestartAsAdministrator()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Process.GetCurrentProcess().MainModule.FileName,
                Verb = "runas"
            };

            try
            {
                Process.Start(startInfo);
            }
            catch (Exception)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("The application must be run as administrator. Please restart with appropriate permissions.");
                Console.ResetColor();
            }

            Environment.Exit(0);
        }
    }
}