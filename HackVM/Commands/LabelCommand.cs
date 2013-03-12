using System.Collections.Generic;

namespace HackVM.Commands
{
    public class LabelCommand : ICommand
    {
        private readonly string _name;

        public LabelCommand(string name)
        {
            _name = name;
        }

        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            asm.Add(string.Format("({0})", ParserContext.GetLabelFor(_name)));

            return asm;
        }
    }
}