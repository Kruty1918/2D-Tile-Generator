using UnityEngine;
using UnityEngine.Events;

public class SwipeDirectionListernen : MonoBehaviour, ISwipeDirectionListernen
{
    [SerializeField] private UnityEvent OnSwipeDown;
    [SerializeField] private UnityEvent OnSwipeLeft;
    [SerializeField] private UnityEvent OnSwipeRight;
    [SerializeField] private UnityEvent OnSwipeUp;

    [SerializeField] private UnityEvent OnSwipeHorizontal;


    private bool isInvokeEvent = true;
    public bool IsInvokeEvent { set {  isInvokeEvent = value; } }

    private void Start()
    {
        SwipeDetector.Instance.AddListernen(this);
    }

    private void OnEnable()
    {
        SwipeDetector.Instance.AddListernen(this);
    }

    private void OnDisable()
    {
        SwipeDetector.Instance.RemoveListernen(this);
    }

    private void OnDestroy()
    {
        SwipeDetector.Instance.RemoveListernen(this);
    }

    public void Swipe(SwipeDetector.SwipeDirection swipeDirection)
    {
        switch (swipeDirection)
        {
            case SwipeDetector.SwipeDirection.Up:
                SwipeUp();
                break;
            case SwipeDetector.SwipeDirection.Down:
                SwipeDown();
                break;
            case SwipeDetector.SwipeDirection.Left:
                SwipeLeft();
                break;
            case SwipeDetector.SwipeDirection.Right:
                SwipeRight();
                break;
            case SwipeDetector.SwipeDirection.Horizontal:
                SwipeHorizontal();
                break;
            default:
                break;
        }
    }

    private void SwipeDown()
    {
        InvokeEvent(OnSwipeDown);
    }

    private void SwipeLeft()
    {
        InvokeEvent(OnSwipeLeft);
    }

    private void SwipeRight()
    {
        InvokeEvent(OnSwipeRight);
    }

    private void SwipeUp()
    {
        InvokeEvent(OnSwipeUp);
    }

    private void SwipeHorizontal()
    {
        InvokeEvent(OnSwipeHorizontal);
    }

    private void InvokeEvent(UnityEvent @event)
    {
        if (!isInvokeEvent || !GameSettings.IsCloseSwipe) return;

        @event.Invoke();
    }
}
