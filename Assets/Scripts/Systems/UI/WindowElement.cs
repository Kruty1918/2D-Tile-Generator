using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowElement : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Animator _animator;

    public string Name { get { return _name; } }
    public Animator Animator { get { return _animator; } }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void PlayAnimaiton(string animationName)
    {
        _animator.Play(animationName);
    }
}
