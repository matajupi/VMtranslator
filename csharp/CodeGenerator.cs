using System;
using System.Collections.Generic;
using System.IO;

namespace VMtranslator
{
    abstract class CodeGenerator
    {
        public StreamWriter CodeWriter { get; set; }
        public bool IsAllSuccessful { get; protected set; } = true;

        public void Generate(Instruction instruction)
        {
            switch (instruction.Kind)
            {
                case InstructionKind.Add:
                    this.Add(instruction);
                    break;
                case InstructionKind.Sub:
                    this.Sub(instruction);
                    break;
                case InstructionKind.Neg:
                    this.Neg(instruction);
                    break;
                case InstructionKind.Eq:
                    this.Eq(instruction);
                    break;
                case InstructionKind.Gt:
                    this.Gt(instruction);
                    break;
                case InstructionKind.Lt:
                    this.Lt(instruction);
                    break;
                case InstructionKind.And:
                    this.And(instruction);
                    break;
                case InstructionKind.Or:
                    this.Or(instruction);
                    break;
                case InstructionKind.Not:
                    this.Not(instruction);
                    break;
                case InstructionKind.Push:
                    this.Push(instruction);
                    break;
                case InstructionKind.Pop:
                    this.Pop(instruction);
                    break;
            }
        }

        protected abstract void Add(Instruction instruction);
        protected abstract void Sub(Instruction instruction);
        protected abstract void Neg(Instruction instruction);
        protected abstract void Eq(Instruction instruction);
        protected abstract void Gt(Instruction instruction);
        protected abstract void Lt(Instruction instruction);
        protected abstract void And(Instruction instruction);
        protected abstract void Or(Instruction instruction);
        protected abstract void Not(Instruction instruction);
        protected abstract void Push(Instruction instruction);
        protected abstract void Pop(Instruction instruction);
    }

    class HackAssemblyGenerator : CodeGenerator
    {
        private static readonly int PointerBaseAddress = 3;
        private static readonly int TempBaseAddress = 5;
        private int labelNumber = 1;

        private void GetOneOperand()
        {
            this.CodeWriter.WriteLine("@SP");
            this.CodeWriter.WriteLine("A=M-1");
        }
        private void GetTwoOperand()
        {
            this.CodeWriter.WriteLine("@SP");
            this.CodeWriter.WriteLine("AM=M-1");
            this.CodeWriter.WriteLine("D=M");
            this.CodeWriter.WriteLine("A=A-1");
        }
        private void Compare(string compareCommand)
        {
            this.GetTwoOperand();
            this.CodeWriter.WriteLine("D=M-D");
            this.CodeWriter.WriteLine($"@.Ltrue{this.labelNumber}");
            this.CodeWriter.WriteLine($"D;{compareCommand}");
            this.CodeWriter.WriteLine("D=0");
            this.CodeWriter.WriteLine($"@.Lend{this.labelNumber}");
            this.CodeWriter.WriteLine("0;JMP");
            this.CodeWriter.WriteLine($"(.Ltrue{this.labelNumber})");
            this.CodeWriter.WriteLine("D=-1");
            this.CodeWriter.WriteLine($"(.Lend{this.labelNumber})");
            this.CodeWriter.WriteLine("@SP");
            this.CodeWriter.WriteLine("A=M-1");
            this.CodeWriter.WriteLine("M=D");
            this.labelNumber++;
        }
        protected override void Add(Instruction instruction)
        {
            this.GetTwoOperand();
            this.CodeWriter.WriteLine("M=D+M");
        }
        protected override void Sub(Instruction instruction)
        {
            this.GetTwoOperand();
            this.CodeWriter.WriteLine("M=M-D");
        }
        protected override void Neg(Instruction instruction)
        {
            this.GetOneOperand();
            this.CodeWriter.WriteLine("M=-M");
        }
        protected override void Eq(Instruction instruction)
        {
            this.Compare("JEQ");
        }
        protected override void Gt(Instruction instruction)
        {
            this.Compare("JGT");
        }
        protected override void Lt(Instruction instruction)
        {
            this.Compare("JLT");
        }
        protected override void And(Instruction instruction)
        {
            this.GetTwoOperand();
            this.CodeWriter.WriteLine("M=D&M");
        }
        protected override void Or(Instruction instruction)
        {
            this.GetTwoOperand();
            this.CodeWriter.WriteLine("M=D|M");
        }
        protected override void Not(Instruction instruction)
        {
            this.GetOneOperand();
            this.CodeWriter.WriteLine("M=!M");
        }
        protected override void Push(Instruction instruction)
        {
            this.ReferData(instruction);
            this.CodeWriter.WriteLine("@SP");
            this.CodeWriter.WriteLine("A=M");
            this.CodeWriter.WriteLine("M=D");
            this.CodeWriter.WriteLine("@SP");
            this.CodeWriter.WriteLine("M=M+1");
        }
        protected override void Pop(Instruction instruction)
        {
            this.CodeWriter.WriteLine("@SP");
            this.CodeWriter.WriteLine("AM=M-1");
            this.CodeWriter.WriteLine("D=M");
            this.CodeWriter.WriteLine("@R13");
            this.CodeWriter.WriteLine("M=D");
            this.ReferAddress(instruction);
            this.CodeWriter.WriteLine("D=A");
            this.CodeWriter.WriteLine("@R14");
            this.CodeWriter.WriteLine("M=D");
            this.CodeWriter.WriteLine("@R13");
            this.CodeWriter.WriteLine("D=M");
            this.CodeWriter.WriteLine("@R14");
            this.CodeWriter.WriteLine("A=M");
            this.CodeWriter.WriteLine("M=D");
        }

        private void ReferData(Instruction instruction)
        {
            var segment = instruction.Segment;
            if (segment == Segment.Constant)
            {
                this.CodeWriter.WriteLine($"@{instruction.Offset}");
                this.CodeWriter.WriteLine($"D=A");
            }
            else
            {
                this.ReferAddress(instruction);
                this.CodeWriter.WriteLine("D=M");
            }
        }

        private void ReferAddress(Instruction instruction)
        {
            var segment = instruction.Segment;
            switch (segment)
            {
                case Segment.Argument:
                    this.CodeWriter.WriteLine("@ARG");
                    this.CodeWriter.WriteLine("A=M");
                    break;
                case Segment.Local:
                    this.CodeWriter.WriteLine("@LCL");
                    this.CodeWriter.WriteLine("A=M");
                    break;
                case Segment.This:
                    this.CodeWriter.WriteLine("@THIS");
                    this.CodeWriter.WriteLine("A=M");
                    break;
                case Segment.That:
                    this.CodeWriter.WriteLine("@THAT");
                    this.CodeWriter.WriteLine("A=M");
                    break;
                case Segment.Pointer:
                    this.CodeWriter.WriteLine($"@{PointerBaseAddress}");
                    break;
                case Segment.Temp:
                    this.CodeWriter.WriteLine($"@{TempBaseAddress}");
                    break;
                case Segment.Static:
                    this.CodeWriter.WriteLine($"@{Path.GetFileNameWithoutExtension(instruction.FileName)}.{instruction.Offset}");
                    return;
                case Segment.Constant:
                    ErrorManager.DumpError("Constant segment cannot be referenced.", instruction);
                    this.IsAllSuccessful = false;
                    return;
            }
            this.CodeWriter.WriteLine("D=A");
            this.CodeWriter.WriteLine($"@{instruction.Offset}");
            this.CodeWriter.WriteLine("A=D+A");
        }
    }
}