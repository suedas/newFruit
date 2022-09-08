using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragDrop : MonoBehaviour
{
    
    Vector3 offset,orginalPos;
    public GameObject DropArea;
    public GameObject ss;
    //LayerMask layer = (1 << 6);
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
        //transform.position = GetMouse() + offset;
        transform.position =new Vector3(GetMouse().x,7,GetMouse().z) + offset;
    }
    private void OnMouseUp()
    {
        var rayOrgin = Camera.main.transform.position;
        var rayDirection = GetMouse() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrgin, rayDirection, out hitInfo))
        {
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.transform.tag=="DropArea")
            {
                
                if (DropArea.transform.childCount==0)
                {
                    //transform.GetComponent<Collider>().enabled = false;
                    transform.DOMove( new Vector3(hitInfo.transform.position.x, orginalPos.y, hitInfo.transform.position.z),.2f);// hitInfo.transform.position;
                    transform.parent = DropArea.transform;
                 }
                else if(DropArea.transform.childCount==1)
                {
                    transform.DOMove( new Vector3(hitInfo.transform.position.x+4, orginalPos.y, hitInfo.transform.position.z),.2f);// hitInfo.transform.position;
                    transform.parent = DropArea.transform;
                    StartCoroutine(Match());
                }             
            }     
            else
            {
                transform.DOMove(orginalPos,.2f);
                transform.parent = null;                
            }
        }
        else if(!transform.parent && !transform.parent.CompareTag("DropArea"))
        {
            transform.DOMove(orginalPos, .2f);
            transform.parent = null;
        }           
        transform.GetComponent<Collider>().enabled = true;      
    }
    Vector3 GetMouse()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
       // return Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x,7,mouseScreenPos.z));
    }
    
     public IEnumerator Match()
     {
        yield return new WaitForSeconds(.2f);
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
}
