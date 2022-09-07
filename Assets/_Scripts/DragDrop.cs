using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    
    Vector3 offset,orginalPos;
    public GameObject DropArea;
    public GameObject ss;
    // public string destinationTag = "DropArea";

    //private void Awake()
    //{
    //    orginalPos = transform.position;
    //}


    void Awake()
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
                if (DropArea.transform.childCount==0)
                {
                    transform.position = new Vector3(hitInfo.transform.position.x-3, hitInfo.transform.position.y + 4, hitInfo.transform.position.z);// hitInfo.transform.position;
                    transform.parent = DropArea.transform;
                }
                else if(DropArea.transform.childCount==1)
                {
                    transform.position = new Vector3(hitInfo.transform.position.x+4, hitInfo.transform.position.y + 4, hitInfo.transform.position.z);// hitInfo.transform.position;
                    transform.parent = DropArea.transform;
                    Match();
                }
              
            }     
            else
            {
                transform.position = orginalPos;
                transform.parent = null;                
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
    public void Match()
    {
        if (DropArea.transform.GetChild(0).tag==DropArea.transform.GetChild(1).tag)
        {
            int dropChild = DropArea.transform.childCount;
            for (int i = 0; i < dropChild; i++)
            {
                Destroy(DropArea.transform.GetChild(i).gameObject);              
            }
            GameObject newFruit= Instantiate(ss,new Vector3( DropArea.transform.position.x,DropArea.transform.position.y+4,DropArea.transform.position.z), Quaternion.identity);
            newFruit.transform.parent = DropArea.transform;
        }
        else
        {
            DropArea.transform.GetChild(1).position = orginalPos;
        }
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
