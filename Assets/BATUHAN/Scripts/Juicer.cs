using System;
using System.Collections;
using System.Collections.Generic;
using EMRE.Scripts;
using UnityEngine;

public class Juicer : MonoBehaviour
{
    [SerializeField] private Transform juiceOutPoint;

    private static readonly WaitForSecondsRealtime JuiceInterval = new WaitForSecondsRealtime(0.1f);
    
    
    public void JuiceFruit(Fruit fruit)
    {
        var juice = fruit.TurnToJuice();

        juice.transform.position = juiceOutPoint.position;

        var force = juiceOutPoint.forward * 10f;
        juice.Throw(force);
        
        fruit.Destroy();
    }
    /*private void DequeueFruit()
    {
        if (!m_Fruits.TryDequeue(out var fruit)) return;
        
        if (!fruit) return;
        
        var juice = fruit.TurnToJuice();

        juice.transform.position = juiceOutPoint.position;

        var force = juiceOutPoint.forward * 10f;
        juice.Throw(force);
        
        fruit.Destroy();
    }*/
}
