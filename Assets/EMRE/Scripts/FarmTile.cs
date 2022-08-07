using EMRE.Scripts;
using UnityEngine;


public enum FarmTileState
{
    Empty,
    Growing,
    Grown
}


public class FarmTile : MonoBehaviour
{
    private FarmLand m_FarmLand;
    private Harvestable m_Harvestable;


    public Harvestable Harvestable => m_Harvestable;


    public FarmTileState State
    {
        get
        {
            if (!m_Harvestable) return FarmTileState.Empty;

            return m_Harvestable.IsGrown
                ? FarmTileState.Grown
                : FarmTileState.Growing;
        }
    }
    
    
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    
    public Vector3 Size
    {
        get => transform.localScale;
        private set => transform.localScale = value;
    }


    public void Inject(FarmLand farmLand)
    {
        m_FarmLand = farmLand;
    }
    
    public void SetSize(float size)
    {
        var oldSize = Size;
        oldSize.x = size;
        oldSize.z = size;
        Size = oldSize;
    }

    public bool TryPlantSeed()
    {
        if (m_Harvestable) return false;

        if (m_FarmLand.State != FarmLandState.Seeding) return false;
        
        m_Harvestable = Instantiate(m_FarmLand.CurrentHarvestable, transform);
        m_Harvestable.Position = transform.position;
        
        m_FarmLand.OnSeedPlanted();

        return true;
    }
}
