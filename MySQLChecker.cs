using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAGLE_Installer
{
    public class MySQLChecker
    {
        public void MySQLCheck()
        {
            Console.WriteLine("Checking for mysql-5.6.25-winx64");

            string folderPath = @"C:\Program Files\MySQL\mysql-5.6.25-winx64";
            
            EnsureMySQLFolderExists(folderPath);

            
        }

        // Checks if the Directory mysql-5.6.25-winx64 exists!
        static bool EnsureMySQLFolderExists(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Directory mysql-5.6.25-winx64 exists!");
                    Console.ResetColor();
                    return true;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Directory mysql-5.6.25-winx64 does not exists!");
                    Console.BackgroundColor = ConsoleColor.Yellow; Console.WriteLine("Attmept installing MDT Dependencies manually");
                    return false;
                }

            }

            catch (UnauthorizedAccessException ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: No permission to access or create the folder. {ex.Message}");
                Console.ResetColor();
            }
            catch (PathTooLongException ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: The specified path is too long. {ex.Message}");
                Console.ResetColor();
            }
            catch (IOException ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: An I/O error occurred while creating the directory. {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.ResetColor();
            }
            return false;
        }

    }
}
