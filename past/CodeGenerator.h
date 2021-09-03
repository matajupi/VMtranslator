#ifndef VMTRANSLATOR_CODEGENERATOR_H
#define VMTRANSLATOR_CODEGENERATOR_H

#include "vmtranslator_all.h"


class CodeGenerator
{
    std::ostream *assembly;
    void refer_address(Instruction *instruction)
    {
        refer_segment(instruction);
        *assembly << "A=M" << std::endl;
        if (instruction->segment != Segment::STATIC)
        {
            *assembly << "D=A" << std::endl;
            *assembly << "@" << instruction->index << std::endl;
            *assembly << "A=D+A" << std::endl;
        }
    }

    void refer_segment(Instruction *instruction)
    {
        switch (instruction->segment)
        {
            case Segment::LOCAL:
                *assembly << "@LCL" << std::endl;
                break;
            case Segment::ARGUMENT:
                *assembly << "@ARG" << std::endl;
                break;
            case Segment::THIS:
                *assembly << "@THIS" << std::endl;
                break;
            case Segment::THAT:
                *assembly << "@THAT" << std::endl;
                break;
            case Segment::POINTER:
                *assembly << "@R3" << std::endl;
                break;
            case Segment::TEMP:
                *assembly << "@R5" << std::endl;
                break;
            case Segment::CONSTANT:
                *assembly << "@" << instruction->index << std::endl;
                break;
            case Segment::STATIC:
                *assembly << "@" << instruction->filename 
                    << "." << instruction->index << std::endl;
                break;
        }
    }
public:
    CodeGenerator(std::ostream *assembly)
        : assembly(assembly)
    {
        // Epilogue
    }

    void generate(std::vector<Instruction *> *instructions)
    {
        for (auto instruction : *instructions)
        {
            generate_one_instruction(instruction);
        }
    }

    void generate_one_instruction(Instruction *instruction)
    {
        switch (instruction->type)
        {
            case InstructionType::ADD:
                *assembly << "@SP" << std::endl;
                *assembly << "M=M-1" << std::endl;
                *assembly << "A=M" << std::endl;
                *assembly << "D=M" << std::endl;
                *assembly << "A=A-1" << std::endl;
                *assembly << "M=D+M" << std::endl;
                break;
            case InstructionType::PUSH:
                if (instruction->segment == Segment::CONSTANT)
                {
                    refer_segment(instruction);
                    *assembly << "D=A" << std::endl;
                }
                else
                {
                    refer_address(instruction);
                    *assembly << "D=M" << std::endl;
                }
                *assembly << "@SP" << std::endl;
                *assembly << "A=M" << std::endl;
                *assembly << "M=D" << std::endl;
                *assembly << "@SP" << std::endl;
                *assembly << "M=M+1" << std::endl;
                break;
            case InstructionType::POP:
                *assembly << "@SP" << std::endl;
                *assembly << "M=M-1" << std::endl;
                *assembly << "A=M" << std::endl;
                *assembly << "D=M" << std::endl;
                *assembly << "@R5" << std::endl;
                *assembly << "M=D" << std::endl;
                refer_address(instruction);
                *assembly << "D=A" <<  std::endl;
                *assembly << "@R6" << std::endl;
                *assembly << "M=D" << std::endl;
                *assembly << "@R5" << std::endl;
                *assembly << "D=M" << std::endl;
                *assembly << "@R6" << std::endl;
                *assembly << "A=M" << std::endl;
                *assembly << "M=D" << std::endl;
                break;
        }
    }
};


#endif // VMTRANSLATOR_CODEGENERATOR_H