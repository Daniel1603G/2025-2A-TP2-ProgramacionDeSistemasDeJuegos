// MenuSpawner.cs
using UnityEngine;

public class MenuSpawner : MonoBehaviour, ISetupSpawner<SpawnMenuConfig>
{
    [SerializeField] private SpawnButton buttonPrefab;

    public void Setup(SpawnMenuConfig config)
    {
        foreach (var btnCfg in config.buttonConfigs)
        {
            var btnInstance = Instantiate(buttonPrefab, transform);
            btnInstance.Setup(btnCfg);
        }
    }
}