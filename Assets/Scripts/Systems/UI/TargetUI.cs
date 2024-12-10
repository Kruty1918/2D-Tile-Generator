using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour, ITargetUI
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private Image targetImage;
    [SerializeField] private Button button;
    [SerializeField] private Color targetColor = Color.white;
    [SerializeField] private SettingsType settingsType;
    [SerializeField] private int _idSettings;
    public SettingsType settings => settingsType;

    public int idSettings => _idSettings;

    private void Awake()
    {
        if (!targetImage)
        {
            targetImage = GetComponent<Image>();
        }

        if (!button)
        {
            button = GetComponent<Button>();
        }

        button.onClick.AddListener(() => SetTarget(true));
    }

    private void Start()
    {
        CreateWorldManager.Instance.AddTarget(this);
    }

    public void SetTarget(bool active)
    {
        foreach (GameObject target in targets)
        {
            target.SetActive(active);
        }

        bool change = targetImage != null && active;

        targetImage.color = change ? targetColor : Color.white;

        CreateWorldManager.Instance.ChangeSettings(active, this);
    }
}

public interface ITargetUI
{
    SettingsType settings { get; }
    int idSettings { get; }
    void SetTarget(bool active);
}

public enum SettingsType
{
    GameMode, WorldSize, GameDifficulty
}