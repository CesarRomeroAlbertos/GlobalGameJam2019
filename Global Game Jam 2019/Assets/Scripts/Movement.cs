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
        public float maxSpeed;
        public float maxJumpSpeed;


        InteractableObject collidingObject;
        Rigidbody2D rigidBody;
        Animator anim;
        public FoxSoundManager soundManager;
        public ParticleSystem splash;

        public Collider2D groundCollider;

        public bool grounded;

        Vector3 lastPosition;
        public float currentSpeed;

        public GameObject head1;
        public GameObject head2;

        public bool busy;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            collidingObject = null;
            grounded = false;
            anim = GetComponent<Animator>();
            soundManager = GetComponent<FoxSoundManager>();
            lastPosition = transform.position;
            currentSpeed = 0;

            head1.SetActive(true);
            head2.SetActive(false);

            busy = false;
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(rigidBody.velocity.x) >= maxSpeed)
            {
                if (rigidBody.velocity.x < 0) rigidBody.velocity = new Vector2(Mathf.Lerp(-maxSpeed, rigidBody.velocity.x, 0.5f), rigidBody.velocity.y);
                else rigidBody.velocity = new Vector2(Mathf.Lerp(maxSpeed, rigidBody.velocity.x,0.5f), rigidBody.velocity.y);
            }
        }

        // Update is called once per frame
        void Update()
        {

                //horizontal movement
                if (Input.GetAxis("Horizontal") != 0 && !busy)
                {
                    if (grounded)
                    {
                    if(!soundManager.source.isPlaying)
                    {
                        soundManager.play("andar");
                    }
                        if (rigidBody.velocity.x == 0)
                        {
                            rigidBody.AddForce(new Vector2(rigidBody.velocity.x + startingSpeed * Input.GetAxis("Horizontal"), 0));
                        }
                        //transform.Translate(new Vector3(speed * Input.GetAxis("Horizontal"), 0, 0));
                        rigidBody.AddForce(Vector2.right * speed * Input.GetAxis("Horizontal"));
                    }
                    else
                    {
                        rigidBody.AddForce(Vector2.right * (speed) * Input.GetAxis("Horizontal"));
                        if (Mathf.Abs(rigidBody.velocity.x) >= maxJumpSpeed)
                        {
                            if (rigidBody.velocity.x < 0) rigidBody.velocity = new Vector2(Mathf.Lerp(-maxJumpSpeed, rigidBody.velocity.x, 0.5f), rigidBody.velocity.y);
                            else rigidBody.velocity = new Vector2(Mathf.Lerp(maxJumpSpeed, rigidBody.velocity.x, 0.5f), rigidBody.velocity.y);
                        }
                }
                }
                else if (!busy)
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
                if (Input.GetButtonDown("Jump") && grounded && !busy)
                {
                    rigidBody.AddForce(jumpStrength * Vector2.up);
                
                    //grounded = false;
                }

                //interaction
                if (Input.GetButtonUp("Interact") && !busy)
                {
                    if (collidingObject != null)
                        collidingObject.Interact();
                    else if (grounded)
                    {
                        anim.SetBool("sniff", true);
                        busy = true;
                    }
                }

                if (!grounded && rigidBody.velocity.y <= 0 && !busy)
                {
                    rigidBody.AddForce(new Vector2(0, Physics.gravity.y * rigidBody.mass * 3));
                }

                anim.SetFloat("velocity", rigidBody.velocity.x);
                anim.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
                anim.SetBool("grounded", grounded);
                anim.SetFloat("verticalSpeed", rigidBody.velocity.y);
                if (Mathf.Abs(rigidBody.velocity.x) > 2) anim.SetBool("running", true);
                else anim.SetBool("running", false);
                if (Mathf.Abs(rigidBody.velocity.x) > 0.1 && !busy)
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

            currentSpeed = transform.position.x - lastPosition.x;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sniff"))
            {
                anim.SetBool("sniff", false);
                busy = false;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dig"))
            {
                anim.SetBool("dig", false);
                busy = false;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hunt"))
            {
                anim.SetBool("hunted", false);
                busy = false;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Drink"))
            {
                anim.SetBool("drink", false);
                busy = false;
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
                soundManager.play("salto");
                grounded = true;
                if(!GetComponent<SpriteRenderer>().flipX)
                    Instantiate(splash, transform.position - new Vector3(-1, 0.5f, 0), Quaternion.identity, this.transform);
                else
                    Instantiate(splash, transform.position - new Vector3(1, 0.5f, 0), Quaternion.identity, this.transform);
            }
            else if (collision.CompareTag("Leaf") && !grounded)
            {
                collision.gameObject.GetComponentInParent<LeafFall>().target = gameObject;
                
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

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Floor"))
                grounded = true;
        }
    }
}