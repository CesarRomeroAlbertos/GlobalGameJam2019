using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AbstractClasses
{
    public class SnowDrift : InteractableObject
    {
        public ParticleSystem splash;
        private Movement player;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        override
        public void Interact()
        {
            player = FindObjectOfType<Movement>();
            player.busy = true;
            Animator anim = player.GetComponent<Animator>();
            anim.SetBool("drink", false);
            anim.SetBool("dig", true);
            anim.SetBool("sniff", true);

            player.soundManager.play("escarbar");
            anim.SetBool("hunted", false);

            Instantiate(splash, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
