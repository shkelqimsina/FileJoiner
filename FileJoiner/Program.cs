using System;
using System.Collections.Generic;
using System.IO;

namespace FileJoiner
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Files = ListFiles(args[0]);

            for (int i = 0; i < args.Length; i++)
            {
                // First argument is the directory location
                if (i == 0)
                    continue;
                File.WriteAllText(
                    args[0] + "\\" + args[i],
                    JoinFiles(Files, Path.GetExtension(args[i]))
                    );
            }
        }

        public static string JoinFiles(IList<string> Files, string Extension)
        {
            string all = "";
            foreach (string file in Files)
            {
                if (Path.GetExtension(file) == Extension)
                {
                    TextReader tr = new StreamReader(file);
                    string content = "";
                    while (true)
                    {
                        string tmp = tr.ReadLine();
                        if (tmp == null)
                            break;
                        else
                            content += tmp + "\n";
                    }
                    tr.Close();
                    tr = null;
                    all += content;
                    if (Extension == ".css" && content.Contains("@import"))
                        Console.WriteLine("Import links are not considered!");
                }
            }
            return all;
        }

        static string[] ListFiles(string directory)
        {
            string[] f = Directory.GetFiles(directory);
            return f;
        }
    }
}
