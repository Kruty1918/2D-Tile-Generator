using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationObserver : MonoBehaviour, IAnimationObserver
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject[] gameObjects;

    public string observerName;

    [SerializeField] private bool useActivation;

    [SerializeField] private List<AnimatorObserver> animatorObservers = new List<AnimatorObserver>();

    [SerializeField] private List<string> blackAnimationList = new List<string>();

    [SerializeField] private UnityEvent onActivation = new UnityEvent();

    private void Start()
    {
        AnimationHandler.instance.AddAnimator(this);
    }

    public void PlayAnimation(string animationName, Action onComplete = null)
    {
        if (animator != null)
        {
            if (useActivation)
            {
                foreach (var observer in animatorObservers)
                {
                    AnimationHandler.instance.SetAnimatorActivation(observer.animatorName, observer.activation);
                    onActivation.Invoke();
                }
            }

            SetActive(true);
            animator.Play(animationName);
        }
    }

    public bool HasAnimation(string animationName)
    {
        if (animator == null)
            return false;

        // Check for animation existence without UnityEditor features
        RuntimeAnimatorController runtimeController = animator.runtimeAnimatorController;

        if (runtimeController != null)
        {
            AnimationClip[] animationClips = runtimeController.animationClips;
            return Array.Exists(animationClips, clip => clip.name == animationName && !blackAnimationList.Contains(animationName));
        }

        return false;
    }


    public string GetName()
    {
        return observerName;
    }

    public void SetActive(bool active)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(active);
        }
    }
}
