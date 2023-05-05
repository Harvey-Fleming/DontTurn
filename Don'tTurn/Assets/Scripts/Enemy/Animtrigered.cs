using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animtrigered : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    public Collider2D TheOtherCollider;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 2f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 2f);


        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 2f, Color.red);

        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 2f);


        if (hit == TheOtherCollider)
        {
            anim.SetBool("Attack", true);
        }
        else if (hit.collider == null)
        {
            anim.SetBool("Attack", false);
        }



        if (hit2 == TheOtherCollider)
        {
            anim.SetBool("Attack", true);
        }
        else if (hit2.collider == null)
        {
            anim.SetBool("Attack", false);
        }
    }
}

