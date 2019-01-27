using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AbstractClasses
{
    public class SnowDrift : InteractableObject
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
            Animator anim = player.GetComponent<Animator>();
            anim.SetBool("sniff", true);
        }
    }
}
