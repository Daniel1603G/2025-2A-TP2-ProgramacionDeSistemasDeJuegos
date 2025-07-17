using UnityEngine;

public interface ICharacterFactory
{
    GameObject CreateCharacter(CharacterSpawnConfig config, Vector3 position, Quaternion rotation);
}