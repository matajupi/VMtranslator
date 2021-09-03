from instruction import Instruction, InstructionKind, Segment, \
     memory_access_instructions, arithmetic_instructions
import error_manager as em

def parse(istream, filename):
    instructions = []
    for line in istream:
        instruction = parse_one_line(line, filename)
        if instruction == None:
            continue
        instructions.append(instruction)
    return instructions

def parse_one_line(line, filename):
    chunks = line.split()
    if "//" in chunks:
        chunks = chunks[:chunks.index("//")]
        
    if len(chunks) == 0:
        return None

    kind = get_instruction_kind(chunks[0])
    segment = None
    index = None
    if kind == None:
        em.print_error(f"The instruction named '{chunks[0]}' does not exist.")
        return None
    
    if len(chunks) == 3 and kind in memory_access_instructions:
        segment = get_segment(chunks[1])
        if segment == None:
            em.print_error(f"The segment named '{chunks[1]}' does not exist.")
            return None
        index = chunks[2]
        if not index.isdecimal():
            em.print_error("The index part of the instruction must be a decimal number.")
            return None
    elif len(chunks) == 1 and kind in arithmetic_instructions:
        pass # Do nothing
    else:
        em.print_error("The number of argument is invalid.")
        return None

    instruction = Instruction(filename=filename, kind=kind, segment=segment, index=index)
    return instruction

def get_instruction_kind(kindstr):
    for kind in InstructionKind:
        if kind.value == kindstr:
            return kind
    return None

def get_segment(segstr):
    for seg in Segment:
        if seg.value == segstr:
            return seg
    return None