
using UnityEngine;

[CreateAssetMenu(menuName = "Spawner/CharacterSpawnConfig")]
public class CharacterSpawnConfig : ScriptableObject
{
    [Header("Prefab & Models")]
    [Tooltip("Prefab del Character a instanciar")]
    public Character characterPrefab;

    [Tooltip("Datos de CharacterModel para ISetup<CharacterModel>")]
    public CharacterModel characterModel;

    [Tooltip("Datos de PlayerControllerModel para ISetup<IPlayerControllerModel>")]
    public PlayerControllerModel controllerModel;

    [Tooltip("Animator Controller a asignar")]
    public RuntimeAnimatorController animatorController;
}