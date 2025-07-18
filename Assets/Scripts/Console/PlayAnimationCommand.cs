
using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class PlayAnimationCommand : IConsoleCommand
{
    private readonly AnimationNamesConfig _config;

    public PlayAnimationCommand(AnimationNamesConfig config)
    {
        _config = config;
    }

    public string Name => "playanimation";
    public string[] Aliases => new[] { "pa" };
    public string Description => "playanimation [animationName] - Play animation on all characters.";

    public void Execute(string[] args)
    {
        var console = CommandConsole.Instance;
        if (args.Length == 0)
        {
            console.AppendLog("Usage: playanimation [animationName]");
            return;
        }

        string animName = args[0];
        if (!_config.animationNames.Contains(animName))
        {
            var validList = string.Join(
                ", ",
                (IEnumerable<string>)_config.animationNames
            );
            console.AppendLog($"Animation '{animName}' not found. Valid names: {validList}");
            return;
        }

        var characters = Object.FindObjectsOfType<Character>();
        int played = 0, total = characters.Length;

        foreach (var character in characters)
        {
          
            var autoAnim = character.GetComponent<CharacterAnimator>();
            if (autoAnim) autoAnim.enabled = false;

           
            var animator = character.GetComponentInChildren<Animator>();
            if (animator == null) continue;

          
            var clips = animator.runtimeAnimatorController
                .animationClips
                .Select(c => c.name)
                .ToList();  


            console.AppendLog(
                $"[{character.name}] Clips: {string.Join(", ", clips)}"
            );
            animator.CrossFade(animName, 0f);
         
            played++;
        }

        console.AppendLog($"Played '{animName}' on {played}/{total} characters.");
    }

}