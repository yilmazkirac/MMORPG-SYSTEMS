using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellectItem : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;

    public void ActiveItem(string ItemName)
    {
        foreach (var item in _items)
        {
            if (item.name==ItemName)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }            
        }
    }
}
