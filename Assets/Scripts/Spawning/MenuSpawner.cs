
using UnityEngine;

public class MenuSpawner : MonoBehaviour, ISetupSpawner<SpawnMenuConfig>
{
   
    [SerializeField] private SpawnButton buttonPrefab;
    [SerializeField] private SpawnMenuConfig menuConfig;  

    private void Awake()
    {
       
        Setup(menuConfig);
    }

    public void Setup(SpawnMenuConfig config)
    {
        foreach (var button  in config.buttonConfigs)
        {
            
            var buttonIns = Instantiate(buttonPrefab, transform);
          
            buttonIns.Setup(button);
        }
    }
}