error_stream = None

def set_error_stream(stream):
    error_stream = stream

def print_error(*args):
    print("Error: ", file=error_stream, end="")
    print(*args, file=error_stream)

def print_message(*args):
    print(*args, file=error_stream)