using Systems;
using Systems.GridSystem_;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2Int Coord { get; set; }

    #region Movement Setup

    [Header("Movement Setup")]
    [SerializeField] private TileLayers movementLayers;
    [Min(1)]
    public int movementRadius = 1;
    /// <summary>
    /// —к≥льки раз≥в може рухатись юн≥т
    /// </summary>
    public int countMovementSteps = 1;

    public TileLayers MovementLayers { get { return movementLayers; } }

    #endregion

    #region Attack Setup

    [Header("Attack Setup")]
    public int unitHP;
    public int attackDamage;
    [Min(1)]
    public int attackRadius = 1;
    /// <summary>
    /// —к≥льки раз≥в може атакувати юн≥т
    /// </summary>
    public int countAttackSteps = 1;

    #endregion

    #region Animation Setup
    
    //[Header("Animation Setup")]     

    #endregion

    #region Initialization

    private void Start()
    {
        InitUnit(transform.position);
    }

    private void InitUnit(Vector2 worldPosition)
    {
        Coord = ConvertCoords.ConvertToTileCoord(worldPosition, true);
        GridSystem.AddElement(GridId.UnitGrid, Coord, this);
    }

    #endregion

    public bool HasMovementSteps() => countMovementSteps > 0;
    public bool HasAttackSteps() => countAttackSteps > 0;
}