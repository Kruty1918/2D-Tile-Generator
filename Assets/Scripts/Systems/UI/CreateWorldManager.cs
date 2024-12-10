//using EasyTransition;
using Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CreateWorldManager : MonoBehaviour
{
    public static CreateWorldManager Instance { get; private set; }

    [SerializeField] private TMP_InputField worldName;
    [SerializeField] private TMP_InputField worldSeed;

    [SerializeField] private int maxSeedLeght = 8;

    private HashSet<ITargetUI> targetUIs = new HashSet<ITargetUI>();

    private GameMode gameMode;
    private WorldSize worldSize;
    private GameDifficulty difficulty;

    private void Awake()
    {
        Instance = this;
        Init();
    }


    private void Start()
    {
        RandomizationSeed();
        RandomWorldSettings();            
    }

    private void Init()
    {
        worldName.text = "New World";
    }

    public void CreateWorld()
    {
        string createdTime = "Created: " + DateTime.Now.ToString();
        WorldData worldData = new WorldData(worldName.text, int.Parse(worldSeed.text), gameMode, worldSize, difficulty, createdTime);

        //if (HasSameWorld(worldData))
        //{
        //    SM.Instance<UIManager>().CreateNewWorld(worldData);
        //}
    }

    //private bool HasSameWorld(WorldData worldData)
    //{
    //    return WorldTools.HasSameWorld(worldData) && ValidName();
    //}

    private bool ValidName()
    {
        return worldName.text != string.Empty && !string.IsNullOrEmpty(worldSeed.text);
    }

    public void ChangeGameMode(int id)
    {
        gameMode = (GameMode)id;
    }

    public void ChangeWorldSize(int id)
    {
        worldSize = (WorldSize)id;
    }

    public void ChangeDifficulty(int id)
    {
        difficulty = (GameDifficulty)id;
    }

    public void RandomizationSeed()
    {     
        worldSeed.text = UnityEngine.Random.Range(0, int.MaxValue).ToString();
    }

    public void AddTarget(ITargetUI target)
    {
        targetUIs.Add(target);
    }

    public void RemoveTarget(ITargetUI target)
    {
        targetUIs.Remove(target);
    }

    public void SetAciteAll(bool active)
    {
        foreach (var target in targetUIs)
        {
            target.SetTarget(active);
        }
    }

    public void ChangeSettings(bool active, ITargetUI targetUI)
    {


        foreach (var target in targetUIs)
        {
            if (targetUI.settings == target.settings && targetUI != target && active)
            {
                target.SetTarget(false);
            }
        }
    }

    public void RandomWorldSettings()
    {
        ITargetUI gameMode = GetRandomUI(SettingsType.GameMode);
        ITargetUI worldSize = GetRandomUI(SettingsType.WorldSize);
        ITargetUI difficulty = GetRandomUI(SettingsType.GameDifficulty);

        this.gameMode = (GameMode)gameMode.idSettings;
        this.worldSize = (WorldSize)worldSize.idSettings;
        this.difficulty = (GameDifficulty)difficulty.idSettings;

        gameMode.SetTarget(true);
        worldSize.SetTarget(true);
        difficulty.SetTarget(true);
    }

    private ITargetUI GetRandomUI(SettingsType type)
    {
        var uiList = targetUIs.Where(ui => ui.settings == type).ToList();
        return uiList[UnityEngine.Random.Range(0, uiList.Count)];
    }
}