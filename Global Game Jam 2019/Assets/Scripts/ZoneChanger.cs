using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChanger : MonoBehaviour
{

    public int zone;
    // Start is called before the first frame update
    void Start()
    {
        zone = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zone"))
        {
            collision.enabled = false;
            zone++;
        }
    }
}
