using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
   
    private ICharacterFactory _factory;

    private void Awake()
    {
        _factory = new CharacterFactory();
    }

    public void Spawn(CharacterSpawnConfig config)
    {
        _factory.CreateCharacter(config, transform.position, transform.rotation);
    }
}