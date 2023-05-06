using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionAnimationSwitching : MonoBehaviour
{
    Animator animator;
    public CorruptionScript corruptionScript; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(corruptionScript.time >= 50)
        {
            animator.SetBool("IsCorrupt", true); 
        }
    }
}
