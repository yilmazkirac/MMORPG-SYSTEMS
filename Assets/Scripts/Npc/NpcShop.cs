using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcShop : MonoBehaviour, ICommand
{
    public void Execute()
    {
        UIManager.Instance.ActivePanel(UIManager.Instance.ShopPanel);
        UIManager.Instance.NormEnvanter.SetActive(true);
    }
}
