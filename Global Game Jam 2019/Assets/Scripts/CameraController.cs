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
            Camera camera = GetComponent<Camera>();

            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

            third = camera.orthographicSize * camera.aspect / 3;

            leftBound = player.transform.position.x - third;
            rightBound = player.transform.position.x + third;

            followPos = player.transform.position.x;

            body = player.GetComponent<Rigidbody2D>();

            //Aspect Ratio
            float aspectRatio = 16.0f / 9.0f;

            float aspectRatioIni = (float)Screen.width / (float)Screen.height;

            float escalado = aspectRatioIni / aspectRatio;

            if (escalado < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = escalado;
                rect.x = 0;
                rect.y = (1.0f - escalado) / 2.0f;

                camera.rect = rect;
            }
            else
            {
                float scalewidth = 1.0f / escalado;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
            }
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
