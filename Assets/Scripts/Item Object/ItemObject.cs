using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, ICommand
{
   public Item Item;
   public int ItemCount=1;

    public void Execute()
    {
        
         if (Item is StacableItem)
         {
             bool kontrol = false;
             foreach (var item in UIManager.Instance.NormEnvanter.GetComponent<Inventory>().slots)
             {
                if (item.GetComponent<Slot>().IsFull && item.GetComponentInChildren<Item>().ItemName == Item.ItemName && item.GetComponentInChildren<StacableItem>().ItemCount + ItemCount <= item.GetComponentInChildren<StacableItem>().MaxItemCount)
                {
                    item.GetComponentInChildren<StacableItem>().SetItemCount(ItemCount);
                    item.GetComponentInChildren<StacableItem>().ItemCountText.text = item.GetComponentInChildren<StacableItem>().ItemCount.ToString();
                    kontrol = true;
                    break;
                }

                else if (item.GetComponent<Slot>().IsFull && item.GetComponentInChildren<Item>().ItemName == Item.ItemName && item.GetComponentInChildren<StacableItem>().ItemCount == item.GetComponentInChildren<StacableItem>().MaxItemCount)
                {
                    continue;
                }


                else if (item.GetComponent<Slot>().IsFull && item.GetComponentInChildren<Item>().ItemName == Item.ItemName && item.GetComponentInChildren<StacableItem>().ItemCount + ItemCount > item.GetComponentInChildren<StacableItem>().MaxItemCount)
                {
                

                  
                    foreach (var item2 in UIManager.Instance.NormEnvanter.GetComponent<Inventory>().slots)
                    {
                        if (!item2.GetComponent<Slot>().IsFull)
                        {
                            int valueKalan = ((item.GetComponentInChildren<StacableItem>().ItemCount + ItemCount) - item.GetComponentInChildren<StacableItem>().MaxItemCount);
                            int valueEklenecek = (item.GetComponentInChildren<StacableItem>().MaxItemCount - item.GetComponentInChildren<StacableItem>().ItemCount);
                            item.GetComponentInChildren<StacableItem>().SetItemCount(valueEklenecek);
                            item.GetComponentInChildren<StacableItem>().ItemCountText.text = item.GetComponentInChildren<StacableItem>().ItemCount.ToString();
                            //--------------------
                            Item newItem = Instantiate(Item, item2.transform);
                            newItem.GetComponentInChildren<StacableItem>().SetItemCount(valueKalan);
                          //  newItem.GetComponentInChildren<StacableItem>().ItemCountText.text = item2.GetComponentInChildren<StacableItem>().ItemCount.ToString();
                            newItem.transform.position = item2.transform.position;
                            item2.GetComponent<Slot>().IsFull = true;
                            newItem.Slot = item2.GetComponent<Slot>();
                            newItem.OldSlot = item2.GetComponent<Slot>();
                            break;
                        }
                    }
                    kontrol = true;
                    break;
                }
             }

             if (!kontrol)
             {
             
                foreach (var item in UIManager.Instance.NormEnvanter.GetComponent<Inventory>().slots)
                {
                    if (!item.GetComponent<Slot>().IsFull)
                    {                        
                        Item newItem = Instantiate(Item, item.transform);
                        newItem.GetComponentInChildren<StacableItem>().SetItemCount(ItemCount);
                      //  newItem.GetComponentInChildren<StacableItem>().ItemCountText.text = item.GetComponentInChildren<StacableItem>().ItemCount.ToString();
                        newItem.transform.position = item.transform.position;
                        item.GetComponent<Slot>().IsFull = true;
                        newItem.Slot = item.GetComponent<Slot>();
                        newItem.OldSlot = item.GetComponent<Slot>();
                        break;
                    }
                }
            }
         }
       else 
        {       
            foreach (var item in UIManager.Instance.NormEnvanter.GetComponent<Inventory>().slots)
            {
                if (!item.GetComponent<Slot>().IsFull)
                {
                    Item newItem = Instantiate(Item, item.transform);
                    newItem.transform.position = item.transform.position;
                    item.GetComponent<Slot>().IsFull = true;
                    newItem.Slot = item.GetComponent<Slot>();
                    newItem.OldSlot = item.GetComponent<Slot>();
                    break;
                }
            }
        }
    }
}
