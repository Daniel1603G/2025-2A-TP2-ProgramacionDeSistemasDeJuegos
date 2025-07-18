using UnityEngine;

public interface IConsoleCommand 
{
    
        string Name { get; }
        string[] Aliases { get; }
        string Description { get; }
        void Execute(string[] args);
    
}
