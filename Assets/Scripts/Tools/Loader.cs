using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
//using EasyTransition;
using Mediators;

public class Loader : MonoBehaviour
{
    public static Loader Instance { get; private set; }

    [SerializeField] private Slider slider;
    [SerializeField] private float delayBeforeLoadingScene = 1;
    //[SerializeField] private TransitionSettings transitionSettings;

    private AsyncOperation loadingOperation;
    private float delayedProgress;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync(SceneLoader.NextSceneName);
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        if (loadingOperation != null)
        {
            slider.value = Mathf.Clamp01(delayedProgress);
        }
    }

    private IEnumerator LoadScene()
    {
        loadingOperation.allowSceneActivation = false;

        // Initial delay
        yield return new WaitForSeconds(delayBeforeLoadingScene);

        float timer = 0f;
        float transitionDuration = 1.5f;

        while (timer < transitionDuration)
        {
            delayedProgress = Mathf.Lerp(0f, 1f, timer / transitionDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        //SM.Instance<TransitionManager>().Transition(transitionSettings);

        // Delay after the transition
        yield return new WaitForSeconds(1.5f);

        loadingOperation.allowSceneActivation = true;
    }
}
