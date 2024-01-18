
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using System.Collections.Generic;

public class RangedEnemy : MonoBehaviour
{
    public bool patroller;
    public NavMeshAgent agent;

    public Transform player;
    public Rigidbody playerrb;

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
    public float sightRange;
    public bool playerInSightRange;
    public NoisePlay noise;
    public GameObject mesh;
    //public static int look=0;
    //public bool looking = false;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerrb = GameObject.Find("Player").GetComponent<Rigidbody>();
 
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


        if (!playerInSightRange && patroller)
        {
            //Debug.Log("Patrol");
            Patroling();
        }

        if (playerInSightRange)
        {
            AttackPlayer();
            //Debug.Log("Atk");
        }
    }
    private void Patroling()
    {
        if (!walkPointSet || !agent.hasPath)
        {

            SearchWalkPoint();
        }

        if (walkPointSet && agent.hasPath)
        {

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
            
            if (hit.collider.gameObject.layer == 3)
            {
                agent.path = path;
                walkPointSet = true;
            }

        }
    }


    Vector2 relativePosition;
    float d;
    public float setSpeed = 50f;
    float yspeed;
    float hangtime;
    float sinangle;
    float cosangle;
    float dy;
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move


        agent.SetDestination(transform.position);

        transform.LookAt(new Vector3(player.position.x,transform.position.y,player.position.z));

        dy = player.transform.position.y - transform.position.y ;
        relativePosition = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z);
        d = Mathf.Sqrt(Mathf.Pow(relativePosition.x, 2) + Mathf.Pow(relativePosition.y, 2));
        hangtime = d / setSpeed;
        sinangle = relativePosition.x / d;
        cosangle = relativePosition.y / d;

        yspeed = (4.9f * Mathf.Pow(hangtime, 2) + dy) / hangtime;

        if (!alreadyAttacked && Time.timeScale != 0f)
        {

            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position + transform.forward * 0.6f + transform.up * 1f, this.transform.rotation).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(setSpeed * sinangle, yspeed, setSpeed * cosangle);
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
