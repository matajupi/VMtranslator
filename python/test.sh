#!bin/bash
emulator_path="../../../tools/CPUEmulator.sh"

assert() {
    filename="$1"
    python3 main.py "$filename.vm"
    sh "$emulator_path" "$filename.tst"

    if [ "$?" = 0 ]; then
        echo "$filename.vm => Success"
    else
        echo "$filename.vm => Failure"
        exit 1
    fi
}

assert "../tests/StackArithmetic/SimpleAdd/SimpleAdd"
assert "../tests/StackArithmetic/StackTest/StackTest"
assert "../tests/MemoryAccess/BasicTest/BasicTest"
assert "../tests/MemoryAccess/PointerTest/PointerTest"
assert "../tests/MemoryAccess/StaticTest/StaticTest"
echo OK
