using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{   
    private Factory factory;
    [SerializeField]
    private Text costText;
    // Start is called before the first frame update
    void Start()
    {
        factory = this.GetComponent<Factory>();
        costText.text = "Total cost: $0";


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateCost()
    {
        costText.text = string.Format("Total cost: ${0}" , factory.Cost.ToString());
    }
}
