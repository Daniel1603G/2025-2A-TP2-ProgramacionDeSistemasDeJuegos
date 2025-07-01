using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ISetupSpawner<CharacterSpawnConfig>
{
    [SerializeField] private float spawnSpacingX = 10f;
    private int spawnIndex;
    private void OnEnable()
    {
        spawnIndex = 0;
    }
public void Setup(CharacterSpawnConfig config)
{
    
    Vector3 spawnPos = transform.position + new Vector3(spawnIndex * spawnSpacingX, 0f, 0f);

    spawnIndex++;
   
    var instance = Instantiate(
        config.characterPrefab,
        transform.position,
        transform.rotation
    );
    
 

   
    if (!instance.TryGetComponent<ISetup<CharacterModel>>(out var charSetup))
        charSetup = instance.gameObject.AddComponent<Character>();
    charSetup.Setup(config.characterModel);

    
    if (!instance.TryGetComponent<ISetup<IPlayerControllerModel>>(out var ctrlSetup))
        ctrlSetup = instance.gameObject.AddComponent<PlayerController>();
    ctrlSetup.Setup(config.controllerModel);

  
    var animator = instance.GetComponentInChildren<Animator>()
                   ?? instance.gameObject.AddComponent<Animator>();
    animator.runtimeAnimatorController = config.animatorController;
}
}
