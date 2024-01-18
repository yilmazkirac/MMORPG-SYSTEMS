using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StacableItem : Item
{
    public int MaxItemCount;
    public int ItemCount;
    public TextMeshProUGUI ItemCountText;
    public TMP_InputField InputField;

    private void Start()
    {
        InputField.gameObject.SetActive(false);
        ItemCountText.text = ItemCount.ToString();
    }
    public void SetItemCount(int value)
    {
        ItemCount += value;
        ItemCountText.text = ItemCount.ToString();
    }

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
                                if (!result.gameObject.GetComponent<Slot>().IsFull)
                                {
                                    SetItemSlot(this, result.gameObject);
                                    control = false;
                                    break;
                                }

                                else
                                {
                                    if (result.gameObject.GetComponentInChildren<StacableItem>()!=null)
                                    {
                                        if (result.gameObject.GetComponentInChildren<Item>().ItemName == ItemName)
                                        {
                                            if (result.gameObject.GetComponentInChildren<StacableItem>().ItemCount + ItemCount <= result.gameObject.GetComponentInChildren<StacableItem>().MaxItemCount)
                                            {
                                                result.gameObject.GetComponentInChildren<StacableItem>().SetItemCount(ItemCount);
                                                result.gameObject.GetComponentInChildren<StacableItem>().ItemCountText.text = result.gameObject.GetComponentInChildren<StacableItem>().ItemCount.ToString();
                                                RemoveItem(this);
                                                control = false;
                                                break;
                                            }
                                            else if ((result.gameObject.GetComponentInChildren<StacableItem>().ItemCount + ItemCount > (result.gameObject.GetComponentInChildren<StacableItem>().MaxItemCount)))
                                            {

                                                int valueKalan = (result.gameObject.GetComponentInChildren<StacableItem>().ItemCount + ItemCount) - (result.gameObject.GetComponentInChildren<StacableItem>().MaxItemCount);
                                                int valueEklenecek = (result.gameObject.GetComponentInChildren<StacableItem>().MaxItemCount - result.gameObject.GetComponentInChildren<StacableItem>().ItemCount);
                                                result.gameObject.GetComponentInChildren<StacableItem>().SetItemCount(valueEklenecek);
                                                result.gameObject.GetComponentInChildren<StacableItem>().ItemCountText.text = result.gameObject.GetComponentInChildren<StacableItem>().ItemCount.ToString();
                                                //--------------------

                                                ItemCount = valueKalan;
                                                ItemCountText.text = ItemCount.ToString();
                                                break;
                                            }
                                        }
                                    }                                    
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

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (eventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift))
        {
            if (ItemCount>1)
            {
                ActiveInputField();
            }            
        }
    }
    private void ActiveInputField()
    {
        InputField.text = ((int)ItemCount / 2).ToString();
        InputField.gameObject.SetActive(true);
    }
    private void DeActiveInputField()
    {
        InputField.text = "";
        InputField.gameObject.SetActive(false);
    }
    public void SmashItem()
    {
        if (InputField.text != "")
        {
            int value;
            bool isOkey = int.TryParse(InputField.text,out value);
            if (isOkey)
            {
                if (value<ItemCount)
                {
                    foreach (var item in UIManager.Instance.NormEnvanter.GetComponent<Inventory>().slots)
                    {
                        if (!item.GetComponent<Slot>().IsFull)
                        {
                            SetItemCount(-value);
                            StacableItem newItem = Instantiate(this, item.transform);
                            newItem.GetComponent<StacableItem>().ItemCount=0;
                            newItem.GetComponent<StacableItem>().SetItemCount(value);
                            // newItem.GetComponent<StacableItem>().ItemCountText.text = newItem.GetComponent<StacableItem>().ItemCount.ToString();
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
        InputField.text = "";
        DeActiveInputField();
    }
}
