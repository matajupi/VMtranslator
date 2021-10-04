using System;
using System.Collections.Generic;

namespace VMtranslator
{
    class Instruction
    {
        public InstructionKind Kind { get; set; }
        public Segment Segment { get; set; }
        public string Identifier { get; set; }
        public int RowNumber { get; set; }
        public int Number { get; set; }
        public int Offset { get; set; }
        public string FileName { get; set; }

        public static Dictionary<string, InstructionKind> ArithmeticInstructionTable = new Dictionary<string, InstructionKind>()
        {
            { "add" , InstructionKind.Add  },
            { "sub" , InstructionKind.Sub  },
            { "neg" , InstructionKind.Neg  },
            { "eq"  , InstructionKind.Eq   },
            { "gt"  , InstructionKind.Gt   },
            { "lt"  , InstructionKind.Lt   },
            { "and" , InstructionKind.And  },
            { "or"  , InstructionKind.Or   },
            { "not" , InstructionKind.Not  },
        };

        public static Dictionary<string, InstructionKind> MemoryAccessInstructionTable = new Dictionary<string, InstructionKind>()
        {
            { "push", InstructionKind.Push },
            { "pop" , InstructionKind.Pop  },
        };

        public static Dictionary<string, InstructionKind> ProgramFlowInstructionTable = new Dictionary<string, InstructionKind>()
        {
            // TODO: Program flow
        };

        public static Dictionary<string, InstructionKind> FunctionCallInstructionTable = new Dictionary<string, InstructionKind>()
        {
            // TODO: Function call
        };

        public static Dictionary<string, Segment> SegmentTable = new Dictionary<string, Segment>()
        {
            { "argument", Segment.Argument },
            { "local"   , Segment.Local    },
            { "static"  , Segment.Static   },
            { "constant", Segment.Constant },
            { "this"    , Segment.This     },
            { "that"    , Segment.That     },
            { "pointer" , Segment.Pointer  },
            { "temp"    , Segment.Temp     },
        };
    }

    enum InstructionKind
    {
        Add,
        Sub,
        Neg,
        Eq,
        Gt,
        Lt,
        And,
        Or,
        Not,
        Push,
        Pop,
    }

    enum Segment
    {
        Argument,
        Local,
        Static,
        Constant,
        This,
        That,
        Pointer,
        Temp,
    }
}