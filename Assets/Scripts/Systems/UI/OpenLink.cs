using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [SerializeField] private string url = "your url adres";

    public void OpenUrl()
    {
        Application.OpenURL(url);
    }
}