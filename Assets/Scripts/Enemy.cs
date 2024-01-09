
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using System.Collections.Generic;

public class Enemy : MonoBehaviour
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
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public NoisePlay  noise;
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
        if (Physics.Raycast(this.transform.position, direction, out hit, range)){
            
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
        playerInSightRange = ICanSee(sightRange)||Physics.CheckSphere(transform.position, 0.5f*sightRange, whatIsPlayer) ;
        playerInAttackRange = ICanSee(attackRange) || Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if (!playerInSightRange && !playerInAttackRange)
        {
            //Debug.Log("Patrol");
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
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
        if (!walkPointSet)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("SprintJump", false);
            animator.SetBool("SprintSlide", false);
            SearchWalkPoint();
        }

        if (walkPointSet)
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

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        RaycastHit hit;
        if (Physics.Raycast(walkPoint, -transform.up, out hit, 2f))
        {
            Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == 3)
            {
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
    Vector2 relativePosition;
    float d;
    public float setSpeed=50f;
    float yspeed;
    float hangtime;
    float sinangle;
    float cosangle;
    float dy;
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move

        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", false);
        animator.SetBool("SprintSlide", false);
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        
        dy = player.transform.position.y - transform.position.y+0.5f;
        relativePosition = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z);
        d = Mathf.Sqrt(Mathf.Pow(relativePosition.x,2)+Mathf.Pow(relativePosition.y,2));
        hangtime = d / setSpeed;
        sinangle = relativePosition.x / d;
        cosangle = relativePosition.y / d;

        yspeed = (4.9f * Mathf.Pow(hangtime, 2) + dy) / hangtime;
        
        if (!alreadyAttacked && Time.timeScale != 0f)
        {
     
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position+transform.forward*0.5f+transform.up*0.3f, Quaternion.identity).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(setSpeed*sinangle, yspeed, setSpeed*cosangle);
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
