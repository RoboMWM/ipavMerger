using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipavMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            //Terminate program if proper arguments are not supplied
            if (args.Length < 2)
            {
                Console.WriteLine("ayy lemayo do u even reed br0");
                return;
            }
            String path1 = args[0];
            String path2 = args[1];

            //First backup files so if dis dum progrum dun' goofs...
            int i = 0;
            for (i = 0; i < 100; i++)
            {
                if (!Directory.Exists("backup" + i))
                {
                    Directory.CreateDirectory("backup" + i);
                    Directory.CreateDirectory("backup_" + i);
                    Console.WriteLine("Attempting to backup files first...");

                    //If any of this fails, just stop
                    if (!tryBackup(path1, "backup" + i) || !tryBackup(path2, "backup_" + i))
                    {
                        Console.WriteLine("Backup failed, so you probably have some incorrect paths up there buddy :/");
                        return;
                    }

                    //set path1 and path2 to backup directories so we can copy these files to a working directory
                    //*genius*
                    path1 = "backup" + i;
                    path2 = "backup_" + i;
                    break;
                }
            }

            //Load 'em up
            //StreamReader loggedAddresses1 = new StreamReader(path1 + "loggedAddresses.ipav");

            //String line = "";
            //while (line != null)
            //{
            //    line = loggedAddresses1.ReadLine();
            //    if (!line.Substring(0, 4).Equals("    "))
            //}
        }
        static bool copyFiles(String sourcePath, String sourceFile, String destinationPath)
        {
            try
            {
                String sauceFile = Path.Combine(sourcePath, sourceFile);
                String destFile = Path.Combine(destinationPath, sourceFile);
                File.Copy(sauceFile, destFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
        static bool tryBackup(String sourcePath, String destinationPath)
        {
            //Prepare for dum-dum
            if (copyFiles(sourcePath, "loggedAddresses.ipav", destinationPath))
                if (copyFiles(sourcePath, "loggedPlayers.ipav", destinationPath))
                    if (copyFiles(sourcePath, "loggedUUIDs.ipav", destinationPath))
                        return true;
                    else
                        return false;
                else
                    return false;
            else
                return false;
        }
    }
}
