using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [Header("Upgrade Building Arrows")]
    [SerializeField] private Material ubaMat;
    [SerializeField] private Color ubaInitColor, ubaTargetColor;
    [SerializeField] private float ubaAnimSpeed;
    private void Start()
    {
        ubaMat.color = ubaInitColor;
        ubaMat.DOColor(ubaTargetColor, ubaAnimSpeed)
            .SetEase(Ease.Flash)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
