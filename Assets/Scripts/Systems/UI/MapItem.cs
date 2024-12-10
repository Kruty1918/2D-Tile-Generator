//using EasyTransition;
using Mediators;
using TMPro;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI worldNameText;
    [SerializeField] private TextMeshProUGUI gameModeText;
    [SerializeField] private TextMeshProUGUI worldSizeText;
    [SerializeField] private TextMeshProUGUI gameDifficultyText;
    [SerializeField] private TextMeshProUGUI dataCreatedText;

    private WorldData data;

    public MapItem(WorldData worldData)
    {        
        Initialize(worldData);
    }

    public void Initialize(WorldData worldData)
    {
        worldNameText.text = worldData.worldName;
        gameModeText.text = worldData.gameMode.ToString();
        worldSizeText.text = worldData.worldSize.ToString();
        gameDifficultyText.text = worldData.difficulty.ToString();
        dataCreatedText.text = worldData.createdTime;

        data = worldData;
    }

    public void LoadWorld()
    {
        SM.Instance<UIManager>().LoadWorld(data);
    }
}