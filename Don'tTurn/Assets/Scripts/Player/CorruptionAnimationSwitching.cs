using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionAnimationSwitching : MonoBehaviour
{
    Animator animator;
    public CorruptionScript corruptionScript;
    public RuntimeAnimatorController normalAnimationController;
    public RuntimeAnimatorController corruptedAnimationController;
    private bool audioPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(corruptionScript.time == 100f)
        {
            if(audioPlayed == false)
            {
                TransformAudio();
            }
            animator.SetBool("IsCorrupt", true);
            animator.runtimeAnimatorController = corruptedAnimationController; 
        }
        else
        {
            audioPlayed = false;
            animator.SetBool("IsCorrupt", false);
            animator.runtimeAnimatorController = normalAnimationController;
        }
    }

    void TransformAudio()
    {
        if (animator.GetBool("IsCorrupt") == false)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.transformation, this.transform.position);
            audioPlayed = true;
            Debug.Log("play transform");
        }
    }
}
