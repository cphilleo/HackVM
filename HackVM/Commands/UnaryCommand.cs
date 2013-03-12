using System.Collections.Generic;

namespace HackVM.Commands
{
    public class UnaryCommand : ICommand
    {
        private readonly string _op;

        public UnaryCommand(string op)
        {
            _op = op;
        }

        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            //pop y into D
            asm.Add("@SP");
            asm.Add("M=M-1");
            asm.Add("A=M");
            asm.Add("D=M");
            //do in place op 
            asm.Add(string.Format("M={0}D", _op));
            //inc SP
            asm.Add("@SP");
            asm.Add("M=M+1");

            return asm;
        }
    }
}