CFLAGS=-std=c11 -g -static
SRCS=$(wildcard *.c)
OBJS=$(SRCS:.c=.o)

VMtranslator: $(OBJS)
	gcc -o VMtranslator $(OBJS) $(LDFLAGS)

$(OBJS): vm_translator.h

test: VMtranslator
	sh test.sh

clean:
	rm -f VMtranslator *.o *~