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
        Animator anim;
        public ParticleSystem splash;

        public Collider2D groundCollider;

        public bool grounded;

        Vector3 lastPosition;
        public float currentSpeed;

        public GameObject head1;
        public GameObject head2;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            collidingObject = null;
            grounded = true;
            anim = GetComponent<Animator>();
            lastPosition = transform.position;
            currentSpeed = 0;

            head1.SetActive(true);
            head2.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

            
            //horizontal movement
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (grounded)
                {
                    if (rigidBody.velocity.x == 0)
                    {
                        rigidBody.AddForce(new Vector2(rigidBody.velocity.x + startingSpeed * Input.GetAxis("Horizontal"), 0));
                    }
                    //transform.Translate(new Vector3(speed * Input.GetAxis("Horizontal"), 0, 0));
                    rigidBody.AddForce(Vector2.right * speed * Input.GetAxis("Horizontal"));
                }
                else
                {
                    rigidBody.AddForce(Vector2.right * (speed/3) * Input.GetAxis("Horizontal"));
                }
            }
            else
            {
                if (grounded)
                {
                    if (Mathf.Abs(rigidBody.velocity.x) > 0)
                    {
                        rigidBody.velocity = new Vector2(Mathf.Lerp(rigidBody.velocity.x, 0, 1 / Mathf.Abs(rigidBody.velocity.x) * 0.75f), rigidBody.velocity.y);
                    }
                    else
                        rigidBody.velocity = Vector2.up * rigidBody.velocity.y;
                }
            }

            //jump
            if (Input.GetButtonDown("Jump") && grounded)
            {
                rigidBody.AddForce(jumpStrength * Vector2.up);
                //grounded = false;
            }

            //interaction
            if (Input.GetButtonUp("Interact"))
            {
                if (collidingObject != null)
                    collidingObject.Interact();
            }

            if(!grounded && rigidBody.velocity.y<=0)
            {
                rigidBody.AddForce(new Vector2(0,Physics.gravity.y * rigidBody.mass*3));
            }

            currentSpeed = transform.position.x - lastPosition.x;

            anim.SetFloat("velocity", rigidBody.velocity.x);
            anim.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
            anim.SetBool("grounded", grounded);
            anim.SetFloat("verticalSpeed", rigidBody.velocity.y);
            if (Mathf.Abs(rigidBody.velocity.x) > 2) anim.SetBool("running", true); 
            else anim.SetBool("running", false);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sniff")) anim.SetBool("sniff", false);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hunt")) anim.SetBool("hunted", false);
            if (Mathf.Abs(rigidBody.velocity.x) > 0.1)
            {
                GetComponent<SpriteRenderer>().flipX = rigidBody.velocity.x < 0;
                if (GetComponent<SpriteRenderer>().flipX)
                {
                    head1.SetActive(false);
                    head2.SetActive(true);
                }
                else
                {
                    head1.SetActive(true);
                    head2.SetActive(false);
                }
            }

            lastPosition = transform.position;
        }

        /// <summary>
        /// When the character enters the trigger of an interactable 
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Interactable"))
                collidingObject = collision.gameObject.GetComponent<InteractableObject>();
            else if (collision.CompareTag("Floor"))
            {
                grounded = true;
                if(!GetComponent<SpriteRenderer>().flipX)
                    Instantiate(splash, transform.position - new Vector3(-1, 0.5f, 0), Quaternion.identity, this.transform);
                else
                    Instantiate(splash, transform.position - new Vector3(1, 0.5f, 0), Quaternion.identity, this.transform);
            }
            else if (collision.CompareTag("Leaf") && !grounded)
            {
                Debug.Log("Porfa");
                collision.gameObject.GetComponent<LeafFall>().target = gameObject;
                
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Interactable"))
            {
                collidingObject = null;
            }
            else if (collision.CompareTag("Floor"))
                grounded = false;
        }
    }
}