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

    public ParticleSystem splash;

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
        if(transform.position.y>=1.6f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        }
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

        if (!grounded && rigidBody.velocity.y <= 0)
        {
            rigidBody.AddForce(new Vector2(0, Physics.gravity.y * rigidBody.mass * 3));
        }

        if (Mathf.Abs(rigidBody.velocity.x) > 0.1)
        {
            GetComponent<SpriteRenderer>().flipX = rigidBody.velocity.x < 0;
        }

        anim.SetFloat("velocity", rigidBody.velocity.x);
        anim.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        anim.SetBool("grounded", grounded);
        anim.SetFloat("verticalSpeed", rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            grounded = true;
            if (!GetComponent<SpriteRenderer>().flipX)
                Instantiate(splash, transform.position - new Vector3(-1, 0.5f, 0), Quaternion.identity, this.transform);
            else
                Instantiate(splash, transform.position - new Vector3(1, 0.5f, 0), Quaternion.identity, this.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Floor"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
            grounded = false;
    }

}
