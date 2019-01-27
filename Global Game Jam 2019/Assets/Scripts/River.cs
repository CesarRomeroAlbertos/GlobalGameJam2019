using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AbstractClasses
{
    public class River : InteractableObject
    {
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
            anim.SetBool("drink", true);
            anim.SetBool("dig", false);
            anim.SetBool("sniff", false);
            anim.SetBool("hunted", false);

        }
    }
}

