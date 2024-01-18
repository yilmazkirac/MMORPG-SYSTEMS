using UnityEngine;
using UnityEngine.EventSystems;

public class AvailableItem : StacableItem
{


    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
            if (eventData.button == PointerEventData.InputButton.Right && Input.GetKey(KeyCode.LeftShift))
        {
            SetItemUseInventorySlot();
        }
        else if (eventData.button == PointerEventData.InputButton.Right && !Input.GetKey(KeyCode.LeftShift))
        {
            UseItem();
        }
    }     
   
    private void SetItemUseInventorySlot()
    {
        switch (Slot.GetComponentInParent<Inventory>().InventoryType)
        {
            case INVENTORYTYPE.NORMINVENTORY:
                foreach (var item in UIManager.Instance.UseEnvanter.GetComponent<Inventory>().slots)
                {
                    if (!item.GetComponent<Slot>().IsFull)
                    {
                        SetItemSlot(this, item);
                        break;
                    }
                }
                break;
            case INVENTORYTYPE.USEINVENTOR:
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
    private void UseItem()
    {
       switch (ItemName)
        { 
            case "RedPot":

                break;
            case "BluePot":

                break;
            case "GreenPot":

                break;
        }
    }
}
