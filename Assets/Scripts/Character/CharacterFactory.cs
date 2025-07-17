using Unity.VisualScripting;
using UnityEngine;

public class CharacterFactory : ICharacterFactory
{
    public GameObject CreateCharacter(CharacterSpawnConfig config, Vector3 position, Quaternion rotation)
    {

        var go = Object.Instantiate(config.prefab.gameObject, position, rotation);

        var character = go.GetComponent<Character>() ?? go.AddComponent<Character>();
        character.Setup(config.characterModel);

        var controller = go.GetComponent<PlayerController>() ?? go.AddComponent<PlayerController>();
        controller.Setup(config.controllerModel);

        var animator = go.GetComponentInChildren<Animator>() ?? go.AddComponent<Animator>();
        animator.runtimeAnimatorController = config.animatorController;

        return go;
    }
}
