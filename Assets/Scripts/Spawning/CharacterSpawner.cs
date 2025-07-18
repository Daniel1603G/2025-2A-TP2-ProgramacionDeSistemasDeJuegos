using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private float spawnSpaceX = 3f;
    private ICharacterFactory _factory;
    private int spawnCount;

    private void Awake()
    {
        _factory = new CharacterFactory();
        spawnCount = 0;
    }

    public void Spawn(CharacterSpawnConfig config)
    {
       
        
        Vector3 offset   = new Vector3(spawnCount * spawnSpaceX, 0f, 0f);
        Vector3 spawnPos = transform.position + offset;
        spawnCount++;
        
        _factory.CreateCharacter(config, spawnPos, transform.rotation);
    }
}