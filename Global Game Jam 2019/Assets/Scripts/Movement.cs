using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AbstractClasses;

namespace Assets.Scripts
{
    public class Movement : MonoBehaviour
    {

        public float speed;
        public float startingSpeed;
        public float jumpStrength;
        

        InteractableObject collidingObject;
        Rigidbody2D rigidBody;
        public Collider2D groundCollider;

        bool grounded;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            collidingObject = null;
            grounded = true;
        }

        // Update is called once per frame
        void Update()
        {
            //horizontal movement
            if (Input.GetAxis("Horizontal") != 0)
            {
                if(rigidBody.velocity.x==0)
                {
                    rigidBody.AddForce(new Vector2(rigidBody.velocity.x + startingSpeed* Input.GetAxis("Horizontal"), 0));
                }
                //transform.Translate(new Vector3(speed * Input.GetAxis("Horizontal"), 0, 0));
                rigidBody.AddForce(Vector2.right*speed* Input.GetAxis("Horizontal"));
            }
            else
            {
                if (grounded)
                {
                    if (rigidBody.velocity.x > 0)
                    {
                        rigidBody.velocity = new Vector2(Mathf.Lerp(rigidBody.velocity.x, 0, 1 / rigidBody.velocity.x*0.75f), rigidBody.velocity.y);
                    }
                    else
                        rigidBody.velocity = Vector2.up * rigidBody.velocity.y;
                }
            }

            //jump
            if (Input.GetButtonDown("Jump") && grounded)
            {
                rigidBody.AddForce(jumpStrength * Vector2.up);
                grounded = false;
            }

            //interaction
            if (Input.GetButtonUp("Interact"))
            {
                if (collidingObject != null)
                    collidingObject.Interact();
            }
        }

        /// <summary>
        /// When the character enters the trigger of an interactable 
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Interactable"))
                collidingObject = null;
            else if (collision.CompareTag("Floor"))
                grounded = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Interactable"))
                collidingObject = collision.gameObject.GetComponent<InteractableObject>();
            else if (collision.CompareTag("Floor"))
                grounded = false;
        }
    }
}
