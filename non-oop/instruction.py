from enum import Enum
from collections import namedtuple

class InstructionKind(Enum):
    PUSH = "push"
    POP = "pop"
    ADD = "add"
    SUB = "sub"
    NEG = "neg"
    EQ = "eq"
    GT = "gt"
    LT = "lt"
    AND = "and"
    OR = "or"
    NOT = "not"

memory_access_instructions = { InstructionKind.PUSH, InstructionKind.POP }
arithmetic_instructions = { 
    InstructionKind.ADD, InstructionKind.SUB, InstructionKind.NEG, 
    InstructionKind.EQ, InstructionKind.GT, InstructionKind.LT, 
    InstructionKind.AND, InstructionKind.OR, InstructionKind.NOT }

class Segment(Enum):
    LOCAL = "local"
    ARGUMENT = "argument"
    STATIC = "static"
    CONSTANT = "constant"
    THIS = "this"
    THAT = "that"
    POINTER = "pointer"
    TEMP = "temp"

Instruction = namedtuple('Instruction', ['filename', 'kind', 'segment', 'index'])