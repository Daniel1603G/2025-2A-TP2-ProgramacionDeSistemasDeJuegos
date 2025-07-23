using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ISetupSpawner<CharacterSpawnConfig>
{
    private float spawnSpaceX = 3f;   
    private ICharacterFactory _factory;
    private int spawnCount;

    private void Awake()
    {
        _factory = new CharacterFactory();
        spawnCount = 0;
    }
    
    public void Setup(CharacterSpawnConfig config)
    {
        spawnSpaceX = config.spawnSpaceX;
    }
    
    public void Spawn(CharacterSpawnConfig characterConfig)
    {
        Vector3 offset = new Vector3(spawnCount * spawnSpaceX, 0f, 0f);
        Vector3 spawnPos = transform.position + offset;
        spawnCount++;
        
        _factory.CreateCharacter(characterConfig, spawnPos, transform.rotation);
    }
}