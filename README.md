# EAGLE Installer

![eagle_flag](https://github.com/user-attachments/assets/54769c73-b6d7-4668-8c83-c75ca46adf4a)


# How it works
Step 1. The installer will check if the C:\temp directory has been created, if not it will create it for you.

Step 2. Execution the following dependency installers, "MDTDependencies_V4.61NT.exe", "MDTDependencies_V4.62BIRDS.exe" in that order as well.

Step 3. Check if the .NET framework 3.5 HTTP and Non-HTTP packages are also installed, if not it will be automatically installed for you.

Step 4. Run the neccessary MSIs.

Step 5. Execute the HMI 5.10 Installer, and install Spyglass

Step 6. Execute the Eagle 2.9.4 Installer

Step 7. Installer will check to ensure that the mysql-5.6.25-winx64 file is installed in C:\Program Files

Step 8. Execute the install_shema.bat program along with the neccessary arguments

## Note You will need to do the following after the process is complete.

1. Change the Eagle application to run as administrator.

2.  In the Programsettings.ini file add the following parameters, replace Executable with the location of your Spyglass.exe if not in C drive :
    "
        [ExternalHMI]
        ProcessName=MDT.Gryphon.Spyglass
        Executable=C:\MDT\Spyglass\bin\MDT.Gryphon.Spyglass.exe
        Caption=MPC

    "
    






## This is a writeup of how to install Eagle on a laptop manually.


1. Install MDT Dependencies V4.61NT.
    - Install in respective drive i.e.. C:\

2. Install MDT Dependencies_Birds

3. Ensure HTTP and Non-HTTP is enabled under .NET Framework 3.5

4. Run the following commands in an elevated command prompt in the following directory: "C:\Windows\System32" : 
    - start/w "" msiexec /i odbcconnector\x86\mysql-connector-odbc-5.1.13-win32.msi /passive /norestart
    - start/w "" msiexec /i odbcconnector\x86\mysql-workbench-community-6.3.4-win32.msi /passive /norestart
    - start/w "" msiexec /i odbcconnector\x64\mysqlodbc.msi /passive /norestart

5. Install HMI 5.10

6. Install Spyglass

7. Install Eagle-2.9.4 

8. Ensure mysql-5.6.25-winx64 is installed in "C:\Program Files"

9. In command prompt navigate to "C:\MDT\Eagle\installation\Migrations\"

10. Run the following command: "install_schema.bat mobile"
- When asked to continue enter Y

11. Change both the app and shortcut to be ran as administrator

12. In the Programsettings.ini file add the following parameters:
    "
        [ExternalHMI]
        ProcessName=MDT.Gryphon.Spyglass
        Executable=C:\MDT\Spyglass\bin\MDT.Gryphon.Spyglass.exe
        Caption=MPC

    "
