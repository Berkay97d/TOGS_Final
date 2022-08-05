using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tag] 
    public string harvestableAreaTag, plantableAreaTag;
    
    [Tag] 
    public string harvestableTag, plantableTag;
}
