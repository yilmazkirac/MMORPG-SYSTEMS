using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorMove : MonoBehaviour, IDragHandler
{

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        { 
           transform.position = Input.mousePosition;
        }
    }
}
