@17
D=A
@SP
A=M
M=D
@SP
M=M+1
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue1
D;JEQ
D=0
@.Lend1
0;JMP
(.Ltrue1)
D=-1
(.Lend1)
@SP
A=M-1
M=D
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
@16
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue2
D;JEQ
D=0
@.Lend2
0;JMP
(.Ltrue2)
D=-1
(.Lend2)
@SP
A=M-1
M=D
@16
D=A
@SP
A=M
M=D
@SP
M=M+1
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue3
D;JEQ
D=0
@.Lend3
0;JMP
(.Ltrue3)
D=-1
(.Lend3)
@SP
A=M-1
M=D
@892
D=A
@SP
A=M
M=D
@SP
M=M+1
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue4
D;JLT
D=0
@.Lend4
0;JMP
(.Ltrue4)
D=-1
(.Lend4)
@SP
A=M-1
M=D
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
@892
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue5
D;JLT
D=0
@.Lend5
0;JMP
(.Ltrue5)
D=-1
(.Lend5)
@SP
A=M-1
M=D
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue6
D;JLT
D=0
@.Lend6
0;JMP
(.Ltrue6)
D=-1
(.Lend6)
@SP
A=M-1
M=D
@32767
D=A
@SP
A=M
M=D
@SP
M=M+1
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue7
D;JGT
D=0
@.Lend7
0;JMP
(.Ltrue7)
D=-1
(.Lend7)
@SP
A=M-1
M=D
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
@32767
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue8
D;JGT
D=0
@.Lend8
0;JMP
(.Ltrue8)
D=-1
(.Lend8)
@SP
A=M-1
M=D
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
D=M-D
@.Ltrue9
D;JGT
D=0
@.Lend9
0;JMP
(.Ltrue9)
D=-1
(.Lend9)
@SP
A=M-1
M=D
@57
D=A
@SP
A=M
M=D
@SP
M=M+1
@31
D=A
@SP
A=M
M=D
@SP
M=M+1
@53
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
M=D+M
@112
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
M=M-D
@SP
A=M-1
M=-M
@SP
AM=M-1
D=M
A=A-1
M=D&M
@82
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
M=D|M
@SP
A=M-1
M=!M
