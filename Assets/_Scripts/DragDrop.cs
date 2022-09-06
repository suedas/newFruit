using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset,orginalPos;
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
            if (hitInfo.transform.tag == "DropArea")
            {
                transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + 4, hitInfo.transform.position.z);// hitInfo.transform.position;
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


        


    //Vector3 objectPos;
    //private void OnMouseDown()
    //{
    //    objectPos = Camera.main.WorldToScreenPoint(transform.position);

    //}
    //private void OnMouseDrag()
    //{
    //    Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectPos.z);
    //    transform.position = Camera.main.ScreenToWorldPoint(pos);

    //}

}
