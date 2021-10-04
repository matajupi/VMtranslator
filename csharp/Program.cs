using System;
using System.Collections.Generic;
using System.IO;

namespace VMtranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Dump Prologue

            // Get specified path
            if (args.Length != 1)
            {
                ErrorManager.DumpUsage("./VMtranslator VM_FILE_PATH");
                Environment.Exit(127);
            }
            var specifiedPath = args[0];

            // Get VM files
            var vmFiles = new List<string>();
            if (File.Exists(specifiedPath))
            {
                if (Path.GetExtension(specifiedPath) != ".vm")
                {
                    ErrorManager.DumpError("The file extension must be '.vm'.");
                    Environment.Exit(127);
                }
                vmFiles.Add(specifiedPath);
            }
            else if (Directory.Exists(specifiedPath))
            {
                var files = Directory.GetFiles(specifiedPath);
                foreach (var file in files)
                {
                    if (Path.GetExtension(file) == ".vm")
                        vmFiles.Add(file);
                }
            }
            else
            {
                ErrorManager.DumpError($"The file or directory named {specifiedPath} does not exists.");
                Environment.Exit(127);
            }

            var asmFile = Path.ChangeExtension(specifiedPath, ".asm");

            // Parse and generate
            var parser = new Parser();
            var generator = new HackAssemblyGenerator();
            using (var ostream = File.Open(asmFile, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(ostream))
            {
                generator.CodeWriter = sw;
                foreach (var vmFile in vmFiles)
                {
                    using (var istream = File.Open(vmFile, FileMode.Open, FileAccess.Read))
                    {
                        foreach(var instruction in parser.Parse(istream, istream.Name))
                        {
                            if (!parser.IsAllSuccessful)
                                break;
                            generator.Generate(instruction);
                        }
                    }
                }
            }

            var isAllSuccessful = parser.IsAllSuccessful && generator.IsAllSuccessful;
            if (!isAllSuccessful)
            {
                File.Delete(asmFile);
            }

            // TODO: Dump Epilogue
        }
    }
}
