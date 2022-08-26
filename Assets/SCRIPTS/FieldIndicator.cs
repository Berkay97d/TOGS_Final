using UnityEngine;

public class FieldIndicator : MonoBehaviour
{
    private const float Edge = 2.56f;
    
    
    [SerializeField] private SpriteRenderer
        outline,
        fill;


    [SerializeField] private Color
        outlineColor,
        fillColor;

    [SerializeField] private Vector2 size;


    private void OnValidate()
    {
        UpdateSprites();
    }


    private void UpdateSprites()
    {
        outline.color = outlineColor;
        fill.color = fillColor;

        outline.size = size * Edge;
        fill.size = size * Edge;
    }
}
