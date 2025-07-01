using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandConsole : MonoBehaviour, ILogHandler
{
    [Header("UI References")]
    [SerializeField] private Canvas consoleCanvas;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private Button toggleButton;

    [Header("Animation Config")]
    [SerializeField] private AnimationNamesConfig animationNamesConfig;

    private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase);
    private ILogHandler defaultLogHandler;

    private void Awake()
    {
        defaultLogHandler = Debug.unityLogger.logHandler;
        Debug.unityLogger.logHandler = this;

        RegisterCommand(new HelpCommand());
        RegisterCommand(new AliasesCommand());
        RegisterCommand(new PlayAnimationCommand(animationNamesConfig));
        RegisterCommand(new PrintUserNameCommand());

        consoleCanvas.enabled = false;
        toggleButton.onClick.AddListener(ToggleConsole);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) ToggleConsole();

        if (consoleCanvas.enabled && Input.GetKeyDown(KeyCode.Return))
        {
            var line = inputField.text;
            inputField.text = string.Empty;
            inputField.ActivateInputField();
            ProcessInput(line);
        }
    }

    private void ToggleConsole()
    {
        consoleCanvas.enabled = !consoleCanvas.enabled;
        if (consoleCanvas.enabled) inputField.ActivateInputField();
    }

    private void RegisterCommand(ICommand cmd)
    {
        commands[cmd.Name] = cmd;
        foreach (var alias in cmd.Aliases)
            commands[alias] = cmd;
    }

    private void ProcessInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var name = parts[0];
        var args = parts.Length > 1 ? parts.Skip(1).ToArray() : Array.Empty<string>();

        if (commands.TryGetValue(name, out var cmd))
        {
            try { cmd.Execute(args, this); }
            catch (Exception ex) { LogException(ex, this); }
        }
        else
        {
            Log($"Unknown command '{name}'. Type 'help' to list commands.");
        }
    }

    public void Log(string message)
    {
        outputText.text += message + "\n";
        defaultLogHandler.LogFormat(LogType.Log, null, message);
    }

    #region ILogHandler
    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        defaultLogHandler.LogFormat(logType, context, format, args);
        var msg = string.Format(format, args);
        Log($"[{logType}] {msg}");
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        defaultLogHandler.LogException(exception, context);
        Log($"[Exception] {exception.Message}");
    }
    #endregion

    #region ICommand

    public interface ICommand
    {
        string Name { get; }
        string[] Aliases { get; }
        string Description { get; }
        void Execute(string[] args, CommandConsole console);
    }

    private class HelpCommand : ICommand
    {
        public string Name => "help";
        public string[] Aliases => new[] { "h" };
        public string Description => "help [command] - List commands or show details.";
        public void Execute(string[] args, CommandConsole console)
        {
            if (args.Length == 0)
            {
                console.Log("Available commands:");
                foreach (var cmd in console.commands.Values.GroupBy(c => c.Name).Select(g => g.First()))
                    console.Log($"- {cmd.Name}: {cmd.Description}");
            }
            else
            {
                var cmdName = args[0];
                if (console.commands.TryGetValue(cmdName, out var cmd))
                    console.Log($"{cmd.Name}: {cmd.Description}");
                else
                    console.Log($"No help found for '{cmdName}'.");
            }
        }
    }

    private class AliasesCommand : ICommand
    {
        public string Name => "aliases";
        public string[] Aliases => new[] { "alias", "as" };
        public string Description => "aliases [command] - Show command aliases.";
        public void Execute(string[] args, CommandConsole console)
        {
            if (args.Length == 0) { console.Log("Usage: aliases [command]"); return; }
            var cmdName = args[0];
            if (console.commands.TryGetValue(cmdName, out var cmd))
                console.Log($"{cmd.Name} aliases: {string.Join(", ", cmd.Aliases)}");
            else
                console.Log($"Command '{cmdName}' not found.");
        }
    }

    private class PlayAnimationCommand : ICommand
    {
        public string Name => "playanimation";
        public string[] Aliases => new[] { "pa" };
        public string Description => "playanimation [name] - Play animation on all characters.";

        private readonly AnimationNamesConfig config;
        public PlayAnimationCommand(AnimationNamesConfig cfg) => config = cfg;

        public void Execute(string[] args, CommandConsole console)
        {
            if (args.Length == 0) { console.Log("Usage: playanimation [animationName]"); return; }
            var animName = args[0];
            if (!config.animationNames.Contains(animName))
            {
                console.Log($"Animation '{animName}' not found. Valid names: {string.Join(", ", config.animationNames)}");
                return;
            }
            var characters = UnityEngine.Object.FindObjectsOfType<Character>();
            int played = 0, total = characters.Length;
            foreach (var character in characters)
            {
                var animator = character.GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    animator.Play(animName, 0, 0f);
                    played++;
                }
            }
            console.Log($"Played '{animName}' on {played}/{total} characters.");
        }
    }

    private class PrintUserNameCommand : ICommand
    {
        public string Name => "printusername";
        public string[] Aliases => new[] { "name" };
        public string Description => "printusername - Logs 'My Nam Jeff'.";
        public void Execute(string[] args, CommandConsole console) => console.Log("My Nam Jeff");
    }

    #endregion
}
