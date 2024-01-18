using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, ICommand
{
    public void Execute()
    {
        if (InputManager.Instance.item == null)
            return;            
        
        if (InputManager.Instance.item.ItemName == "Axe")
        {
            AnimatorManager.Instance.IsStopAnim = true;
            AnimatorManager.Instance.StartAnim(true, animName: InputManager.Instance.item.ItemName, boolName: "IsStop");
            StartCoroutine(Timer());    
        }       
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
       
            AnimatorManager.Instance.IsStopAnim = false;
            gameObject.GetComponent<ItemObject>().Execute();
            AnimatorManager.Instance.StartAnim(false, boolName: "IsStop");
            Destroy(gameObject);
        
    }
}
