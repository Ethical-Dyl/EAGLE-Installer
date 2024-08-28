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

            Console.WriteLine("Dylan Paynter, SPE.......");
            Console.WriteLine("Beginning the Eagle Install Process");
            Thread.Sleep(1200);
            Console.WriteLine("Checking for Install MDT Dependencies V4.61NT and MDT Dependencies_Birds and C:\\temp");
            Thread.Sleep(1200);

            // Run the Temp folder check and Dependency checks
            tempAndDependencyCheck.TempDepCheck(new string[] { });

            // Run the .Net 3.5 checker and installation
            dotNetInstaller.dotNetInstall();

            // Run the HMI and Eagle Installers
            hmiInstaller.MdtInstaller();

            // Run the mysql-5.6.25-winx64 checker


            // Run the install_schema.bat funcitons


            // Change Eagle & MPC to run as administrator

            
            //Programsettings.ini checker



        }
    }
}
