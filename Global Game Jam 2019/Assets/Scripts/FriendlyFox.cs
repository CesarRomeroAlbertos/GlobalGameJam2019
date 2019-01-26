using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class FriendlyFox : MonoBehaviour
{

    public bool grounded;
    bool run;
    Movement player;

    Animator anim;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Movement>();
        grounded = true;
        run = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x-transform.position.x)<1)
        {
            if (run)
            {
                this.GetComponent<Animator>().SetTrigger("RunAway");
                run = false;
            }
            else
            {
                this.GetComponent<Animator>().SetTrigger("Follow");
                run = true;
            }

            if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>3)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(10*(GetComponent<Rigidbody2D>().velocity.x/Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)),
                    GetComponent<Rigidbody2D>().velocity.y);
            }
        }

        anim.SetFloat("velocity", rigidBody.velocity.x);
        anim.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        anim.SetBool("grounded", grounded);
        anim.SetFloat("verticalSpeed", rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
            grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
            grounded = false;
    }

}
