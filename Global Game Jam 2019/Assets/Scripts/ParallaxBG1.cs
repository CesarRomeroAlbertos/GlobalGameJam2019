using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ParallaxBG1 : MonoBehaviour
    {
        public float parallax;

        private Movement player;

        // Start is called before the first frame update
        void Start()
        {
            switch (gameObject.layer)
            {
                case 9: parallax = 5;
                    break;
                case 10: parallax = 8;
                    break;
                case 11: parallax = 11;
                    break;
                case 12: parallax = 0.8f;
                    break;
            }

            player = FindObjectOfType<Movement>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x + (player.currentSpeed / parallax), transform.position.y, transform.position.z);
        }
    }
}
