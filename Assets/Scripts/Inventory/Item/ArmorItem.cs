using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmorItem : Item
{
    public int Armor;
    public int Health;
    public int Mana;
    public int Damage;
    public int CritChange;


    public ITEMTYPE ItemType;
    public override void OnEndDrag(PointerEventData eventData)
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
                        if (result.gameObject.name == "Slot")
                        {
                            if (item == result.gameObject.GetComponentInParent<Inventory>().InventoryType)
                            {
                                if (result.gameObject.GetComponent<StatsSlot>() != null && !result.gameObject.GetComponent<Slot>().IsFull)
                                {
                                    if (ItemType == result.gameObject.GetComponent<StatsSlot>().ItemType)
                                    {
                                        SetItemSlot(this, result.gameObject);
                                        GameManager.Instance.PlayerController.GetComponent<Stats>().UpdateStats(armor:Armor,health:Health,mana:Mana,damage:Damage,crit:CritChange);//Ekle
                                        control = false;
                                        break;
                                    }
                                }


                                else if (!result.gameObject.GetComponent<Slot>().IsFull)
                                {
                                    if (this.Slot.GetComponentInParent<Inventory>().InventoryType==INVENTORYTYPE.STATSINVENTORY)
                                    {
                                        GameManager.Instance.PlayerController.GetComponent<Stats>().UpdateStats(armor: -Armor, health: -Health, mana: -Mana, damage: -Damage, crit: -CritChange); //cikar
                                    }                                   
                                    SetItemSlot(this, result.gameObject);                                    
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
                            GameManager.Instance.PlayerController.GetComponent<Stats>().UpdateStats(armor: -Armor, health: -Health, mana: -Mana, damage: -Damage, crit: -CritChange); //cikar
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

    public override void OnPointerClick(PointerEventData eventData)
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
            case INVENTORYTYPE.NORMINVENTORY:
                foreach (var item in UIManager.Instance.StatsEnvanter.GetComponent<Inventory>().slots)
                {
                    if (ItemType == item.GetComponent<StatsSlot>().ItemType)
                    {
                        if (!item.GetComponent<StatsSlot>().IsFull)
                        {                    
                            SetItemSlot(this, item);
                            GameManager.Instance.PlayerController.GetComponent<Stats>().UpdateStats(armor: Armor, health: Health, mana: Mana, damage: Damage, crit: CritChange); //ekle
                            break;
                        }
                        else
                        {
                            ArmorItem newArmor = item.GetComponentInChildren<ArmorItem>();
                            GameManager.Instance.PlayerController.GetComponent<Stats>().UpdateStats(armor: Armor, health: Health, mana: Mana, damage: Damage, crit: CritChange); //ekle
                            GameManager.Instance.PlayerController.GetComponent<Stats>().UpdateStats(armor: -newArmor.Armor, health: -newArmor.Health, mana: -newArmor.Mana, damage: -newArmor.Damage, crit: -newArmor.CritChange); //cikar
                            ItemSwap(item.GetComponentInChildren<ArmorItem>(), this, item, this.Slot.gameObject);
                            break;
                        }
                    }
                }
                break;
            case INVENTORYTYPE.STATSINVENTORY:
                foreach (var item in UIManager.Instance.NormEnvanter.GetComponent<Inventory>().slots)
                {
                    if (!item.GetComponent<Slot>().IsFull)
                    {
                        GameManager.Instance.PlayerController.GetComponent<Stats>().UpdateStats(armor: -Armor, health: -Health, mana: -Mana, damage: -Damage, crit: -CritChange); //cikar
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
