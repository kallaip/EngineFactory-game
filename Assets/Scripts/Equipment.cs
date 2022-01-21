using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int id;
    public int cost;
    public string equipmentName;
    public Vector3[] occupyMask;

    public void rotateMask(Vector3 Rotation)
    {
        for (int i = 0; i < occupyMask.Length; i++)
        {
            occupyMask[i] = Quaternion.Euler(Rotation) * occupyMask[i];
            occupyMask[i].x = (int)Math.Round(occupyMask[i].x);
            occupyMask[i].y = (int)Math.Round(occupyMask[i].y);
            occupyMask[i].z = (int)Math.Round(occupyMask[i].z);

        }

    }
}
