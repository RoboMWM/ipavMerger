﻿using System;
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
                    Console.WriteLine("Attempting to backup files to backup" + i + "...");

                    //If any of this fails, just stop
                    if (!tryBackup(path1, "backup" + i) || !tryBackup(path2, "backup_" + i))
                    {
                        Console.WriteLine("Backup failed, so you probably have some incorrect paths up there buddy :/");
                        return;
                    }

                    //copy these files to a working directory *genius.jpg*
                    Console.WriteLine("Copying files from backup to working directory...");

                    //Perform noob sanity check
                    if (Directory.Exists("working1"))
                        Directory.Delete("working1", true);
                    if (Directory.Exists("working2"))
                        Directory.Delete("working2", true);

                    //Now that we're all clean, let's make 'em again! Ha... ha.......
                    Directory.CreateDirectory("working1");
                    Directory.CreateDirectory("working2");
                    path1 = "backup" + i;
                    path2 = "backup_" + i;

                    //copy pasta
                    if (!tryBackup(path1, "working1") || !tryBackup(path2, "working2"))
                    {
                        Console.WriteLine("err, how did this happen :?");
                        return;
                    }

                    //Set paths to working directory, then break loop.
                    path1 = "working1\\";
                    path2 = "working2\\";
                    break;
                }
            }

            //Load 'em up
            Console.WriteLine("Loading files...");
            StreamReader loggedAddresses1 = new StreamReader(path1 + "loggedAddresses.ipav");
            List<Parent> LoggedAddresses1 = listLoader(loggedAddresses1, 1);
            Console.WriteLine("loggedAddresses.ipav has " + la1 + " lines.");

            
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
        static List<Parent> listLoader(StreamReader file, int which)
        {
            List<Parent> leList = new List<Parent>();
            String line = file.ReadLine();
            int key = 0;
            while (line != null) //redundant because yolo
            {
                if (!line.Substring(0,1).Equals("-"))
                {
                    String parent = line;
                    List<Child> child = new List<Child>();
                    //Stupid code incoming
                    while (line.Substring(0,1).Equals("-"))
                    {
                        String[] stupid = line.Split('\"');
                        List<string> moreStupid = new List<string>();
                        for (int i = 0; i <= 8; i++)
                        {
                            moreStupid.Add(stupid[i]);
                        }
                        child.Add((Child)moreStupid);
                    }
                }
                if (line == null)
                    break;
            }
            //return
        }
    }
}
