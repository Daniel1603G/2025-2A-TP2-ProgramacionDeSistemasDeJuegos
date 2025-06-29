using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ISetupSpawner<CharacterSpawnConfig>
{
    [SerializeField] private Character prefab;
    [SerializeField] private CharacterModel characterModel;
    [SerializeField] private PlayerControllerModel controllerModel;
    [SerializeField] private RuntimeAnimatorController animatorController;
    
    public void Setup(CharacterSpawnConfig config)
    {
        // Solo decide posición y rotación
        var instance = Instantiate(
            config.characterPrefab,
            transform.position,
            transform.rotation
        );

        // Delegamos la configuración concreta al ISetup<CharacterModel>
        if (!instance.TryGetComponent<ISetup<CharacterModel>>(out var charSetup))
            charSetup = instance.gameObject.AddComponent<Character>();
        charSetup.Setup(config.characterModel);

        // Delegamos la configuración del controller
        if (!instance.TryGetComponent<ISetup<IPlayerControllerModel>>(out var ctrlSetup))
            ctrlSetup = instance.gameObject.AddComponent<PlayerController>();
        ctrlSetup.Setup(config.controllerModel);

        // Asignamos el AnimatorController sin lógica extra
        var animator = instance.GetComponentInChildren<Animator>()
                       ?? instance.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = config.animatorController;
    }
}
