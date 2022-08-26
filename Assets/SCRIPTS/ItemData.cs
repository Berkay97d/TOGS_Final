using IdleCashSystem.Core;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
    public Item prefab;
    public Sprite icon;
    public IdleCash value;
    public float growthDuration;
    public float collectableDelay;
}
