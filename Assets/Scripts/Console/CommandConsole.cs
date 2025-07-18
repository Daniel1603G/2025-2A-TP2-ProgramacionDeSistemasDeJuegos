using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;

public class CommandConsole : MonoBehaviour, ICommandRegistry
{
    public static CommandConsole Instance { get; private set; }

    [Header("Input System")]

    [Header("UI References")]
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private Button toggleButton;

    [Header("Animation Config")]
    [SerializeField] private AnimationNamesConfig animationNamesConfig;

    private readonly Dictionary<string, IConsoleCommand> commands = new(StringComparer.OrdinalIgnoreCase);
    
    public IEnumerable<IConsoleCommand> Commands
        => commands
            .Values
            .GroupBy(c => c.Name)
            .Select(g => g.First());

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

      
        RegisterCommand(new HelpCommand(this));
        RegisterCommand(new AliasesCommand());
        RegisterCommand(new PlayAnimationCommand(animationNamesConfig));

    
        consolePanel.SetActive(false);
        toggleButton.onClick.AddListener(ToggleConsole);
        inputField.onSubmit.AddListener(OnInputSubmit);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            ToggleConsole();
    }
   

    private void OnToggleConsole(InputAction.CallbackContext ctx)
    {
        ToggleConsole();
    }

    private void ToggleConsole()
    {
        consolePanel.SetActive(!consolePanel.activeSelf);
        if (consolePanel.activeSelf)
            inputField.ActivateInputField();
    }

    private void OnInputSubmit(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return;

        AppendLog($"> {input}");
        ParseAndExecute(input);
        inputField.text = string.Empty;
        inputField.ActivateInputField();
    }

    private void RegisterCommand(IConsoleCommand command)
    {
        commands[command.Name] = command;
        foreach (var alias in command.Aliases)
            commands[alias] = command;
    }

    public bool TryGetCommandInfo(string name, out IConsoleCommand command)
    {
        return commands.TryGetValue(name, out command);
    }
    
    

    private void ParseAndExecute(string input)
    {
        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return;

        var key = parts[0];
        var args = parts.Skip(1).ToArray();
        if (commands.TryGetValue(key, out var cmd))
        {
            try { cmd.Execute(args); }
            catch (Exception ex) { AppendLog($"<color=red>Error:</color> {ex.Message}"); }
        }
        else
        {
            AppendLog($"<color=yellow>Unknown command:</color> {key}");
        }
    }

    public void AppendLog(string message)
    {
        outputText.text += message + "\n";
    }
}

