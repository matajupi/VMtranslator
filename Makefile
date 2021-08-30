SRCS=main.c parser.c code_writer.c vm_translator.h

vmt.exe: $(SRCS)
	gcc $(SRCS) -o vmt.exe
