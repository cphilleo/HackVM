using System;
using System.Collections.Generic;

namespace HackVM.Commands
{
    public class PushCommand : ICommand
    {
        private readonly string _segment;
        private readonly string _index;

        public PushCommand(string segment, string index)
        {
            _segment = segment;
            _index = index;
        }

        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            switch(_segment)
            {
                case "constant":
                    //store constant value in D
                    asm.Add("@" + _index);
                    asm.Add("D=A");
                    break;
                case "local":
                    //get value from local + index into D
                    asm.Add("@LCL");
                    asm.Add("A=M");
                    for (var i = 0; i < Convert.ToInt32(_index); i ++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("D=M");
                    break;
                case "argument":
                    //get value from argument + index into D
                    asm.Add("@ARG");
                    asm.Add("A=M");
                    for (var i = 0; i < Convert.ToInt32(_index); i ++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("D=M");
                    break;
                case "this":
                    //get value from this + index into D
                    asm.Add("@THIS");
                    asm.Add("A=M");
                    for (var i = 0; i < Convert.ToInt32(_index); i ++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("D=M");
                    break;
                case "that":
                    //get value from that + index into D
                    asm.Add("@THAT");
                    asm.Add("A=M");
                    for (var i = 0; i < Convert.ToInt32(_index); i ++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("D=M");
                    break;
                case "temp":
                    //get value from that + index into D
                    asm.Add("@5");
                    for (var i = 0; i < Convert.ToInt32(_index); i ++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("D=M");
                    break;
                case "pointer":
                    //get value from that + index into D
                    asm.Add("@3");
                    for (var i = 0; i < Convert.ToInt32(_index); i ++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("D=M");
                    break;
                case "static":
                    //get value from that + index into D
                    asm.Add("@" + ParserContext.GetStatic(_index));
                    asm.Add("D=M");
                    break;
            }

            //store D in RAM[SP]
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=D");
            //increment SP
            asm.Add("@SP");
            asm.Add("M=M+1");

            return asm;
        }
    }
}