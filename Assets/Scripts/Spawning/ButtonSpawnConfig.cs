
using UnityEngine;

[CreateAssetMenu(menuName = "Spawner/ButtonSpawnConfig")]
public class ButtonSpawnConfig : ScriptableObject
{
    [Tooltip("Texto del botón")] public string buttonTitle;

    [Tooltip("Configuración de spawn que se pasará al CharacterSpawner")]
    public CharacterSpawnConfig spawnConfig;
}
