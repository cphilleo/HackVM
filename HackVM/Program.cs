using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HackVM.Commands;

namespace HackVM
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: HackVM {file}|{folder}");
                return;
            }

            var path = args[0];
            var outputPath = string.Empty;
            var files = new List<string>();

            if (!path.EndsWith(".vm"))
            {
                var dirName = new DirectoryInfo(path).Name;
                files.AddRange(Directory.GetFiles(path, "*.vm"));

                outputPath = path + @"\" + dirName + ".asm";
            }

            else
            {
                files.Add(path);
                outputPath = path.Replace(".vm", ".asm");
            }

            Console.WriteLine("path: " + path);
            Console.WriteLine("outputPath: " + outputPath);

            foreach (var file in files)
            {
                Console.WriteLine("file: " + file);
            }

            var output = "";

            //write bootstrap code
            foreach (var asm in new BootstrapCommand().GetAssembly())
            {
                output += asm + "\n";
            }

            foreach (var file in files)
            {
                //Set context
                Console.WriteLine("parsing file: " + file);
                ParserContext.SetFile(Path.GetFileNameWithoutExtension(file));

                var parser = new Parser(file);

                foreach (var command in parser.GetCommands())
                {
                    foreach (var asm in command.GetAssembly())
                    {
                        Console.WriteLine(asm);
                        output += asm + "\n";
                    }
                }
            }

            File.WriteAllText(outputPath, output);
        }
    }
}
