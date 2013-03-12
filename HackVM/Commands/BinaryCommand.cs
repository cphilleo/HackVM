using System.Collections.Generic;

namespace HackVM.Commands
{
    public class BinaryCommand : ICommand
    {
        private readonly string _op;

        public BinaryCommand(string op)
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
            //do in place operation
            asm.Add("A=A-1");
            asm.Add(string.Format("M=M{0}D", _op));

            return asm;
        }
    }
}