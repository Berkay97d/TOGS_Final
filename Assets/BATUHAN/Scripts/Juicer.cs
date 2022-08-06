using System.Collections;
using System.Collections.Generic;
using EMRE.Scripts;
using UnityEngine;

public class Juicer : MonoBehaviour
{
    [SerializeField] private Transform juiceOutPoint;
    
    Queue<Fruit> fruits = new Queue<Fruit>();

    public void EnqueuItem(Fruit fruit)
    {
        fruits.Enqueue(fruit);
    }
    private void DequeuItem()
    {
        var fruit = fruits.Dequeue();
        var juice = fruit.TurnToJuice();

        juice.transform.position = juiceOutPoint.position;

        var pos = new Vector3(0, 0, 10);
        fruit.Throw(pos);
    }

    private void Juicing()
    {
        
    }
}
