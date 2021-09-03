#include "vmtranslator_all.h"

int main(int argc, char **argv)
{
    if (argc != 2)
    {
        std::cerr << "Error : No source file or directory specified." 
            << std::endl;
        std::cerr << "Usage: VMtranslator [path]" << std::endl;
        exit(EXIT_FAILURE);
    }

    fs::path path(argv[1]);
    std::vector<fs::path> paths;
    if (fs::is_directory(path))
    {
        for (auto const &entry : fs::directory_iterator(path))
        {
            auto entry_path = entry.path();
            if (entry_path.extension() == ".vm")
                paths.push_back(entry_path);
        }
    }
    else if (fs::is_regular_file(path))
    {
        if (path.extension() == ".vm")
            paths.push_back(path);
        else
        {
            std::cerr << "Error: The file extension must be '.vm'." << std::endl;
            exit(EXIT_FAILURE);
        }
    }
    else
    {
        std::cerr << "Error: The file or directory named '" 
            << path << "' does not exist." << std::endl;
        exit(EXIT_FAILURE);
    }

    // Create generator
    path.replace_extension(".asm");
    std::ofstream assembly(path);
    if (!assembly.is_open())
    {
        std::cerr << "Error: Assembly file '" << path
            << "' could not open." << std::endl;
        exit(EXIT_FAILURE);
    }
    CodeGenerator generator(&assembly);

    // Parse
    auto instructions = new std::vector<Instruction *>();
    for (auto const &sourcepath : paths)
    {
        std::ifstream source(sourcepath);
        if (!source.is_open())
        {
            std::cerr << "Error: Source file '" << sourcepath 
                << "' could not open." << std::endl;
            exit(EXIT_FAILURE);
        }
        Parser parser(&source, sourcepath.filename().replace_extension(""));
        parser.parse(instructions);
        generator.generate(instructions);
        source.close();
        instructions->clear();
    }
    delete instructions;

    assembly.close();
    return EXIT_SUCCESS;
}