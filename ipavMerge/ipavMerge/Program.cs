using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            StreamReader loggedPlayers1 = new StreamReader(path1 + "loggedPlayers.ipav");
            StreamReader loggedUUIDs1 = new StreamReader(path1 + "loggedUUIDs.ipav");
            StreamReader loggedAddresses2 = new StreamReader(path2 + "loggedAddresses.ipav");
            StreamReader loggedPlayers2 = new StreamReader(path2 + "loggedPlayers.ipav");
            StreamReader loggedUUIDs2 = new StreamReader(path2 + "loggedUUIDs.ipav");
            Console.WriteLine("Parsing files...");
            List<Parent> ListAddresses1 = listLoader(loggedAddresses1);
            List<Parent> ListPlayers1 = listLoader(loggedPlayers1);
            List<Parent> ListUUIDs1 = listLoader(loggedUUIDs1);
            List<Parent> ListAddresses2 = listLoader(loggedAddresses2);
            List<Parent> ListPlayers2 = listLoader(loggedPlayers2);
            List<Parent> ListUUIDs2 = listLoader(loggedUUIDs2);

            for (int j = 0; j < ListAddresses1.Count; j++)
            {
                if (!ListAddresses2.Contains(ListAddresses1[j]))
                {
                    //Console.WriteLine("yay" + j);
                }
            }

            //for (int j = 0; j < ListPlayers1.Count; j++)
            //{
            //    for (int k = 0; k < ListPlayers2.Count; k++)
            //    {
            //        if (ListPlayers2[k].ToString().Equals(ListPlayers1[j].ToString()))
            //        {
            //            Console.WriteLine(j + " yay " + k);
            //        }
            //    }
            //}
            //Console.WriteLine("loggedAddresses.ipav contains: ");
            //Action<Parent> print = link =>
            //{
            //    Console.WriteLine(link);
            //};
            //ListAddresses1.ForEach(print);

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

        static List<Parent> listLoader(StreamReader file)
        {
            int key = 0;
            List<Parent> leList = new List<Parent>();
            String line = file.ReadLine();
            while (line != null) //redundant because yolo
            {
                String parent = line;
                line = file.ReadLine();
                List<Child> child = new List<Child>();
                //Stupid code incoming
                while (line.Substring(0, 1).Equals("-"))
                {
                    String[] stupid = line.Split('\"');
                    //Check for keys split into more than one line
                    if (stupid.Length < 8)
                    {
                        String firstLine = line;
                        line = file.ReadLine();
                        String mergedLines = String.Concat(firstLine, line);
                        String[] stupider = mergedLines.Split('\"');
                        child.Add(new Child(stupider[0],
                        stupider[1],
                        stupider[2],
                        stupider[3],
                        stupider[4],
                        stupider[5],
                        stupider[6],
                        stupider[7]));
                    }   
                    else
                        child.Add(new Child(stupid[0],
                        stupid[1],
                        stupid[2],
                        stupid[3],
                        stupid[4],
                        stupid[5],
                        stupid[6],
                        stupid[7]));

                    line = file.ReadLine();
                    if (line == null)
                        break;
                }
                leList.Add(new Parent(parent, child));
            }
            return leList;
        }
    }
}
