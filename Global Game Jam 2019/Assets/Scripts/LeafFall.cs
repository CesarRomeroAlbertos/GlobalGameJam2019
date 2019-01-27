using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LeafFall : MonoBehaviour
    {
        public LeafSpawner spawner;

        public bool falling;

        public GameObject target;

        // Start is called before the first frame update
        void Start()
        {
            transform.Rotate(0, 0, Random.RandomRange(-20, 20));
            spawner = FindObjectOfType<LeafSpawner>();
            falling = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (falling && !target)
            {
                if (transform.position.y > -2.5)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                }
                else if (transform.position.y < -2.5f && spawner.leafFloorCount < spawner.nLeafs)
                {
                    GetComponentInChildren<Animator>().enabled = false;
                    falling = false;
                    spawner.leafFloorCount++;
                }
                else if (transform.position.y < -2.5f && spawner.leafFloorCount >= spawner.nLeafs)
                {
                    spawner.removeLeaf(gameObject);
                    Destroy(gameObject);
                }
            }
            else if (target)
            {
                GetComponentInChildren<Animator>().enabled = false;
                spawner.removeLeaf(gameObject);
                Destroy(gameObject);
                target.GetComponent<Animator>().SetBool("hunted", true);
                target.GetComponent<Movement>().busy = true;
            }
        }
    }
}
