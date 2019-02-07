using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    bool ending;
    bool fade;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        ending = false;
        fade = false;
        startTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ending && startTime == 0)
        {
            startTime = Time.time;
        }
        else if (ending)
        {
            if (Time.time - startTime > 3 && !fade)
            {
                FindObjectOfType<Fade>().end = true;
                FindObjectOfType<Fade>().endTime = Time.time;
                fade = true;
            }
            if (Time.time-startTime > 5)
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Movement>().end = true;
            ending = true;
        }
    }
}
