using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentHandler : MonoBehaviour
{
    [SerializeField]
    private Factory factory;
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private Equipment[] equipments;
    [SerializeField]
    private Layout layout;
    [SerializeField]
    private Target[] targets;
    [SerializeField]
    private Tool[] tools;


    private Equipment selectedEquipment;

    private Tool selectedTool;
    private Tool toolInst = null;

    private Target selectedTarget;
    private Target targetInst = null;
 

    private Vector3 targetPosition;
    private int targetRotation = 0;

    private bool targetValid;



    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector3(0, 0, 0);
        calculateTargetPosition();
        setTargetPosition();
        
    }

    // Update is called once per frame
    void Update()
    {
        calculateTargetPosition();
        setTargetPosition();
        

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) InteractWithLayout();
        }

        if (Input.GetMouseButtonDown(1) && targetInst != null)
        {
            targetRotation += 90;
            if (targetRotation == 360) targetRotation = 0;
            Debug.Log("Target rotation: " + targetRotation);
            targetInst.transform.Rotate(new Vector3(0, 90, 0));
            targetInst.rotateMask(new Vector3(0, 90, 0));
        }
    }

    public void InteractWithLayout()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Equipment clickedEquipment; 
        if (Physics.Raycast(ray, out hit)) 
        {
            Vector3 gridPosition = layout.CalculateGridPosition(hit.point);

            if (!layout.CheckEquipmentAtPosition(gridPosition) && targetValid)
            {
                Debug.Log("Empty slot");
                if (selectedEquipment != null)
                {
                    factory.equipmentCounts[selectedEquipment.id]++;
                    layout.addEquipment(selectedEquipment, gridPosition, targetRotation);
                    uiController.updateCost();
                }

            } else {
                Debug.Log("Used slot");
                // there is something to do with
                if (selectedTool != null)
                { 
                    Debug.Log("Tool id:" + selectedTool.id);
                    if (selectedTool.id == 0)
                    {
                        // delete

                        Debug.Log("Deleting from layout");
                        clickedEquipment = layout.getEquipmentAtPosition(gridPosition);
                        factory.equipmentCounts[clickedEquipment.id]--;
                        layout.removeEquipment(gridPosition);
                        uiController.updateCost();
                    }

                }
            }
        }
    }

    public void EnableBuilder(int equipment)
    {
        selectedTool = null;
        selectedEquipment = equipments[equipment];
        Debug.Log("Selected eq: " + selectedEquipment.equipmentName);

        if (targetInst != null)
        {
            Debug.Log("destroy target");
            Destroy(targetInst.gameObject);
            targetInst = null;
        }
        if (toolInst != null)
        {
            Debug.Log("destroy tool");
            Destroy(toolInst.gameObject);
            toolInst = null;
        }

        int target = equipment;
        selectedTarget = targets[target];
        targetInst = Instantiate(selectedTarget, targetPosition, Quaternion.identity);
        targetRotation = 0;
    }

    public void EnableTool(int tool)
    {
        selectedEquipment = null;
        selectedTool = tools[tool];
        Debug.Log("Selected tool: " + selectedTool.toolName);

        if (targetInst != null)
        {
            Debug.Log("destroy target");
            Destroy(targetInst.gameObject);
            targetInst = null;
        }

        if (toolInst != null)
        {
            Debug.Log("destroy tool");
            Destroy(toolInst.gameObject);
            toolInst = null;
        }

        toolInst = Instantiate(selectedTool, targetPosition, Quaternion.identity);
        targetRotation = 0;
    }


    public void calculateTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
           targetPosition = layout.CalculateGridPosition(hit.point);
        }

    }

    public void setTargetPosition()
    {
        if (targetInst != null)
        {
            targetInst.transform.position = targetPosition;
            validateTarget();
        }
        if (toolInst != null)
            toolInst.transform.position = targetPosition;
    }

    public void validateTarget()
    {
        targetValid = true;
        //testing boundaries

        for (int i = 0; i < targetInst.occupyMask.Length; i++)
        {
            Vector3 np = targetPosition + targetInst.occupyMask[i];
            if ((int)np.x < 0  || (int)np.z < 0  || (int) np.x > 49 || (int)np.z > 49)
            {
                targetValid = false;
                break;
            }
        }

        //testing for overlapping with others
        if (targetValid) { 
            for (int i = 0; i < targetInst.occupyMask.Length; i++)
            {
                if (layout.CheckEquipmentAtPosition(targetPosition + targetInst.occupyMask[i]))
                {
                    targetValid = false;
                    break;
                }
            }
        }

        if (layout.CheckEquipmentAtPosition(targetPosition)) targetValid = false;

        if (targetValid)
        {
            targetInst.setvalidity(true);
        } else
        {
            targetInst.setvalidity(false);
        }

    }

}
