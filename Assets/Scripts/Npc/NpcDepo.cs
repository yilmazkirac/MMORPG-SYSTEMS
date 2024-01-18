using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDepo : MonoBehaviour, ICommand
{
    public void Execute()
    {
        UIManager.Instance.ActivePanel(UIManager.Instance.DepoPanel);
        UIManager.Instance.NormEnvanter.SetActive(true);
    }
}
