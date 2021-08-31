#include "vmtranslator_all.h"

int main(int argc, char **argv)
{
    if (argc != 2)
    {
        std::cerr << "usage: VMtranslator path" << std::endl;
        return 1;
    }

    auto path = std::string(argv[1]);
    if (std::filesystem::is_directory(path))
    {
        // TODO: directory process
    }
    else if (std::filesystem::is_regular_file(path))
    {
        // TODO: file process
    }
    return 0;
}