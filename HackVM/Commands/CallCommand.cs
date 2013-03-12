using System;
using System.Collections.Generic;

namespace HackVM.Commands
{
    public class CallCommand : ICommand
    {
        private readonly string _name;
        private readonly int _numArgs;

        public CallCommand(string name, string numArgs)
        {
            _name = name;
            _numArgs = Convert.ToInt32(numArgs);
        }

        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            var ret = ParserContext.GetUniqueLabel("ReturnFrom_" + _name);

            //push ret address on stack
            asm.Add("@" + ret);
            asm.Add("D=A");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");

            //push LCL
            asm.Add("@LCL");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");

            //push ARG
            asm.Add("@ARG");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");

            //push THIS
            asm.Add("@THIS");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");

            //push THAT
            asm.Add("@THAT");
            asm.Add("D=M");
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            asm.Add("@SP");
            asm.Add("M=M+1");

            //Reposition ARG (SP - N - 5)
            asm.Add("@SP");
            asm.Add("D=M");
            var offset = _numArgs + 5;
            asm.Add(string.Format("@{0}", offset));
            asm.Add("D=D-A");
            asm.Add("@ARG");
            asm.Add("M=D");

            //Reposition LCL
            asm.Add("@SP");
            asm.Add("D=M");
            asm.Add("@LCL");
            asm.Add("M=D");

            //goto function
            asm.Add("@" + _name);
            asm.Add("0;JMP");

            //write return label
            asm.Add(string.Format("({0})", ret));

            return asm;
        }
    }
}