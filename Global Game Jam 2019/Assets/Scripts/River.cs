using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AbstractClasses
{
    public class River : InteractableObject
    {
        private Movement player;
        AudioSource source;

        // Start is called before the first frame update
        void Start()
        {
            source = this.GetComponent<AudioSource>();
            player = FindObjectOfType<Movement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 20)
            {
                if (!source.isPlaying)
                    source.Play();
            }
            else
                source.Stop();
            

        }

        override
        public void Interact()
        {
            player = FindObjectOfType<Movement>();
            player.busy = true;
            Animator anim = player.GetComponent<Animator>();
            anim.SetBool("drink", true);
            anim.SetBool("dig", false);
            anim.SetBool("sniff", false);
            anim.SetBool("hunted", false);

        }
    }
}

