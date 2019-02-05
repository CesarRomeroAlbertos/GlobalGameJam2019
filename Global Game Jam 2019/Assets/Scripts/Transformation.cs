using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Transformation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Animator>().SetTrigger("human");
            collision.GetComponent<Movement>().human = true;
            collision.GetComponent<Movement>().speed = collision.GetComponent<Movement>().speed / 2.5f;
        }

    }
}
