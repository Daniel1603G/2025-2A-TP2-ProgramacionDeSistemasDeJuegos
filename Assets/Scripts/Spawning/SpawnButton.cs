using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnButton : MonoBehaviour, ISetup<ButtonSpawnConfig>
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text label;

    private ButtonSpawnConfig _config;

    public void Setup(ButtonSpawnConfig config)
    {
        _config = config;
        label.text = config.buttonTitle;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        var spawner = FindObjectOfType<CharacterSpawner>();
        spawner.Spawn(_config.spawnConfig);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }
}