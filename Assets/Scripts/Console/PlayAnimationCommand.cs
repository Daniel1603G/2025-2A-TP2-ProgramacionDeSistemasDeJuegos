
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


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

            var animName = args[0];
            if (!_config.animationNames.Contains(animName))
            {
                console.AppendLog($"Animation '{animName}' not found. Valid names: {string.Join(", ", _config.animationNames)}");
                return;
            }

            var characters = UnityEngine.Object.FindObjectsOfType<Character>();
            int played = 0, total = characters.Length;
            foreach (var character in characters)
            {
                var autoAnim = character.GetComponent<CharacterAnimator>();
                if (autoAnim)
                    autoAnim.enabled = false;

                var animator = character.GetComponent<Animator>();
                if (animator)
                {
                    animator.Play(animName, 0, 0f);
                    played++;
                }
            }

            console.AppendLog($"Played '{animName}' on {played}/{total} characters.");
        }
    }

    