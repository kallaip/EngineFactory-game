using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int id;
    public string targetName;
    public Material goodMaterial;
    public Material badMaterial;
    public Vector3[] occupyMask;

    public void setvalidity(bool valid)
    {
        Material[] mats;
        mats = gameObject.GetComponentInChildren<Renderer>().materials;
        
        if (valid)
        {
            gameObject.GetComponentInChildren<Renderer>().material = goodMaterial;

            //TODO: ez miért nem működik több anyagra?
            /*
            for (int i=0; i< gameObject.GetComponentInChildren<Renderer>().materials.Length; i++)
            {
                gameObject.GetComponentInChildren<Renderer>().materials[i] = goodMaterial;
            }
            */
        } else
        {
            gameObject.GetComponentInChildren<Renderer>().material = badMaterial;
            //TODO: ez miért nem működik több anyagra?
            /*
            for (int i = 0; i < gameObject.GetComponentInChildren<Renderer>().materials.Length; i++)
            {
                gameObject.GetComponentInChildren<Renderer>().materials[i] = badMaterial;
            }
            */
        }
    }

    public void rotateMask(Vector3 Rotation)
    {
        for (int i = 0; i < occupyMask.Length; i++)
        {
            occupyMask[i] = Quaternion.Euler(Rotation) * occupyMask[i];
            occupyMask[i].x = (int) Math.Round(occupyMask[i].x);
            occupyMask[i].y = (int) Math.Round(occupyMask[i].y);
            occupyMask[i].z = (int) Math.Round(occupyMask[i].z);
            
        }

    }
}
