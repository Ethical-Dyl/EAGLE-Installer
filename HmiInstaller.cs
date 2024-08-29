using System;
using System.Diagnostics;
using System.IO;

namespace EAGLE_Installer
{
    public class HmiInstaller
    {
        public void MdtInstaller()
        {
            string[] mdtAppInstallFiles = { "HMI-5.10.0-20240117.101849-4861-Installer.exe", "Eagle-2.9.4-20220921.190021-3023-Install.exe" };

            // Ask user to input the folder path
            string folderPath = SelectFolder();
            if (string.IsNullOrEmpty(folderPath))
            {
                Console.WriteLine("Folder selection cancelled. Exiting...");
                return;
            }

            bool appFilesExist = CheckFiles(folderPath, mdtAppInstallFiles);

            if (appFilesExist)
            {
                ExecuteFiles(folderPath, mdtAppInstallFiles);
            }
            else
            {
                PromptUserAndExecute(folderPath, mdtAppInstallFiles);
            }
        }

        // Method to ask user for folder path
        public string SelectFolder()
        {
            Console.WriteLine("Please enter the full path to the folder containing MDT App Install Files:");
            Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

            string folderPath = Console.ReadLine().Trim();


            while (!Directory.Exists(folderPath))
            {
                Console.WriteLine("The specified folder does not exist. Please enter a valid folder path or press Enter to exit:");
                folderPath = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(folderPath))
                {
                    return null;
                }
            }

            return folderPath;
        }

        // Check if the MDT Dependency Files are present
        public bool CheckFiles(string folderPath, string[] fileNames)
        {
            bool allExist = true;
            foreach (string fileName in fileNames)
            {
                string filePath = Path.Combine(folderPath, fileName);
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    allExist = false;
                }
            }
            return allExist;
        }

        // Executes MDT Dependencies, IF present
        public void ExecuteFiles(string folderPath, string[] fileNames)
        {
            Console.WriteLine("All required files found. Running executables in order...");
            foreach (string fileName in fileNames)
            {
                ExecuteFile(Path.Combine(folderPath, fileName));
            }
        }

        // Logic to execute MDT Dependencies
        public void ExecuteFile(string filePath)
        {
            try
            {
                using (Process process = Process.Start(filePath))
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{Path.GetFileName(filePath)} Present! Starting install...");
                    
                    Console.WriteLine($"Successfully ran: {Path.GetFileName(filePath)}");
                    Console.ResetColor();
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor= ConsoleColor.Red;
                Console.WriteLine($"Error executing {Path.GetFileName(filePath)}: {ex.Message}");
                Console.ResetColor();
            }
        }

        // Warning telling user that one or more files are missing...
        public void PromptUserAndExecute(string folderPath, string[] fileNames)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("One or more files are missing");
            Console.ResetColor();
            Console.WriteLine("Would you like to continue anyway? (Y/N)");
            string response = Console.ReadLine().Trim().ToUpper();

            if (response == "Y")
            {
                Console.WriteLine("Continuing with available files...");
                foreach (string fileName in fileNames)
                {
                    string filePath = Path.Combine(folderPath, fileName);
                    if (File.Exists(filePath))
                    {
                        ExecuteFile(filePath);
                    }
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Operation cancelled.");
                Console.ResetColor();
            }
        }
    }
}