using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class OpponentScript : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;
    public Transform groundCheck;
   public GameObject target;
    Transform player;

    public LayerMask groundMask;
    Vector3 toPlayer;
    Vector3 lookHere;
    Vector3 destinationOffset = Vector3.zero;

    public float nearPlayerSpeed = 0.5f;
    public float awayPlayerSpeed = 1.8f;
    public float groundDistance = 0.4f;
    float distToPlayer;
    
    

    bool isGrounded;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        player = target.transform;
        
    }


    void Update()
    {
        
        computeAgentSpeed();
        CheckGrounded();
        UpdateAnimatorParameters();
        if (gameObject.GetComponent<HealthStatus>().currentHP <= 0)
        {
            anim.SetBool("isDead", true);
            agent.isStopped = true;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("die"))
                Destroy(gameObject, 2);
        }


        if (distToPlayer < 50)
        {
            if (!isGrounded)
            {
                agent.isStopped = true; ;
                return; }

            agent.SetDestination(target.transform.position + destinationOffset);

            lookHere = new Vector3(target.transform.position.x, transform.position.y, 
                                     target.transform.position.z);

            transform.LookAt(lookHere);

        }
        else anim.SetBool("isWalking", false);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }


    void computeAgentSpeed()
    {
        agent.isStopped = false;
        toPlayer = target.transform.position - transform.position;
        distToPlayer = toPlayer.magnitude;
        float changeSpeedFactor = Mathf.Clamp01((distToPlayer - 5f) / 5f);
        agent.speed = Mathf.Lerp(nearPlayerSpeed, awayPlayerSpeed, changeSpeedFactor);

        if (!isGrounded)
            agent.speed = 2f;
    }




    void UpdateAnimatorParameters()
    {
        if (!isGrounded)
            anim.SetBool("isGrounded", false);
        else anim.SetBool("isGrounded", true);

        if (distToPlayer <= 6.2)
        {
            anim.SetBool("isWalking", false);
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("1"))
                anim.SetBool("isAttacking", true);

        }
        else
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }

    }
}
