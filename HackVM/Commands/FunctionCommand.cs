using System;
using System.Collections.Generic;

namespace HackVM.Commands
{
    public class FunctionCommand : ICommand
    {
        private readonly string _name;
        private readonly int _numLocals;

        public FunctionCommand(string name, string numLocals)
        {
            _name = name;
            _numLocals = Convert.ToInt32(numLocals);
        }

        public List<string> GetAssembly()
        {
            //set parser context in function
            ParserContext.SetFunction(_name);

            var asm = new List<string>();

            //function name label
            asm.Add(string.Format("({0})", _name));

            //push local variables
            for (var i = 0; i < _numLocals; i++)
            {
                asm.Add("@SP");
                asm.Add("A=M");
                asm.Add("M=0");
                asm.Add("@SP");
                asm.Add("M=M+1");
            }

            return asm;
        }
    }
}