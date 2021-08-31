GCCOPS=-std=c++20 -Wall
SRCS = main.cc CodeWriter.h Parser.h vmtranslator_all.h

VMtranslator: $(SRCS)
	g++ $(GCCOPS) $< -o $@

test: VMtranslator
	sh test.sh

clean:
	rm -f ./VMtranslator