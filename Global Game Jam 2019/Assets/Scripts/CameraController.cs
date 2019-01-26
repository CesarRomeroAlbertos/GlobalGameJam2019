using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {

        public Movement player;
        private Rigidbody2D body;

        public float leftBound;
        public float rightBound;

        private float third;

        public float followPos;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

            third = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect / 3;

            leftBound = player.transform.position.x - third;
            rightBound = player.transform.position.x + third;

            followPos = player.transform.position.x;

            body = player.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

            leftBound = transform.position.x - third;
            rightBound = transform.position.x + third;

            if (body.velocity.x > 0 && player.transform.position.x > leftBound)
            {
                followPos += player.speed * Time.deltaTime * 1.5f;
            }
            else if (body.velocity.x > 0 && player.transform.position.x < leftBound)
            {
                followPos += player.speed * Time.deltaTime;
            }

            if (body.velocity.x < 0 && player.transform.position.x < rightBound)
            {
                followPos -= player.speed * Time.deltaTime * 1.5f;
            }
            else if (body.velocity.x < 0 && player.transform.position.x > rightBound)
            {
                followPos -= player.speed * Time.deltaTime;

            }

            transform.position = new Vector3(followPos, transform.position.y, transform.position.z);
        }
    }
}
