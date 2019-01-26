using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Follow : StateMachineBehaviour
{
    FriendlyFox fox;
    Movement player;

    public float jumpStrength;
    public float startingSpeed;
    public float speed;
    
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fox = FindObjectOfType<FriendlyFox>();
        player = FindObjectOfType<Movement>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Mathf.Abs(player.transform.position.x-fox.transform.position.x)>1)
        {
            if (fox.grounded)
            {
                if (fox.gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
                {
                    fox.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(fox.gameObject.GetComponent<Rigidbody2D>().velocity.x + startingSpeed *
                       ((player.transform.position.x - fox.transform.position.x) / Mathf.Abs(player.transform.position.x - fox.transform.position.x)), 0));
                }
                fox.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed *
                    ((player.transform.position.x - fox.transform.position.x) / (Mathf.Abs(player.transform.position.x - fox.transform.position.x))));
            }
        }
        else
        {
            animator.SetTrigger("RunAway");
        }
        if (Mathf.Abs(player.transform.position.y - fox.transform.position.y) > 1 && fox.grounded)
        {
            fox.gameObject.GetComponent<Rigidbody2D>().AddForce(jumpStrength * Vector2.up);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
