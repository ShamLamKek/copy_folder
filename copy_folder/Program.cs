using System;
using System.IO;


namespace copy_folder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь исходной папки:");
            string initDir = Console.ReadLine(); //Path to initial directory
            DirectoryInfo source = new DirectoryInfo(initDir);

            Console.WriteLine("Введите путь к конечной папке:");
            string endDir = Console.ReadLine(); //Path to end directory
            DirectoryInfo destination = new DirectoryInfo(endDir);

            CopyDirectory(source, destination);
            Console.ReadKey();

        }

        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            //Create subdirectory with creation time
            string timestamp = DateTime.Now.ToString("dddd, dd MMMM yyyy HH.mm.ss");
            DirectoryInfo newdist = destination.CreateSubdirectory(timestamp);

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(newdist.FullName,
                    file.Name));
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = Path.Combine(newdist.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }



        }
    }

}
