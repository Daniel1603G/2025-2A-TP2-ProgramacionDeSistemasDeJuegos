
public class AliasesCommand : IConsoleCommand
{

    public string Name => "aliases";
    public string[] Aliases => new[] { "alias", "as" };
    public string Description => "aliases [command] - Show command aliases.";

    public void Execute(string[] args)
    {
        var console = CommandConsole.Instance;
        if (args.Length == 0)
        {
            console.AppendLog("Usage: aliases [command]");
            return;
        }

        var cmdName = args[0];
        if (console.TryGetCommandInfo(cmdName, out var cmd))
        {
            var list = string.Join(", ", cmd.Aliases);
            console.AppendLog($"Aliases of {cmd.Name}: {list}");
        }
        else
        {
            console.AppendLog($"Command '{cmdName}' not found.");
        }
    }
}   
