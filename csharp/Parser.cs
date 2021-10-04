using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace VMtranslator
{
    class Parser
    {
        public bool IsAllSuccessful { get; private set; } = true;
        private string fileName;
        private int rowNumber = 1;
        
        public IEnumerable<Instruction> Parse(Stream vmCode, string fileName)
        {
            this.fileName = fileName;
            using (var sr = new StreamReader(vmCode))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var instruction = this.ParseOneLine(line);
                    this.rowNumber++;
                    if (instruction == default)
                        continue;
                    yield return instruction;
                }
            }
        }

        public Instruction ParseOneLine(string line)
        {
            // Ignore comment and trim
            line = line.Split("//").First().Trim();
            if (line == string.Empty)
                return default;

            var chunks = line.Split(' ');
            var kind = chunks[0];

            Instruction instruction;
            if (Instruction.ArithmeticInstructionTable.ContainsKey(kind))
            {
                instruction = this.ParseArithmeticInstruction(chunks);
            }
            else if (Instruction.MemoryAccessInstructionTable.ContainsKey(kind))
            {
                instruction = this.ParseMemoryAccessInstruction(chunks);
            }
            else if (Instruction.ProgramFlowInstructionTable.ContainsKey(kind))
            {
                instruction = this.ParseProgramFlowInstruction(chunks);
            }
            else if (Instruction.FunctionCallInstructionTable.ContainsKey(kind))
            {
                instruction = this.ParseFunctionCallInstruction(chunks);
            }
            else
            {
                ErrorManager.DumpError($"The instruction '{kind}' does not exist.", 
                    this.fileName, this.rowNumber);
                this.IsAllSuccessful = false;
                instruction = default;
            }
            return instruction;
        }

        private Instruction ParseArithmeticInstruction(string[] chunks)
        {
            if (chunks.Length != 1)
            {
                ErrorManager.DumpError($"The instruction '{chunks[0]}' has no arguments. But {chunks.Length - 1} were given.",
                    this.fileName, this.rowNumber);
                this.IsAllSuccessful = false;
                return default;
            }
            var instruction = new Instruction();
            instruction.FileName = this.fileName;
            instruction.RowNumber = this.rowNumber;
            instruction.Kind = Instruction.ArithmeticInstructionTable[chunks[0]];
            return instruction;
        }

        private Instruction ParseMemoryAccessInstruction(string[] chunks)
        {
            if (chunks.Length != 3)
            {
                ErrorManager.DumpError($"The instruction '{chunks[0]}' has two arguments. But {chunks.Length - 1} were given.",
                    this.fileName, this.rowNumber);
                this.IsAllSuccessful = false;
                return default;
            }
            var instruction = new Instruction();
            instruction.FileName = this.fileName;
            instruction.RowNumber = this.rowNumber;
            instruction.Kind = Instruction.MemoryAccessInstructionTable[chunks[0]];
            if (!Instruction.SegmentTable.ContainsKey(chunks[1]))
            {
                ErrorManager.DumpError($"The segment '{chunks[1]}' does not exist.",
                    this.fileName, this.rowNumber);
                this.IsAllSuccessful = false;
                return default;
            }
            instruction.Segment = Instruction.SegmentTable[chunks[1]];
            if (!int.TryParse(chunks[2], out int offset))
            {
                ErrorManager.DumpError($"'{chunks[2]}' is not a decimal number.",
                    this.fileName, this.rowNumber);
                this.IsAllSuccessful = false;
                return default;
            }
            instruction.Offset = offset;
            return instruction;
        }

        private Instruction ParseProgramFlowInstruction(string[] chunks)
        {
            return default;
        }

        private Instruction ParseFunctionCallInstruction(string[] chunks)
        {
            return default;
        }
    }
}