using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
//using EasyTransition;
using Mediators.Singleton;
using Mediators;

public class UIManager : MonoSingleton<UIManager>
{
    public half waitBeforeLoadScene = (half)1;

    //public TransitionSettings transitionSettings;

    private HashSet<IWindiwListener> windiwListeners = new HashSet<IWindiwListener>();
    public HashSet<IWindiwListener> WindiwListeners { get { return windiwListeners; } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void ChangeCloseWindowType(bool active)
    {
        GameSettings.SetCloseSwipe(active);
        ChangeCloseSwipeState(active);
    }

    public void AddWindow(IWindiwListener listener)
    {
        if (!windiwListeners.Contains(listener))
        {
            windiwListeners.Add(listener);
        }
    }

    public void RemoveWindow(IWindiwListener listener)
    {
        if (windiwListeners.Contains(listener))
        {
            windiwListeners.Remove(listener);
        }
    }

    public void ChangeCloseSwipeState(bool active)
    {
        foreach (var listener in windiwListeners)
        {
            listener.OnChangeCloseType(active);
        }
    }

    public async void LoadScene(string sceneName)
    {
        //SM.Instance<TransitionManager>().Transition(transitionSettings);
        //await Task.Delay(TimeSpan.FromSeconds(waitBeforeLoadScene));       
        //SceneLoader.LoadScene(sceneName);
    }

    public void CreateNewWorld(WorldData worldData)
    {
        //string saveFileName = WorldTools.GetKey(worldData);
        //new SaveLoadManagerAddon().Save(worldData, saveFileName);

        //LoadWorld(worldData);
    }


    public void LoadWorld(WorldData worldData)
    {
        GameSettings.World = worldData;        
        LoadScene("Game");
    }
}
