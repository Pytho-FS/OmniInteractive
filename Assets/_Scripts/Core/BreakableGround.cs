using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BreakableGround
{
    [SerializeField] BoxCollider2D surfaceCol;
    [Range(2,5)] [SerializeField] int destroyTimer;
    float shakeSpeed = 0.6f;

    IEnumerator shake()
    {
        yield return new WaitForSeconds(destroyTimer-shakeSpeed);

    }



}
