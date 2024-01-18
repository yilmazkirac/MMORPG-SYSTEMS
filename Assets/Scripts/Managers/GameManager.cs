using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GraphicRaycaster Raycaster;
    public GameObject Canvas;
    public PlayerController PlayerController;

    public bool UIMode;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }    

    private void Start()
    {
        PlayerPrefs.SetFloat("Coin",99999);
    }

    public void UIClose()
    {
        foreach (var item in UIManager.Instance.Inventorys)
        {
            if (item.activeInHierarchy)
            {
                item.SetActive(false);
                break;
            }
        }
        foreach (var item in UIManager.Instance.Inventorys)
        {
            if (item.activeInHierarchy)
            {                
                break;
            }
            else
            {
                DeActiveUIMode();
            }
        }
    }
    public void ActiveUIMode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerController.IsUIMode = true;
        UIMode = true;
    }
    public void DeActiveUIMode()
    {
        bool IsControl=false;
        foreach (var item in UIManager.Instance.Inventorys)
        {
            if (item.activeInHierarchy)
            {
                IsControl=true; 
            }
        }
        if (!IsControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerController.IsUIMode = false;
            UIMode = false;
        }       
    }
}
