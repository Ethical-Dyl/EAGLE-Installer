using System;
using System.Diagnostics;
using System.IO;

namespace EAGLE_Installer
{
    public class InstallSchema
    {
        public void Install_Schema()
        {
            string response;
            bool validInstallation = false;
            string installPath = "";

            while (!validInstallation)
            {
                bool validDrive = false;

                while (!validDrive)
                {
                    Console.WriteLine("Where is Eagle installed? C or D?");
                    response = Console.ReadLine().Trim().ToUpper();

                    switch (response)
                    {
                        case "C":
                            installPath = @"C:\MDT\Eagle\installation\Migrations\";
                            validDrive = true;
                            break;
                        case "D":
                            installPath = @"D:\MDT\Eagle\installation\Migrations\";
                            validDrive = true;
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("You did not select a valid drive, please try again.");
                            Console.ResetColor();
                            break;
                    }
                }

                Console.WriteLine($"Checking installation path: {installPath}");

                if (Directory.Exists(installPath))
                {
                    validInstallation = true;
                    Console.WriteLine("Valid installation path found.");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The directory {installPath} does not exist. Please check the installation and try again. ");
                    Console.WriteLine("");
                    Console.ResetColor();
                }
            }

            Console.WriteLine($"Proceeding with installation at: {installPath}");

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    WorkingDirectory = installPath,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,
                    UseShellExecute = false
                };

                using (Process process = Process.Start(psi))
                {
                    if (process != null)
                    {
                        process.StandardInput.WriteLine("install_scheme.bat mobile");
                        process.StandardInput.WriteLine("exit");
                        process.WaitForExit();

                        string output = process.StandardOutput.ReadToEnd();
                        Console.WriteLine("Command output:");
                        Console.WriteLine(output);
                    }
                }

                Console.WriteLine("Installation process completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}