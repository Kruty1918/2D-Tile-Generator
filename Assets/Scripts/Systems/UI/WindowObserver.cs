using Mediators;
using System.Collections.Generic;
using UnityEngine;

public class WindowObserver : MonoBehaviour, IWindiwListener
{
    [SerializeField] private List<GameObject> buttons;

    private void Start()
    {
        OnChangeCloseType(GameSettings.IsCloseSwipe);
        SM.Instance<UIManager>().AddWindow(this);
    }

    private void OnEnable()
    {
        SM.Instance<UIManager>().AddWindow(this);
    }

    private void OnDisable()
    {
        SM.Instance<UIManager>().RemoveWindow(this);
    }

    private void OnDestroy()
    {
        SM.Instance<UIManager>().RemoveWindow(this);
    }

    public void OnChangeCloseType(bool active)
    {
        foreach (var button in buttons)
        {
            button.SetActive(!active);
        }   
    }
}

public interface IWindiwListener
{
    void OnChangeCloseType(bool active);
}