using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showKeyPress : MonoBehaviour
{
    public KeyCode key;

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(key))
        {
            sprite.color = new Color(0.75f, 0.75f, 0.75f);
            transform.localScale = new Vector3(0.9f, 0.9f, 1);
        }
        else
        {
            sprite.color = Color.white;
            transform.localScale = Vector3.one;
        }
    }
}
