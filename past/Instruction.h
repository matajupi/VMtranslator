#ifndef VMTRANSLATOR_INSTRUCTION_H
#define VMTRANSLATOR_INSTRUCTION_H

#include "vmtranslator_all.h"

enum InstructionType
{
    // Arithmetic instruction
    ADD,
    SUB,
    NEG,
    EQ,
    GT,
    LT,
    AND,
    OR,
    NOT,
    // Memory access instruction
    PUSH,
    POP,
    // Program flow instruction
    LABEL,
    GOTO,
    IF,
    // Function call instruction
    FUNCTION,
    RETURN,
    CALL,
};

enum Segment
{
    LOCAL,
    ARGUMENT,
    THIS,
    THAT,
    POINTER,
    TEMP,
    CONSTANT,
    STATIC,
};

class Instruction
{
public:
    InstructionType type;
    Segment segment;
    std::string index;
    std::string filename;
};


#endif // VMTRANSLATOR_INSTRUCTION_H