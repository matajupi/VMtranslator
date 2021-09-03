#ifndef VMTRANSLATOR_PARSER_H
#define VMTRANSLATOR_PARSER_H

#include "vmtranslator_all.h"


class Parser
{
    std::istream *source;
    std::string current_instruction;
    std::string filename;

    bool is_digit(const std::string &str)
    {
        for (const char &c : str)
            if (!std::isdigit(c))
                return false;
        return true;
    }

    Segment to_segment(const std::string &str)
    {
        if (str == "local")
                return Segment::LOCAL;
        if (str == "argument")
                return Segment::ARGUMENT;
        if (str == "this")
                return Segment::THIS;
        if (str == "that")
                return Segment::THAT;
        if (str == "pointer")
                return Segment::POINTER;
        if (str == "temp")
                return Segment::TEMP;
        if (str == "constant")
                return Segment::CONSTANT;
        if (str == "static")
                return Segment::STATIC;
        return static_cast<Segment>(-1);
    }
public:
    Parser(std::istream *source, std::string filename)
        : source(source), filename(filename)
    {
    }

    void parse(std::vector<Instruction *> *instructions)
    {
        while (move_next())
            instructions->push_back(parse_one_line());
    }

    bool move_next()
    {
        return !std::getline(*source, current_instruction).eof();
    }

    Instruction *parse_one_line()
    {
        std::stringstream ss(current_instruction);
        std::vector<std::string> chunks;
        for (std::string chunk; std::getline(ss, chunk, ' '); )
        {
            if (chunk == "//")
                break;
            if (chunk.empty())
                continue;
            chunks.push_back(chunk);
        }
        
        auto instruction = new Instruction();
        instruction->filename = filename;
        if (chunks.size() == 1)
        {
            auto type = chunks.at(0);
            if (type == "add")
                instruction->type = InstructionType::ADD;
                // TODO:
            else
            {
                std::cerr << "Error: The instruction '" << chunks.at(0)
                    << "' is undefined." << std::endl;
                delete instruction;
                exit(EXIT_FAILURE);
            }
        }
        else if (chunks.size() == 2)
        {
            // TODO:
        }
        else if (chunks.size() == 3)
        {
            auto type = chunks.at(0);
            auto arg1 = chunks.at(1);
            auto arg2 = chunks.at(2);
            if (type == "push")
            {
                instruction->type = InstructionType::PUSH;
                goto push_pop;
            }
            else if (type == "pop")
            {
                instruction->type = InstructionType::POP;
            push_pop:
                Segment segment;
                if ((segment = to_segment(arg1)))
                {
                    std::cerr << "Error: The segment named '" << arg1
                        << "' is undefined." << std::endl;
                    delete instruction;
                    exit(EXIT_FAILURE);
                }
                instruction->segment = segment;
                if (!is_digit(arg2))
                {
                    std::cerr << "Error: The second argument of this instruction must be a number."
                        << std::endl;
                    delete instruction;
                    exit(EXIT_FAILURE);
                }
                instruction->index = arg2;
            }
            else
            {
                std::cerr << "Error: The instruction '" << chunks.at(0)
                        << "' is undefined." << std::endl;
                delete instruction;
                exit(EXIT_FAILURE);
            }
        }
        else
        {
            delete instruction;
            instruction = nullptr;
        }
        return instruction;
    }
};


#endif // VMTRANSLATOR_PARSER_H