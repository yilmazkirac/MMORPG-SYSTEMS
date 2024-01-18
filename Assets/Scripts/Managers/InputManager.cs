using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   public static InputManager Instance;
    private void Awake()
    {
        if (Instance != null&& Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private int currentIndex=0;
    public Item item=null;
    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {

  
        if (Input.GetKeyDown(KeyCode.Q))
        {
           // AnimatorManager.Instance.StartAnim("Gathering");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AnimatorManager.Instance.IsStopAnim = true;
            // AnimatorManager.Instance.StartAnimIsStop("Miner");
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentIndex != 1)
            {
                item = UIManager.Instance.UseEnvanter.GetComponent<Inventory>().slots[0].GetComponentInChildren<Item>();
                if (item != null)
                {
                    AnimatorManager.Instance.StartAnim(animName: "SwordPick");
                    GameManager.Instance.PlayerController.GetComponent<SellectItem>().ActiveItem(item.ItemName);
                    currentIndex = 1;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentIndex != 2)
            {
                item = UIManager.Instance.UseEnvanter.GetComponent<Inventory>().slots[1].GetComponentInChildren<Item>();
                if (item != null)
                {
                    AnimatorManager.Instance.StartAnim(animName: "SwordPick");
                    GameManager.Instance.PlayerController.GetComponent<SellectItem>().ActiveItem(item.ItemName);
                    currentIndex = 2;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentIndex!=3)
            {
               item = UIManager.Instance.UseEnvanter.GetComponent<Inventory>().slots[2].GetComponentInChildren<Item>();
                if (item != null)
                {
                    AnimatorManager.Instance.StartAnim(animName: "SwordPick");
                    GameManager.Instance.PlayerController.GetComponent<SellectItem>().ActiveItem(item.ItemName);
                    currentIndex = 3;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (currentIndex != 4)
            {
                item = UIManager.Instance.UseEnvanter.GetComponent<Inventory>().slots[3].GetComponentInChildren<Item>();
                if (item != null)
                {
                    AnimatorManager.Instance.StartAnim(animName: "SwordPick");
                    GameManager.Instance.PlayerController.GetComponent<SellectItem>().ActiveItem(item.ItemName);
                    currentIndex = 4;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (item!=null&&!GameManager.Instance.UIMode)
            {
                if (item.ItemName=="Sword")
                {
                    string itemName = item.ItemName;
                    AnimatorManager.Instance.StartAnim(true, animName: itemName, boolName: "IsStop");
                }                
            }            
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (item != null && !GameManager.Instance.UIMode)
            {
                if (item.ItemName == "Sword")
                {
                    AnimatorManager.Instance.StartAnim(false,boolName: "IsStop");
                }
            }           
        }
              



        if (Input.GetKeyDown(KeyCode.I))
        {
           UIManager.Instance.ActivePanel(UIManager.Instance.NormEnvanter);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.Instance.ActivePanel(UIManager.Instance.StatsEnvanter);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.Instance.DeActiveUIMode();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (AnimatorManager.Instance.IsStopAnim)
            {
                AnimatorManager.Instance.Animator.SetBool("IsStop", false);
                AnimatorManager.Instance.IsStopAnim = false;
            }
            else
            {
                GameManager.Instance.UIClose();
            }
        }
    }
}
