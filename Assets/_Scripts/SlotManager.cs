using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    Vector3 offset, orginalPos;
    public GameObject boxArea;
    // public string destinationTag = "DropArea";
    private void Awake()
    {
        orginalPos = transform.position;
    }
   
    private void OnMouseDown()
    {
        offset = transform.position - GetMouse();
        transform.GetComponent<Collider>().enabled = false;


    }
    private void OnMouseDrag()
    {
        transform.position = GetMouse() + offset;


    }
    private void OnMouseUp()
    {
        var rayOrgin = Camera.main.transform.position;
        var rayDirection = GetMouse() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrgin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == "box")
            {
                transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y+3, hitInfo.transform.position.z);// hitInfo.transform.position;
                transform.parent =hitInfo.transform;
                Debug.Log(hitInfo.transform.childCount); 
            }
            else
            {
                transform.position = orginalPos;
            }
        }
        transform.GetComponent<Collider>().enabled = true;
    }
    Vector3 GetMouse()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
