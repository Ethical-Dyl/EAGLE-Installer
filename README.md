# EAGLE_Installer


## This is a writeup of how to install Eagle on a laptop.


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
