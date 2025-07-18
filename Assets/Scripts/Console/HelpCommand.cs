public class HelpCommand : IConsoleCommand
{
    private ICommandRegistry registry;

    public HelpCommand(ICommandRegistry registry)
    {
        this.registry = registry;
    }

    public string Name
    {
        get { return "help"; }
    }

    public string[] Aliases
    {
        get { return new string[] { "h", "?" }; }
    }

    public string Description
    {
        get { return "help [command] - List commands or show details."; }
    }

    public void Execute(string[] args)
    {
        CommandConsole console = CommandConsole.Instance;

      
        if (args.Length == 0)
        {
            console.AppendLog("Available commands:");
            foreach (IConsoleCommand cmd in registry.Commands)
            {
                console.AppendLog("- " + cmd.Name + ": " + cmd.Description);
            }
            return;
        }

    
        string cmdName = args[0];
        IConsoleCommand found;
        bool exists = registry.TryGetCommandInfo(cmdName, out found);

        if (exists)
        {
            console.AppendLog(found.Name + ": " + found.Description);
        }
        else
        {
            console.AppendLog("No help for '" + cmdName + "'.");
        }
    }
}