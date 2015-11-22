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

            //Removed backup code as I decided to no longer auto-merge files :S
            Console.WriteLine("Copying files to working directory...");
                //Perform noob sanity check
            if (Directory.Exists("working1"))
                Directory.Delete("working1", true);
            if (Directory.Exists("working2"))
                Directory.Delete("working2", true);

            //Now that we're all clean, let's make 'em again! Ha... ha.......
            Directory.CreateDirectory("working1");
            Directory.CreateDirectory("working2");

            //Actually copy files to working directory
            if (!tryBackup(path1, "working1") || !tryBackup(path2, "working2"))
            {
                Console.WriteLine("Hmm something bad happened, so I'll just stop while I can (check folder permissions and whatnot)");
                return;
            }

            //Set paths to working directory.
            path1 = "working1\\";
            path2 = "working2\\";

            //Load 'em up
            Console.WriteLine("Loading files...");
            StreamReader loggedAddresses1 = new StreamReader(path1 + "loggedAddresses.ipav");
            //StreamReader loggedPlayers1 = new StreamReader(path1 + "loggedPlayers.ipav");
            StreamReader loggedUUIDs1 = new StreamReader(path1 + "loggedUUIDs.ipav");
            StreamReader loggedAddresses2 = new StreamReader(path2 + "loggedAddresses.ipav");
            //StreamReader loggedPlayers2 = new StreamReader(path2 + "loggedPlayers.ipav");
            StreamReader loggedUUIDs2 = new StreamReader(path2 + "loggedUUIDs.ipav");
            Console.WriteLine("More loading...");


            List<Parent> listAddresses1 = listLoader(loggedAddresses1);
            //List<Parent> ListPlayers1 = listLoader(loggedPlayers1);
            List<Parent> listUUIDs1 = listLoader(loggedUUIDs1);
            List<Parent> listAddresses2 = listLoader(loggedAddresses2);
            //List<Parent> ListPlayers2 = listLoader(loggedPlayers2);
            List<Parent> listUUIDs2 = listLoader(loggedUUIDs2);
            List<Parent> mergedAddresses;
            if (listAddresses1.Count > listAddresses2.Count)
                mergedAddresses = mergeParents(listAddresses1, listAddresses2);
            else
                mergedAddresses = mergeParents(listAddresses2, listAddresses1);

            List<Alias> listAliases = new List<Alias>();
            //List<List<string>> Aliaslist = new List<List<string>>();
            Console.WriteLine("Looking for aliases...");
            for (int z = 0; z < mergedAddresses.Count; z++)
            {
                List<Child> children = mergedAddresses[z].child;
                int childCount = children.Count;
                if (childCount > 1)
                {
                    for (int y = 0; y < childCount - 1; y++)
                    {
                        if (children[y].uuid != children[y + 1].uuid)
                        {
                            int index = -1;
                            String uuid = children[y].uuid;
                            String uuid2 = children[y + 1].uuid;
                            bool first = false;
                            bool alreadyExist = false;
                            for (int i = 0; i < listAliases.Count; i++)
                            {
                                if (listAliases[i].uuidExist(uuid) >= 0)
                                {
                                    index = i;
                                    first = true;
                                    if (listAliases[i].uuidExist(uuid2) >= 0)
                                        alreadyExist = true;
                                    break;
                                }
                                    
                            }
                            if (index < 0)
                            {
                                for (int i = 0; i < listAliases.Count; i++)
                                {
                                    if (listAliases[i].uuidExist(uuid2) >= 0)
                                    {
                                        index = i;
                                        first = false;
                                        break;
                                    }
                                        
                                }
                            }
                            if (index < 0)
                            {
                                List<string> temp = new List<string>();
                                temp.Add(uuid);
                                temp.Add(uuid2);
                                listAliases.Add(new Alias(temp));
                            }
                            else
                            {
                                if (!alreadyExist)
                                {
                                    if (first)
                                        listAliases[index].playerUuids.Add(uuid2);
                                    else
                                        listAliases[index].playerUuids.Add(uuid);
                                }
                            }


                            //int x = y;
                            //int index = listAliases.FindIndex(delegate (Alias alias)
                            //{ return alias.playerUuids.Equals(children[y].uuid); });
                            //if (!(index >= 0))
                            //{
                            //    index = listAliases.FindIndex(delegate (Alias alias)
                            //    { return alias.player.uuid.Equals(children[y + 1].uuid); });
                            //}
                            //if (!(index >= 0))
                            //{
                            //    List<Child> temp = new List<Child>();
                            //    temp.Add(children[y + 1]);
                            //    listAliases.Add(new Alias(children[y], temp));
                            //    Console.WriteLine("else " + children[y].name);
                            //    continue;
                            //}
                            //if (!listAliases[index].child.Exists(delegate (Child child)
                            //{ return child.Equals(children[x + 1]); }))
                            //{
                            //    listAliases[index].child.Add(children[x + 1]);
                            //    Console.WriteLine("first " + children[y].name);
                            //}
                            //else if (!listAliases[index].child.Exists(delegate (Child child)
                            //        { return child.Equals(children[y]); }))
                            //{
                            //    listAliases[index].child.Add(children[y]);
                            //}           
                        }
                    }
                            
                            //Console.WriteLine("Player " + children[y].name +
                            //    " is an alias of " + children[y + 1].name);
                }
            }

            List<Parent> mergedUUIDs;
            if (listUUIDs1.Count > listUUIDs2.Count)
                mergedUUIDs = mergeParents(listUUIDs1, listUUIDs2);
            else
                mergedUUIDs = mergeParents(listUUIDs2, listUUIDs1);

            for (int i = 0; i < listAliases.Count; i++)
            {
                Console.WriteLine("Alias " + (i + 1) + ":");
                for (int j = 0; j < listAliases[i].playerUuids.Count; j++)
                {
                    String uuid = listAliases[i].playerUuids[j];
                    Console.WriteLine(uuid + " (" + uuidToName(uuid, mergedUUIDs) + ")");
                }
            }
            while (true)
            {
                Console.WriteLine("Find an alias: ");
                String wut = Console.ReadLine();
                int index = -1;
                for (int i = 0; i < listAliases.Count; i++)
                {
                    if (listAliases[i].uuidExist(wut) >= 0)
                    {
                        index = i;
                        break;
                    }
                }
                Console.WriteLine(index);
                if (index >= 0)
                {
                    Console.WriteLine(wut + " has the following aliases: ");
                    for (int j = 0; j < listAliases[index].playerUuids.Count; j++)
                    {
                        String uuid = listAliases[index].playerUuids[j];
                        Console.WriteLine(uuid + " (" + uuidToName(uuid, mergedUUIDs) + ")");
                    }
                }
            }

            

            //for (int z = 0; z < mergedUUIDs.Count; z++)
            //{
            //    for (int y = 0; y < mergedUUIDs[z].child.Count; y++)
            //    {
            //        if (mergedUUIDs[z].child[y].ip.Equals())
            //    }
            //}
            //for (int j = 0; j < ListAddresses1.Count; j++)
            //{
            //    if (!ListAddresses2.Contains(ListAddresses1[j]))
            //    {
            //        //Console.WriteLine("yay" + j);
            //    }
            //}


            //Console.WriteLine("loggedAddresses.ipav contains: ");
            //Action<Parent> print = link =>
            //{
            //    Console.WriteLine(link);
            //};
            //ListAddresses1.ForEach(print);

            //Parent result = listAddresses1.Find(delegate (Parent par)
            //{ return par.parent == searchFor; });




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
            List<Parent> leList = new List<Parent>();
            String line = file.ReadLine();
            while (line != null)
            {
                String parent = "";
                if (line.Substring(0, 1).Equals("'"))
                {
                    int secondQuotationMark = line.IndexOf("'", 2);
                    parent = line.Substring(1, secondQuotationMark - 1);
                }
                else
                {
                    int colon = line.IndexOf(":", 1);
                    parent = line.Substring(0, colon);
                }
                line = file.ReadLine();
                List<Child> child = new List<Child>();
                //Stupid code incoming
                while (line.Substring(0, 1).Equals("-"))
                {
                    String[] stupid = line.Split('\"');
                    child.Add(new Child(stupid[0].Substring(2, stupid[0].Length - 2),
                    stupid[1],
                    stupid[2]));
                    //Check for keys split into more than one line
                    if (stupid.Length < 8)
                        line = file.ReadLine();

                    line = file.ReadLine();
                    if (line == null)
                        break;
                }
                leList.Add(new Parent(parent, child));
            }
            return leList;
        }

        //Pretty sure there's something in the List API for this but I'm too stupid to figure it out :(
        static List<Parent> mergeParents(List<Parent> par1, List<Parent> par2)
        {        
            for (int j = 0; j < par1.Count; j++)
            {
                bool match = false;
                for (int k = 0; k < par2.Count; k++)
                {
                    if (par2[k].parent.Equals(par1[j].parent))
                    {
                        match = true;
                        if (par1[j].child.Count > par2[k].child.Count)
                            par2 = mergeChildren(par1, j, par2, k);
                        else
                            par2 = mergeChildren(par2, k, par1, j);
                        break;
                    }
                }

                if (!match)
                {
                    par2.Add(par1[j]);
                }
            }
            return par2;
        }

        static List<Parent> mergeChildren(List<Parent> par1, int a, List<Parent> par2, int b)
        {  
            List<Child> child1 = par1[a].child;
            List<Child> child2 = par2[b].child;
            for (int j = 0; j < child1.Count; j++)
            {
                bool match = false;
                for (int k = 0; k < child2.Count; k++)
                {
                    if (child2[k].Equals(child1[j]))
                    {
                        match = true;
                        break;
                    }
                }

                if (!match)
                {
                    child2.Add(child1[j]);
                }
            }
            par2[b].child = child2;
            return par2;
        }

        static string uuidToName(String uuid, List<Parent> mergedUUIDs)
        {
            //for (int i = 0; i < mergedUUIDs.Count; i++)
            //{
            //    if (mergedUUIDs[i].parent.Equals(uuid))
            //    index = i;
            //    break;
            //}

            int index = mergedUUIDs.FindIndex(delegate(Parent par)
            { return par.parent.Equals(uuid); });
            if (index < 0)
                return "not found";
            String oi = mergedUUIDs[index].uuidFind(uuid);
            return oi;
        }
    }
}
