using System.Collections.Generic;

namespace HackVM.Commands
{
    public class ReturnCommand : ICommand
    {
        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            //store LCL in temp var FRAME
            asm.Add("@LCL");
            asm.Add("D=M");
            asm.Add("@R14");
            asm.Add("M=D");

            //store return address in temp (RET = *(LCL-5)
            asm.Add("@5");
            asm.Add("A=D-A"); //D = *LCL
            asm.Add("D=M"); //D = *LCL - 5
            asm.Add("@R15");
            asm.Add("M=D");

            //Reposition return value (*ARG = pop())
            //pop into D
            asm.Add("@SP");
            asm.Add("AM=M-1");
            asm.Add("D=M");
            //store D in ARG
            asm.Add("@ARG");
            asm.Add("A=M");
            asm.Add("M=D");

            //Restore SP of caller (*SP = *ARG + 1)
            asm.Add("@ARG");
            asm.Add("D=M+1");
            asm.Add("@SP");
            asm.Add("M=D");

            //Restore THAT (*(FRAME - 1)
            asm.Add("@R14");
            asm.Add("AM=M-1");
            asm.Add("D=M");
            asm.Add("@THAT");
            asm.Add("M=D");

            //Restore THIS (*(FRAME - 2)
            asm.Add("@R14");
            asm.Add("AM=M-1");
            asm.Add("D=M");
            asm.Add("@THIS");
            asm.Add("M=D");

            //Restore ARG (*(FRAME - 3)
            asm.Add("@R14");
            asm.Add("AM=M-1");
            asm.Add("D=M");
            asm.Add("@ARG");
            asm.Add("M=D");

            //Restore LCL (*(FRAME - 4)
            asm.Add("@R14");
            asm.Add("AM=M-1");
            asm.Add("D=M");
            asm.Add("@LCL");
            asm.Add("M=D");

            //goto return address
            asm.Add("@R15");
            asm.Add("A=M");
            asm.Add("0;JMP");

            return asm;
        }
    }
}