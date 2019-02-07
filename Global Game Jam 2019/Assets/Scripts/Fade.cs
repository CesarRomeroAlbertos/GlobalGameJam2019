using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    float startTime;
    public float endTime;
    bool start;
    public bool end;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        sprite = GetComponent<SpriteRenderer>();
        start = true;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(1, 0, (Time.time - startTime) / 2));
            if (sprite.color.a <= 0)
                start = false;
        }
        if(end)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(0, 1, (Time.time - endTime) / 2));
        }
    }
}
