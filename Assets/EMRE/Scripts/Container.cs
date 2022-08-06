using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container<T> : ScriptableObject
{
    [SerializeField] private T[] _elements;


    public T this[int index] => _elements[index];
}
