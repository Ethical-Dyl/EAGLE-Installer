using System;
using System.IO;
using System.Diagnostics;
using EAGLE_Installer;

namespace EagleInstallProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            TempAndDependencyCheck tempAndDependencyCheck = new TempAndDependencyCheck();
            DotNetInstaller dotNetInstaller = new DotNetInstaller();
            HmiInstaller hmiInstaller = new HmiInstaller();
            MySQLChecker mySQLChecker = new MySQLChecker();
            InstallSchema installSchema = new InstallSchema();
            

            Console.WriteLine("Dylan Paynter, SPE.......");
            Console.WriteLine("Beginning the Eagle Install Process");
          

            // Run the Temp folder check and Dependency checks
            tempAndDependencyCheck.TempDepCheck(new string[] { });

            // Run the .Net 3.5 checker and installation
            dotNetInstaller.dotNetInstall();

            // Run the HMI and Eagle Installers
            hmiInstaller.MdtInstaller();

            // Run the mysql-5.6.25-winx64 checker
            mySQLChecker.MySQLCheck();

            // Run the cmd prompts, this is being called directly.
            MsiExecs.ExecuteMSIs();

            // Run the install_schema.bat funcitons
            installSchema.Install_Schema();

            // Change Eagle & MPC to run as administrator


            //Programsettings.ini checker



        }
    }
}
