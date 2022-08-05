using System.Collections.Generic;
using UnityEngine;

public class FarmLand : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FarmTile farmTilePrefab;
    [SerializeField] private Transform farmTileParent;
    [SerializeField] private BoxCollider boxCollider;
    
    [Header("Values")]
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private float size;


    private List<FarmTile> m_FarmTiles;
    private Grid m_Grid;
    
    
    private void Start()
    {
        Build();
    }

    private void Build()
    {
        m_Grid = new Grid(farmTileParent.position, x, y, size);

        foreach (var position in m_Grid)
        {
            var newFarmTile = Instantiate(farmTilePrefab, farmTileParent);
            newFarmTile.Position = position;
            newFarmTile.SetSize(size);
        }

        var boxColliderSize = boxCollider.size;
        boxColliderSize.x = size * x;
        boxColliderSize.z = size * y;
        boxCollider.size = boxColliderSize;

        var boxColliderCenter = boxCollider.center;
        boxColliderCenter.x = size * 0.5f * (x - 1);
        boxColliderCenter.z = size * 0.5f * (y - 1);
        boxCollider.center = boxColliderCenter;
    }
}
