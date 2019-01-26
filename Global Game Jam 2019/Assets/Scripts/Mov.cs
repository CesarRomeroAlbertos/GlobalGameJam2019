using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{

    public bool left;
    public bool right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            left = true;
            right = false;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            right = true;
            left = false;
        }
        else
        {
            right = left = false;
        }
        if (Input.GetKey(KeyCode.W)) transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        if (Input.GetKey(KeyCode.S)) transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
    }
}
