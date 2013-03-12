using System.Collections.Generic;

namespace HackVM.Commands
{
    public interface ICommand
    {
        List<string> GetAssembly();
    }
}