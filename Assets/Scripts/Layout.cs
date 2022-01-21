using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout : MonoBehaviour
{
    private Equipment[,] equipments = new Equipment[50,50];
    // Start is called before the first frame update
    public void addEquipment(Equipment equipment, Vector3 position, int rotation)
    {
        Equipment newEq; 
        newEq = Instantiate(equipment, position, Quaternion.identity);
        newEq.transform.Rotate(new Vector3(0, rotation, 0));
        newEq.rotateMask(new Vector3(0, rotation, 0));
        equipments[(int)position.x, (int)position.z] = newEq;
        for (int i = 0; i < newEq.occupyMask.Length; i++)
        {
            Vector3 newpos = position + newEq.occupyMask[i];
            equipments[(int)newpos.x, (int)newpos.z] = newEq;
        }
    }

    public void removeEquipment(Vector3 position)
    {
        Equipment eq;
        eq = equipments[(int)position.x, (int)position.z];
        for (int i = 0; i < eq.occupyMask.Length; i++)
        {
            Vector3 newpos = position + eq.occupyMask[i];
            equipments[(int)newpos.x, (int)newpos.z] = null;
        }
        Destroy(equipments[(int)position.x, (int)position.z].gameObject);
        equipments[(int)position.x, (int)position.z] = null;
    }

    public bool CheckEquipmentAtPosition(Vector3 position)
    {
        return equipments[(int)position.x, (int)position.z] != null;
    }

    public Equipment getEquipmentAtPosition(Vector3 position)
    {
        return equipments[(int)position.x, (int)position.z];
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.FloorToInt(position.x), 0.25f, Mathf.FloorToInt(position.z));
    }
}
