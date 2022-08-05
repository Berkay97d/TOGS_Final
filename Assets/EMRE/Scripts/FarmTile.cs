using UnityEngine;

public class FarmTile : MonoBehaviour
{
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
    
    
    public void SetSize(float size)
    {
        var oldSize = Size;
        oldSize.x = size;
        oldSize.z = size;
        Size = oldSize;
    }
}
