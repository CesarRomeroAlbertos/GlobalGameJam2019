using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Mouse : MonoBehaviour
{

    public float speed;
    Movement fox;
    bool running;
    float runTime;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        fox = FindObjectOfType<Movement>();
        running = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fox.transform.position.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
        if (Mathf.Abs(fox.transform.position.x-transform.position.x)<5 && !running)
        {
            runTime = Time.time;
            running = true;
            anim.SetTrigger("Run");
        }
        else if(running)
        {
            if(Time.time-runTime >= 1)
            {
                anim.SetTrigger("Burrow");
            }
            else
            {
                if (fox.transform.position.x > transform.position.x)
                    transform.Translate(speed * Vector2.left);
                else
                    transform.Translate(speed * Vector2.right);
            }
        }
    }
}
