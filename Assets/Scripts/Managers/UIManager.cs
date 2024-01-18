using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    [Header("INVENTORY CONTROL---------------")]
    public GameObject[] Inventorys;
    [Header("INVENTORY---------------")]
    public GameObject NormEnvanter;
    public GameObject UseEnvanter;
    public GameObject StatsEnvanter;
    public GameObject ShopPanel;
    public GameObject DepoPanel;

    [Header("SHOP PANEL---------------")]
    public GameObject[] ShopPanels;
    [Header("STATS TEXT---------------")]
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI ManaText;
    public TextMeshProUGUI ArmorText;
    public TextMeshProUGUI CritText;

 
    public void ActivePanel(GameObject panel)
    {
        bool IsActive = !panel.activeSelf;
        if (IsActive)
        {
            panel.SetActive(IsActive);
            GameManager.Instance.ActiveUIMode();
        }
        else
        {
            panel.SetActive(IsActive);
            GameManager.Instance.DeActiveUIMode();
        }        
    }

    public void AllDeActiveAndActivePanel(GameObject panel)
    {
        foreach (var item in ShopPanels)
        {
            item.SetActive(false);
        }
        panel.SetActive(true);
    }
}
