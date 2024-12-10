using System;
using System.Collections.Generic;
using UnityEngine;
using static SwipeDetector;

public class SwipeDetector : MonoBehaviour
{
    public static SwipeDetector Instance { get; private set; }

    private HashSet<ISwipeDirectionListernen> swipeDirectionListernens = new HashSet<ISwipeDirectionListernen>();

    private bool isSwiping = false;
    private Vector2 startMousePosition;

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right,
        Horizontal
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckDirection();
    }

    private void CheckDirection()
    {
        if (HorizotalSwipe())
        {
            foreach (var item in swipeDirectionListernens)
            {
                item.Swipe(SwipeDirection.Horizontal);
            }
        }
    }

    private bool HorizotalSwipe()
    {
        float swipeThreshold = 300f; // Adjust the threshold as needed

        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            startMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
            Vector2 endMousePosition = Input.mousePosition;
            float swipeDistance = endMousePosition.x - startMousePosition.x;

            if (Mathf.Abs(swipeDistance) > swipeThreshold)
            {
                return true;
            }
        }

        return false;
    }


    public void AddListernen(ISwipeDirectionListernen swipeDirection)
    {
        if (!swipeDirectionListernens.Contains(swipeDirection))
        {
            swipeDirectionListernens.Add(swipeDirection);
        }
    }

    public void RemoveListernen(ISwipeDirectionListernen swipeDirection)
    {
        if (swipeDirectionListernens.Contains(swipeDirection))
        {
            swipeDirectionListernens.Remove(swipeDirection);
        }
    }
}

public interface ISwipeDirectionListernen
{
    void Swipe(SwipeDirection swipeDirection);

}