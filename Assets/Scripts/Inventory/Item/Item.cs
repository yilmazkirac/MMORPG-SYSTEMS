using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public string ItemName;
    public List<INVENTORYTYPE> Type;


    [HideInInspector] public Slot Slot;
    [HideInInspector] public Slot OldSlot;
    [HideInInspector] public GraphicRaycaster Raycaster;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            transform.SetParent(GameManager.Instance.Canvas.transform);
            transform.position = Input.mousePosition;
            
        }        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Input.mousePosition;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Raycaster = GameManager.Instance.Raycaster;
            eventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new();
            Raycaster.Raycast(eventData, raycastResults);

            if (raycastResults.Count > 1)
            {
                bool control = true;
                foreach (var item in Type)
                {
                    foreach (var result in raycastResults)
                    {
                        if (result.gameObject.name=="Slot")
                        {
                            if (item == result.gameObject.GetComponentInParent<Inventory>().InventoryType)
                            {
                                if (!result.gameObject.GetComponent<Slot>().IsFull)
                                {
                                    SetItemSlot(this, raycastResults[1].gameObject);
                                    control = false;
                                    break;
                                }
                            }
                        }
                    }
                 
                }
                if (control)
                {
                    ReturnSlot(this);
                }
            }
            else
            {
                if (Slot.GetComponentInParent<Inventory>().InventoryType != INVENTORYTYPE.NORMINVENTORY)
                {
                    foreach (var item in UIManager.Instance.NormEnvanter.GetComponent<Inventory>().slots)
                    {
                        if (!item.GetComponent<Slot>().IsFull)
                        {
                            SetItemSlot(this, item);
                            break;
                        }
                        else
                        {
                            ReturnSlot(this);
                        }
                    }
                }
                else
                {
                    RemoveItem(this);
                }
            }
        }
    }

    public void SetItemSlot(Item item,GameObject slot) 
    {
        item.gameObject.transform.SetParent(slot.transform);
        item.gameObject.transform.position = slot.transform.position;

       /* if (item.OldSlot.GetComponentInParent<Inventory>().InventoryType != INVENTORYTYPE.NORMINVENTORY && slot.GetComponentInParent<Inventory>().InventoryType == INVENTORYTYPE.NORMINVENTORY)
        {
            slot.GetComponentInParent<NormInventory>().SetSlotsValue(1);
        }
        if (slot.GetComponentInParent<Inventory>().InventoryType != INVENTORYTYPE.NORMINVENTORY && item.Slot.GetComponentInParent<Inventory>().InventoryType == INVENTORYTYPE.NORMINVENTORY)
        {
            item.Slot.GetComponentInParent<NormInventory>().SetSlotsValue(-1);
        }*/

        item.OldSlot.IsFull = false;                
        Slot = slot.GetComponent<Slot>();
        item.Slot.IsFull = true;
        OldSlot = Slot;
    }
    public void ItemSwap(Item item1, Item item2, GameObject item1Slot, GameObject item2Slot)
    {
        item1.gameObject.transform.SetParent(item2Slot.transform);
        item1.gameObject.transform.position = item2Slot.transform.position;
        item2.gameObject.transform.SetParent(item1Slot.transform);
        item2.gameObject.transform.position = item1Slot.transform.position;

        item1.Slot = item2Slot.GetComponent<Slot>();
        item2.Slot = item1Slot.GetComponent<Slot>();

        item1.OldSlot = item1.Slot;
        item2.OldSlot = item2.Slot;
    }
    public void ReturnSlot(Item item)
    {
        item.gameObject.transform.SetParent(Slot.gameObject.transform);
        item.gameObject.transform.position = Slot.gameObject.transform.position;
    }
    public void RemoveItem(Item item)
    {
        item.Slot.IsFull = false;
        Destroy(gameObject);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }


    private void UseItem()
    {
        switch (Slot.GetComponentInParent<Inventory>().InventoryType)
        {           
            case INVENTORYTYPE.DEPOINVENTOR:
                foreach (var item in UIManager.Instance.NormEnvanter.GetComponent<Inventory>().slots)
                {
                    if (!item.GetComponent<Slot>().IsFull)
                    {
                        SetItemSlot(this, item);
                        break;
                    }
                }
                break;
        }
    }
}
