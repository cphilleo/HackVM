using System;
using System.Collections.Generic;

namespace HackVM.Commands
{
    public class PopCommand : ICommand
    {
        private readonly string _segment;
        private readonly string _index;

        public PopCommand(string segment, string index)
        {
            _segment = segment;
            _index = index;
        }

        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            //Pop value from top of stack into D
            asm.Add("@SP");
            asm.Add("AM=M-1");
            asm.Add("D=M");

            switch(_segment)
            {
                case "constant":
                    //Not sure if this even makes sense
                    break;
                case "local":
                    //get value from local + index into D
                    asm.Add("@LCL");
                    asm.Add("A=M");
                    for (var i = 0; i < Convert.ToInt32(_index); i++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("M=D");
                    break;
                case "argument":
                    //get value from argument + index into D
                    asm.Add("@ARG");
                    asm.Add("A=M");
                    for (var i = 0; i < Convert.ToInt32(_index); i++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("M=D");
                    break;
                case "this":
                    //get value from this + index into D
                    asm.Add("@THIS");
                    asm.Add("A=M");
                    for (var i = 0; i < Convert.ToInt32(_index); i++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("M=D");
                    break;
                case "that":
                    //get value from that + index into D
                    asm.Add("@THAT");
                    asm.Add("A=M");
                    for (var i = 0; i < Convert.ToInt32(_index); i++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("M=D");
                    break;
                case "temp":
                    //get value from that + index into D
                    asm.Add("@5");
                    for (var i = 0; i < Convert.ToInt32(_index); i++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("M=D");
                    break;
                case "pointer":
                    //get value from that + index into D
                    asm.Add("@3");
                    for (var i = 0; i < Convert.ToInt32(_index); i++)
                    {
                        asm.Add("A=A+1");
                    }
                    asm.Add("M=D");
                    break;
                case "static":
                    //get value from that + index into D
                    asm.Add("@" + ParserContext.GetStatic(_index));
                    asm.Add("M=D");
                    break;
            }

            return asm;
        }
    }
}