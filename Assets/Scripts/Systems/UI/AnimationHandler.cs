using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public static AnimationHandler instance { get; private set; }

    private Dictionary<string, IAnimationObserver> allAnimators;

    [SerializeField] private half durationToPlay = (half)0.5;

    private void Awake()
    {
        instance = this;
        allAnimators = new Dictionary<string, IAnimationObserver>();
    }

    public void AddAnimator(IAnimationObserver animator)
    {
        string animatorName = animator.GetName();
        if (!allAnimators.ContainsKey(animatorName))
        {
            allAnimators[animatorName] = animator;
        }
    }

    public void RemoveAnimator(IAnimationObserver animator)
    {
        string animatorName = animator.GetName();
        allAnimators.Remove(animatorName);
    }

    public void PlayAnimation(string animatorName, string animationName, Action onComplete = null)
    {
        var animator = allAnimators.FirstOrDefault(a => a.Key == animatorName).Value;

        if (animator != null)
        {
            animator.PlayAnimation(animationName, onComplete);
        }
    }

    public async void PlayAnimation(string animationName)
    {
        await Task.Delay(TimeSpan.FromSeconds(durationToPlay));

        foreach (var animator in allAnimators)
        {
            if (animator.Value.HasAnimation(animationName))
            {
                animator.Value.PlayAnimation(animationName);
            }
        }
    }


    public void PlayAnimationOnAll(string animationName, Action onComplete = null)
    {
        foreach (var animator in allAnimators.Values)
        {
            animator.PlayAnimation(animationName, onComplete);
        }
    }

    public void SetAnimatorActivation(string animatorName, bool active)
    {
        GetAnimatorByName(animatorName).SetActive(active);
    }

    public void ClearAllAnimators()
    {
        allAnimators.Clear();
    }

    public IAnimationObserver GetAnimatorByName(string animatorName)
    {
        allAnimators.TryGetValue(animatorName, out var animator);
        return animator;
    }
}