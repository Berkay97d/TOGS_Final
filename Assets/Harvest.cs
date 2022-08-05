using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Harvestable"))
        {
            Destroy(other.gameObject);
        }
    }
}
