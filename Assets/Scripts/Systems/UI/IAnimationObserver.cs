using System;

public interface IAnimationObserver
{
    void PlayAnimation(string animationName, Action onComplete = null);
    string GetName();
    void SetActive(bool active);
    bool HasAnimation(string animationName);
}