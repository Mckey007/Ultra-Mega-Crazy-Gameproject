using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    // this will somewhat look like the enemy is being erased by a pencil eraser
    public void Play(float duration, GameObject parent)
    {
        transform.DOLocalMoveY(0, duration).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            Destroy(parent);
        });
    }
}
