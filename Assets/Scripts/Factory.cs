using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private int cost;
    public int Cost { get; set; }
    public int[] equipmentCounts = new int[2];


    // Start is called before the first frame update
    void Start()
    {
        Cost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CalculateCost()
    {
        Cost = equipmentCounts[0] * 300 + equipmentCounts[1] * 1200;
    }
}
