using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Mov player;

    public float leftBound;
    public float rightBound;

    public float leftLimit;
    public float rightLimit;

    public bool isLerping;

    private float third;

    public float followPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        third = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect / 3;

        leftBound = player.transform.position.x - third;
        rightBound = player.transform.position.x + third;

        leftLimit = player.transform.position.x - (third / 2);
        rightLimit = player.transform.position.x + (third / 2);

        isLerping = false;

        followPos = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        leftBound = transform.position.x - third;
        rightBound = transform.position.x + third;

        leftLimit = player.transform.position.x - (third / 2);
        rightLimit = player.transform.position.x + (third / 2);

        if (player.right && player.transform.position.x > leftBound)
        {
            followPos += 0.15f;
        }
        else if (player.right && player.transform.position.x < leftBound)
        {
            followPos += 0.1f;
        }
        
        if (player.left && player.transform.position.x < rightBound)
        {
            followPos -= 0.15f;
        }
        else if (player.left && player.transform.position.x > rightBound)
        {
            followPos -= 0.1f;
        }

        transform.position = new Vector3(followPos, transform.position.y, transform.position.z);
    }
}
