using System;
using System.IO;

namespace VMtranslator
{
    static class ErrorManager
    {
        public static TextWriter ErrorWriter = Console.Error;
        public static void DumpError(string message)
        {
            ErrorWriter.WriteLine($"Error: {message}");
        }

        public static void DumpError(string message, Instruction instruction)
        {
            DumpError(message, instruction.FileName, instruction.RowNumber);
        }

        public static void DumpError(string message, string fileName, int rowNumber)
        {
            ErrorWriter.WriteLine($"Error: {fileName}({rowNumber}): {message}");
        }

        public static void DumpUsage(string message)
        {
            ErrorWriter.WriteLine($"Usage: {message}");
        }
    }
}