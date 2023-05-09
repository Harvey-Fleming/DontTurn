using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionAnimationSwitching : MonoBehaviour
{
    Animator animator;
    public CorruptionScript corruptionScript;
    public RuntimeAnimatorController normalAnimationController;
    public RuntimeAnimatorController corruptedAnimationController; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(corruptionScript.time == 100)
        {
  
            animator.SetBool("IsCorrupt", true);
            animator.runtimeAnimatorController = corruptedAnimationController; 
        }
        else
        {
            animator.SetBool("IsCorrupt", false);
            animator.runtimeAnimatorController = normalAnimationController;
        }
    }
}
