public class HelpCommand : IConsoleCommand
{
    private readonly ICommandRegistry _registry;
    public HelpCommand(ICommandRegistry registry) => _registry = registry;

    public string Name => "help";
    public string[] Aliases => new[] { "h", "?" };
    public string Description => "help [command] - List commands or show details.";

    public void Execute(string[] args)
    {
        var console = CommandConsole.Instance;

        if (args.Length == 0)
        {
            console.AppendLog("Available commands:");
        
            foreach (var command in _registry.Commands)
                console.AppendLog($"- {command.Name}: {command.Description}");
            return;
        }

        var cmdName = args[0];
      
        if (_registry.TryGetCommandInfo(cmdName, out var found))
            console.AppendLog($"{found.Name}: {found.Description}");
        else
            console.AppendLog($"No help found for '{cmdName}'.");
    }
}