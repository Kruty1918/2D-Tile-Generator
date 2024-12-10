//using EasyTransition;
using Mediators;
using UnityEngine;

public class GamePausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [Range(0f, 1f)]
    [SerializeField] private float pauseTimeScale = 0f;

    public void OnPause()
    {
        SetPauseState(true);
    }

    public void OnBack()
    {
        SetPauseState(false);
    }

    public void OnSettings()
    {

    }

    public void OnHome()
    {
        SM.Instance<UIManager>().LoadScene("Home");
        Time.timeScale = 1;
    }

    private void SetPauseState(bool state)
    {
        Time.timeScale = state ? pauseTimeScale : 1;
        pausePanel.SetActive(state);
    }
}