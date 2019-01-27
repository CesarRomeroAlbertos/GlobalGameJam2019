using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hunt : MonoBehaviour
    {
        private SpriteRenderer spr;

        private LayerMask layer;

        private Movement mov;

        private GameObject basicLeaf;

        private Animator animator;

        public bool hunted;

        // Start is called before the first frame update
        void Start()
        {
            spr = GetComponent<SpriteRenderer>();
            layer = LayerMask.GetMask("Leaf");
            mov = GetComponent<Movement>();
            animator = GetComponent<Animator>();
            hunted = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!spr.flipX) {
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x + (spr.size.x / 2), transform.position.y + (spr.size.y / 2)), 0.75f, layer) && !mov.grounded)
                {
                    hunted = true;
                }
            }
            else
            {
                if (Physics2D.OverlapCircle(new Vector2(transform.position.x - (spr.size.x / 2), transform.position.y + (spr.size.y / 2)), 0.75f, layer) && !mov.grounded)
                {
                    hunted = true;
                }
            }

            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Hunt"))
            {

            }
        }
    }
}
