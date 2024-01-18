using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public static AnimatorManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public Animator Animator;
    public bool IsStopAnim;

    public void StartAnim(bool animBool=false, string animName=null, string boolName = null)
    {
        if (boolName!=null)
        {
            if (animBool)
            {              
                Animator.SetBool(boolName, animBool);
            }
            else
            {
                Animator.SetBool(boolName, animBool);
            }      
        }
        if (animName != null)
        {
            Animator.CrossFade(animName, 0.001f);
        }   
    }
 /*   public void StartAnimIsStop(string animName)
    {
        Animator.SetBool("IsStop", true);
        Animator.CrossFade(animName, 0.001f);
    }*/
}
