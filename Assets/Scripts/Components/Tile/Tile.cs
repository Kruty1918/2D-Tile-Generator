using Systems;
using Systems.GridSystem_;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileLayers layer;

    public TileLayers Layer { get { return layer; } }
    public Vector2Int Coord { get; private set; }
    public bool CanMove { get; set; }


    #region Initialization

    private void Start()
    {
        InitTile(transform.position);
    }


    private void InitTile(Vector2 worldPosition)
    {
        Coord = ConvertCoords.ConvertToTileCoord(worldPosition, true);

        GridSystem.AddElement(GridId.TileGrid, Coord, this);
    }

    #endregion

    public bool CompareLayers(TileLayers layerToCompare)
    {
        return (layerToCompare & layer) == layer;
    }
}