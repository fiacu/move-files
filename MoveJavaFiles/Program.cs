using System;
using System.Collections.Generic;
using System.IO;

namespace MoveJavaFiles
{
    class Program
    {
        /// <summary>
        /// Unifica los archivos de un multi proyecto Swarm a un proyecto unico. Para Java.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                Console.WriteLine("Use: MoveJavafiles.exe $sourceFolder $destinationFolder");
            }
            else
            {
                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(args[0]);
                    foreach (string dir in subDirs)
                    {
                        if (dir.StartsWith(args[0] + "\\Beesion")) {
                            Console.WriteLine("Copying directory: {0}", dir);
                            CopyDir(dir, args[1]);
                        }
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        public static void CopyDir(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            // Get Files & Copy
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                if (name.Contains("pom.xml"))
                    continue;

                // ADD Unique File Name Check to Below!!!!
                string dest = Path.Combine(destFolder, name);
                try
                {
                    File.Copy(file, dest);
                }
                catch (IOException)
                {
                    Console.WriteLine("Can't copy file:" + file);
                }
            }

            // Get dirs recursively and copy files
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyDir(folder, dest);
            }
        }
    }
}
