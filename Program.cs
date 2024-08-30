using System;
using System.IO;
using System.Diagnostics;
using EAGLE_Installer;

namespace EagleInstallProcess
{
    class Program
    {
        static void DisplayTitle()
        {
            Console.Clear();
            Console.WriteLine("Dylan Paynter, SPE...");
            Console.WriteLine("Eagle Install Process");
            Console.WriteLine("If you need assistance or run into an error please contact me, dylan.paynter@nextierofs.com");
            Console.WriteLine("------------------------");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.Title = "Eagle Install Process";

            // Local instances
            TempAndDependencyCheck tempAndDependencyCheck = new TempAndDependencyCheck();
            DotNetInstaller dotNetInstaller = new DotNetInstaller();
            HmiInstaller hmiInstaller = new HmiInstaller();
            MySQLChecker mySQLChecker = new MySQLChecker();
            InstallSchema installSchema = new InstallSchema();

            DisplayTitle();
            Console.WriteLine("Beginning the Eagle Install Process");
            Console.WriteLine();

            // Run the Temp folder check and Dependency checks
            tempAndDependencyCheck.TempDepCheck(new string[] { });
            DisplayTitle();
            Console.WriteLine("Temp folder and dependency checks completed.");
            Console.WriteLine();

            // Run the .Net 3.5 checker and installation
            dotNetInstaller.dotNetInstall();
            DisplayTitle();
            Console.WriteLine(".NET 3.5 installation check completed.");
            Console.WriteLine();

            // Run the HMI and Eagle Installers
            hmiInstaller.MdtInstaller();
            DisplayTitle();
            Console.WriteLine("HMI and Eagle installation completed.");
            Console.WriteLine();

            // Run the mysql-5.6.25-winx64 checker
            mySQLChecker.MySQLCheck();
            DisplayTitle();
            Console.WriteLine("MySQL check completed.");
            Console.WriteLine();

            // Run the cmd prompts, this is being called directly.
            MsiExecs.ExecuteMSIs();
            DisplayTitle();
            Console.WriteLine("MSI executions completed.");
            Console.WriteLine();

            // Run the install_schema.bat functions
            installSchema.Install_Schema();
            DisplayTitle();
            Console.WriteLine("Schema installation completed.");
            Console.WriteLine();


            DisplayTitle();
            Console.WriteLine("Installation process completed.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}