from instruction import InstructionKind, Segment
import error_manager as em

def generate(ostream, instructions):
    for instruction in instructions:
        generate_one_line(ostream, instruction)

lnum = 1

def generate_one_line(ostream, instruction):
    global lnum
    if instruction.kind == InstructionKind.PUSH:
        refer_segment_num(ostream, instruction)
        print("@SP", file=ostream)
        print("A=M", file=ostream)
        print("M=D", file=ostream)
        print("@SP", file=ostream)
        print("M=M+1", file=ostream)
    elif instruction.kind == InstructionKind.POP:
        print("@SP", file=ostream)
        print("AM=M-1", file=ostream)
        print("D=M", file=ostream)
        print("@R13", file=ostream)
        print("M=D", file=ostream)
        refer_segment(ostream, instruction)
        print("D=A", file=ostream)
        print("@R14", file=ostream)
        print("M=D", file=ostream)
        print("@R13", file=ostream)
        print("D=M", file=ostream)
        print("@R14", file=ostream)
        print("A=M", file=ostream)
        print("M=D", file=ostream)
    elif instruction.kind == InstructionKind.ADD:
        print("@SP", file=ostream)
        print("AM=M-1", file=ostream)
        print("D=M", file=ostream)
        print("A=A-1", file=ostream)
        print("M=D+M", file=ostream)
    elif instruction.kind == InstructionKind.SUB:
        print("@SP", file=ostream)
        print("AM=M-1", file=ostream)
        print("D=M", file=ostream)
        print("A=A-1", file=ostream)
        print("M=M-D", file=ostream)
    elif instruction.kind == InstructionKind.NEG:
        print("@SP", file=ostream)
        print("A=M-1", file=ostream)
        print("M=-M", file=ostream)
    elif instruction.kind == InstructionKind.EQ:
        print("@SP", file=ostream)
        print("AM=M-1", file=ostream)
        print("D=M", file=ostream)
        print("A=A-1", file=ostream)
        print("D=M-D", file=ostream)
        print(f"@.Ltrue{lnum}", file=ostream)
        print("D;JEQ", file=ostream)
        print("D=0", file=ostream)
        print(f"@.Lend{lnum}", file=ostream)
        print("0;JMP", file=ostream)
        print(f"(.Ltrue{lnum})", file=ostream)
        print("D=-1", file=ostream)
        print(f"(.Lend{lnum})", file=ostream)
        print("@SP", file=ostream)
        print("A=M-1", file=ostream)
        print("M=D", file=ostream)
        lnum += 1
    elif instruction.kind == InstructionKind.GT:
        print("@SP", file=ostream)
        print("AM=M-1", file=ostream)
        print("D=M", file=ostream)
        print("A=A-1", file=ostream)
        print("D=M-D", file=ostream)
        print(f"@.Ltrue{lnum}", file=ostream)
        print("D;JGT", file=ostream)
        print("D=0", file=ostream)
        print(f"@.Lend{lnum}", file=ostream)
        print("0;JMP", file=ostream)
        print(f"(.Ltrue{lnum})", file=ostream)
        print("D=-1", file=ostream)
        print(f"(.Lend{lnum})", file=ostream)
        print("@SP", file=ostream)
        print("A=M-1", file=ostream)
        print("M=D", file=ostream)
        lnum += 1
    elif instruction.kind == InstructionKind.LT:
        print("@SP", file=ostream)
        print("AM=M-1", file=ostream)
        print("D=M", file=ostream)
        print("A=A-1", file=ostream)
        print("D=D-M", file=ostream)
        print(f"@.Ltrue{lnum}", file=ostream)
        print("D;JGT", file=ostream)
        print("D=0", file=ostream)
        print(f"@.Lend{lnum}", file=ostream)
        print("0;JMP", file=ostream)
        print(f"(.Ltrue{lnum})", file=ostream)
        print("D=-1", file=ostream)
        print(f"(.Lend{lnum})", file=ostream)
        print("@SP", file=ostream)
        print("A=M-1", file=ostream)
        print("M=D", file=ostream)
        lnum += 1
    elif instruction.kind == InstructionKind.AND:
        print("@SP", file=ostream)
        print("AM=M-1", file=ostream)
        print("D=M", file=ostream)
        print("A=A-1", file=ostream)
        print("M=D&M", file=ostream)
    elif instruction.kind == InstructionKind.OR:
        print("@SP", file=ostream)
        print("AM=M-1", file=ostream)
        print("D=M", file=ostream)
        print("A=A-1", file=ostream)
        print("M=D|M", file=ostream)
    elif instruction.kind == InstructionKind.NOT:
        print("@SP", file=ostream)
        print("A=M-1", file=ostream)
        print("M=!M", file=ostream)
    else:
        em.print_error("Some error occurred.")

def refer_segment_num(ostream, instruction):
    segment = instruction.segment
    if segment == Segment.CONSTANT:
        print(f"@{instruction.index}", file=ostream)
        print("D=A", file=ostream)
    else:
        refer_segment(ostream, instruction)
        print("D=M", file=ostream)

POINTER_BASE = 3
TEMP_BASE = 5

def refer_segment(ostream, instruction):
    segment = instruction.segment
    if segment == Segment.LOCAL:
        print("@LCL", file=ostream)
        print("A=M", file=ostream)
    elif segment == Segment.ARGUMENT:
        print("@ARG", file=ostream)
        print("A=M", file=ostream)
    elif segment == Segment.THIS:
        print("@THIS", file=ostream)
        print("A=M", file=ostream)
    elif segment == Segment.THAT:
        print("@THAT", file=ostream)
        print("A=M", file=ostream)
    elif segment == Segment.POINTER:
        print(f"@{POINTER_BASE}", file=ostream)
    elif segment == Segment.TEMP:
        print(f"@{TEMP_BASE}", file=ostream)
    elif segment == Segment.STATIC:
        print(f"@{instruction.filename}.{instruction.index}", file=ostream)
        return
    elif segment == Segment.CONSTANT:
        em.print_error("Constant segment cannot be referenced.")
        return
    else:
        em.print_error("Some error occurred.")
        return
    if True:
        # TODO: Static
        print("D=A", file=ostream)
        print(f"@{instruction.index}", file=ostream)
        print("A=D+A", file=ostream)
