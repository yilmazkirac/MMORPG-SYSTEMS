using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolItem : Item
{
    public int Damage;

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SetItemUseInventorySlot();
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
