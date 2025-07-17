using Unity.VisualScripting;
using UnityEngine;

public class CharacterFactory : ICharacterFactory
{
    public GameObject CreateCharacter(CharacterSpawnConfig config, Vector3 position, Quaternion rotation)
    {

        var result= Object.Instantiate(config.prefab.gameObject, position, rotation);

        var character = result.GetComponent<Character>() ?? result.AddComponent<Character>();
        character.Setup(config.characterModel);

        var controller = result.GetComponent<PlayerController>() ?? result.AddComponent<PlayerController>();
        controller.Setup(config.controllerModel);

        var animator = result.GetComponentInChildren<Animator>() ?? result.AddComponent<Animator>();
        animator.runtimeAnimatorController = config.animatorController;

        return result;
    }
}
