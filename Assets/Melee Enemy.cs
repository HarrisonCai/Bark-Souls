
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using System.Collections.Generic;

public class MeleeEnemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Rigidbody playerrb;
    public Animator animator;
    public LayerMask whatIsGround, whatIsPlayer;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject MeleeHitbox,glowyWarning;
    public bool isAttacking = false;
    public float Delay=0.5f;
    public float DelayTimer;

    //States
    public float sightRange;
    public float attackRange = 2f;
    public bool playerInSightRange, playerInAttackRange;
    public NoisePlay noise;
    public GameObject mesh;
    //public static int look=0;
    //public bool looking = false;
    private void Awake()
    {

        playerrb = GameObject.Find("Player").GetComponent<Rigidbody>();
        animator = mesh.GetComponent<Animator>();
        //if (Difficult.slideVal == 4)
        //{
        //  GetComponent<NavMeshAgent>().speed = 12f;
        //}

        agent = GetComponent<NavMeshAgent>();
    }
    public float hp = 5;


    float fovAngle = 110f;
    bool ICanSee(float range)
    {
        Vector3 direction = player.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, direction, out hit, range))
        {

            return hit.collider.gameObject.tag.Equals("Player") && angle < fovAngle;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {

        //Check for sight and attack range
        playerInSightRange = ICanSee(sightRange) || Physics.CheckSphere(transform.position, 0.5f * sightRange, whatIsPlayer);
        playerInAttackRange =Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        DelayTimer -= Time.deltaTime;
        if (!playerInSightRange && !playerInAttackRange && !isAttacking)
        {
            //Debug.Log("Patrol");
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange && !isAttacking)
        {
            ChasePlayer();
            //Debug.Log("Chase");
        }
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
            //Debug.Log("Atk");
        }
    }
    private void Patroling()
    {
        if (!walkPointSet || !agent.hasPath)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("SprintJump", false);
            animator.SetBool("SprintSlide", false);
            SearchWalkPoint();
        }

        if (walkPointSet && agent.hasPath)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("SprintJump", false);
            animator.SetBool("SprintSlide", false);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        NavMeshPath path = new NavMeshPath();
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        RaycastHit hit;
        if (Physics.Raycast(walkPoint, -transform.up, out hit, 2f) && NavMesh.CalculatePath(transform.position, walkPoint, NavMesh.AllAreas, path))
        {
            Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == 3)
            {
                agent.path = path;
                walkPointSet = true;
            }

        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", true);
        animator.SetBool("SprintSlide", false);
        agent.SetDestination(player.position);
        walkPoint = player.position;
        walkPointSet = true;

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move

        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", false);
        animator.SetBool("SprintSlide", false);
        agent.SetDestination(transform.position);

        if (!isAttacking)
        {
            transform.LookAt(player);
        }

        

        if (!alreadyAttacked && Time.timeScale != 0f)
        {

            ///Attack code here
            MeleeHitbox.SetActive(true);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), Random.Range(timeBetweenAttacks - 0.2f, timeBetweenAttacks + 0.2f));
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }




}
