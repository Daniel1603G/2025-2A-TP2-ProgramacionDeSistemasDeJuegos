
using System.Collections.Generic;

    public interface ICommandRegistry
    {
        
        IEnumerable<IConsoleCommand> Commands { get; }
        bool TryGetCommandInfo(string name, out IConsoleCommand command);
    }
