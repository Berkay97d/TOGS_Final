using System;
using LazyDoTween.Core;
using UnityEngine;

public class Juice : Item
{
    [SerializeField] private DoLazyGroup doLazyGroup;


    private void Start()
    {
        PlayAnimation();
    }


    private void PlayAnimation()
    {
        doLazyGroup.Play();
    }
}
