import sys
import os
import io
import error_manager as em
import parser
import generator

if __name__ == "__main__":
    em.set_error_stream(sys.stderr)

    # Get file or directory path
    args = sys.argv
    if len(args) != 2:
        em.print_error("Requires two command line arguments.")
        em.print_message("Usage: python3 main.py [vm file path | directory path]")
        sys.exit(1)
    path = args[1]

    # Get vm files
    if not os.path.exists(path):
        em.print_error("The file or directory named '" + path + "' does not exist.")
        sys.exit(1)
    file_paths = []
    if os.path.isfile(path):
        _, ext = os.path.splitext(path)
        if ext != ".vm":
            em.print_error("The file extension must be '.vm'.")
            sys.exit(1)
        file_paths.append(path)
    else:
        for entry_path in os.listdir(path):
            _, ext = os.splitext(entry_path)
            if os.isfile(entry_path) and ext == ".vm":
                file_paths.append(entry_path)
    
    # Parse vm code and generate assembly code.
    asm_path = os.path.splitext(path)[0] + ".asm"
    with open(asm_path, 'w') as ostream:
        for p in file_paths:
            with open(p, 'r') as istream:
                filename = os.path.splitext(os.path.basename(p))[0]
                instructions = parser.parse(istream, filename)
                generator.generate(ostream, instructions)
