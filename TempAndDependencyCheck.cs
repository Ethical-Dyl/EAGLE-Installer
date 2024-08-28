using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAGLE_Installer
{
    public  class TempAndDependencyCheck
    {
        public void TempDepCheck(string[] args)
        {
            Console.WriteLine("Dylan Paynter, SPE.......");
            Console.WriteLine("Beginning the Eagle Install Process");
            Thread.Sleep(1200);
            Console.WriteLine("Checking for Install MDT Dependencies V4.61NT and MDT Dependencies_Birds and C:\\temp");
            Thread.Sleep(1200);

            string folderPath = @"C:\temp";
            string[] mdtDependencyFiles = { "MDTDependencies_V4.61NT.exe", "MDTDependencies_V4.62BIRDS.exe" };

            if (EnsureTempFolderExists(folderPath))
            {
                bool allFilesExist = CheckFiles(folderPath, mdtDependencyFiles);

                if (allFilesExist)
                {
                    DotNetInstaller dotNetInstaller = new DotNetInstaller();
                    HmiInstaller hmiInstaller = new HmiInstaller();
                    TempAndDependencyCheck tempAndDependencyCheck = new TempAndDependencyCheck();


                    ExecuteFiles(folderPath, mdtDependencyFiles);
                    //tempAndDependencyCheck.;
                    dotNetInstaller.dotNetInstall();
                    hmiInstaller.MdtInstaller();
                }
                else
                {
                    PromptUserAndExecute(folderPath, mdtDependencyFiles);
                }
            }
            else
            {
                Console.WriteLine("Cannot proceed without the C:\\temp folder.");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }




        // Ensure the C:\temp folder exists
        static bool EnsureTempFolderExists(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"The folder {folderPath} already exists.");
                    Console.ResetColor();
                    return true;
                }
                else
                {
                    Console.WriteLine($"The folder {folderPath} does not exist. Creating now...");
                    Directory.CreateDirectory(folderPath);

                    if (Directory.Exists(folderPath))
                    {
                        Console.WriteLine($"The folder {folderPath} has been successfully created.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to create the folder {folderPath}.");
                        return false;
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Error: No permission to access or create the folder. {ex.Message}");
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine($"Error: The specified path is too long. {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: An I/O error occurred while creating the directory. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return false;
        }





        // Check if the MDT Dependency Files are present
        static bool CheckFiles(string folderPath, string[] fileNames)
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
        static void ExecuteFiles(string folderPath, string[] fileNames)
        {
            Console.WriteLine("All required files found. Running executables in order...");
            foreach (string fileName in fileNames)
            {
                ExecuteFile(Path.Combine(folderPath, fileName));
            }
        }

        // Logic to execute MDT Dependencies
        static void ExecuteFile(string filePath)
        {
            try
            {
                using (Process process = Process.Start(filePath))
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Dependency File Present! Starting install...");
                    Console.ResetColor();
                    process.WaitForExit();
                    Console.WriteLine($"Successfully ran: {Path.GetFileName(filePath)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }


        // Warning telling user that one or more files are missing...
        static void PromptUserAndExecute(string folderPath, string[] fileNames)
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
                Console.WriteLine("Operation cancelled.");
            }
        }
    }
}