using System.Collections.Generic;

namespace HackVM.Commands
{
    public class EqualityCommand : ICommand
    {
        private readonly string _op;

        public EqualityCommand(string op)
        {
            _op = op;
        }

        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            var matchLabel = ParserContext.GetUniqueLabel("MATCH");
            var endLabel = ParserContext.GetUniqueLabel("END");

            //pop y into D
            asm.Add("@SP");
            asm.Add("M=M-1");
            asm.Add("A=M");
            asm.Add("D=M");
            //pop x into A
            asm.Add("@SP");
            asm.Add("M=M-1");
            asm.Add("A=M");
            asm.Add("A=M");
            //check x op y
            asm.Add("D=A-D");
            asm.Add("@" + matchLabel);
            asm.Add(string.Format("D;J{0}", _op));
            //not match
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=0");
            asm.Add("@" + endLabel);
            asm.Add("0;JMP");
            //match
            asm.Add(string.Format("({0})", matchLabel));
            asm.Add("@SP");
            asm.Add("A=M");
            asm.Add("M=-1");
            //end
            asm.Add(string.Format("({0})", endLabel));
            //inc SP
            asm.Add("@SP");
            asm.Add("M=M+1");

            return asm;
        }
    }
}