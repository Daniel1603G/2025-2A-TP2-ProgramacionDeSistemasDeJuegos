using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "Spawner/SpawnMenuConfig")]
public class SpawnMenuConfig : ScriptableObject
{
    [Tooltip("Listado de botones a generar dinámicamente")]
    public List<ButtonSpawnConfig> buttonConfigs;
}
