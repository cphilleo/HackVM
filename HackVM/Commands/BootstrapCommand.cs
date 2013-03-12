using System.Collections.Generic;

namespace HackVM.Commands
{
    public class BootstrapCommand : ICommand
    {
        public List<string> GetAssembly()
        {
            var asm = new List<string>();

            //set SP to 256
            asm.Add("@256");
            asm.Add("D=A");
            asm.Add("@SP");
            asm.Add("M=D");

            //call Sys.init
            asm.AddRange(new CallCommand("Sys.init", "0").GetAssembly());

            return asm;
        }
    }
}