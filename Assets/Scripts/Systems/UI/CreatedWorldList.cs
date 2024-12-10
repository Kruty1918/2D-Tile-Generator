//using Gameframe.SaveLoad;
using System.Collections.Generic;
using UnityEngine;


public class CreatedWorldList : MonoBehaviour
{
    [SerializeField] private MapItem itemPrefab;
    [SerializeField] private RectTransform rectTransform;

    private HashSet<MapItem> mapItems = new HashSet<MapItem>();

    private void OnEnable()
    {
        InitItems();
    }

    private void OnDisable()
    {
        RemoveAllItem();
    }

    public void InitItems()
    {
        //SaveLoadManagerAddon managerAddon = new SaveLoadManagerAddon();

        //string[] keys = managerAddon.GetSavedFiles;

        //for (int i = 0; i < keys.Length; i++)
        //{
        //    WorldData worldData = managerAddon.Manager.Load<WorldData>(keys[i]);
        //    if (worldData != null)
        //    {
        //        CreateNewItem(worldData);
        //    }
        //}
    }

    private void CreateNewItem(WorldData worldData)
    {
        MapItem item = Instantiate(itemPrefab, rectTransform);

        item.Initialize(worldData);

        mapItems.Add(item);
    }

    private void RemoveAllItem()
    {
        foreach (var item in mapItems)
        {
            Destroy(item.gameObject);
        }

        mapItems.Clear();
    }
}
