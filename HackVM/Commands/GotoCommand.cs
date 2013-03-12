using System.Collections.Generic;

namespace HackVM.Commands
{
    public class GotoCommand : ICommand
    {
        private readonly string _name;
        private readonly bool _conditional;

        public GotoCommand(string name, bool conditional)
        {
            _name = name;
            _conditional = conditional;
        }

        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            if (_conditional)
            {
                //pop SP into D
                asm.Add("@SP");
                asm.Add("M=M-1");
                asm.Add("A=M");
                asm.Add("D=M");

                //load address into A
                asm.Add(string.Format("@{0}", ParserContext.GetLabelFor(_name)));
                
                //jump if true
                asm.Add("D;JNE");
            }

            else
            {
                //load address into A
                asm.Add(string.Format("@{0}", ParserContext.GetLabelFor(_name)));

                //unconditional jump
                asm.Add("0;JMP");
            }
            
            return asm;
        }
    }
}