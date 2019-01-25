using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AbstractClasses;

namespace Assets.Scripts
{
    public class Movement : MonoBehaviour
    {

        public float speed;
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
                transform.Translate(new Vector3(speed * Input.GetAxis("Horizontal"), 0, 0));
                //rigidBody.AddForce(Vector2.right*speed* Input.GetAxis("Horizontal"));
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
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Interactable"))
                collidingObject = null;
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Interactable"))
                collidingObject = collision.gameObject.GetComponent<InteractableObject>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Floor"))
                grounded = true;
        }
    }
}
