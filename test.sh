#!bin/bash
emulator_path="../../tools/CPUEmulator.sh"

assert() {
    filename="$1"
    ./VMtranslator "$filename".vm
    sh "$emulator_path" "$filename".tst > /dev/null

    if [ "$?" = 0 ]; then
        echo "$filename.vm => Success"
    else
        echo "$filename.vm => Failure"
        exit 1
    fi
}

echo OK
