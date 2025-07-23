using Unity.VisualScripting;
using UnityEngine;

public class CharacterFactory : ICharacterFactory
{
    public GameObject CreateCharacter(CharacterSpawnConfig config, Vector3 position, Quaternion rotation)
    {
        var result = Object.Instantiate(config.prefab.gameObject, position, rotation);

      
        var characterSetup = result.GetComponent<ISetup<CharacterModel>>() ?? result.AddComponent<Character>();
        characterSetup.Setup(config.characterModel);
        
        
        var controllerSetup = result.GetComponent<ISetup<IPlayerControllerModel>>() ?? result.AddComponent<PlayerController>();
        controllerSetup.Setup(config.controllerModel);

  
        var animator = result.GetComponentInChildren<Animator>() ?? result.AddComponent<Animator>();
        animator.runtimeAnimatorController = config.animatorController;

        return result;
    }
}
