
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using System.Collections.Generic;

public class cow : MonoBehaviour
{


    public Transform player;
    public Rigidbody playerrb;

    public Vector3 go;

    public float cowTimer = 0f;
    public float cowrainTimer;

    public Rigidbody rb;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject bossBar;
    bool isHit = false;
    public float maxhp;
    //States
    public Vector3 CowDir;
    public AudioSource noise;

    public float Timer = 3;
    //public static int look=0;
    //public bool looking = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hp = maxhp;
        playerrb = GameObject.Find("Player").GetComponent<Rigidbody>();

        //if (Difficult.slideVal == 4)
        //{
        //  GetComponent<NavMeshAgent>().speed = 12f;
        //}


    }
    public float hp = 5;


    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (hp != maxhp && Timer > 0)
        {
            Timer -= Time.deltaTime;
            transform.localScale *= (1+Time.deltaTime*0.5f);
        }
        if(hp!=maxhp && !isHit)
        {
            rb.velocity = new Vector3(0, 6, 0);
            isHit = true;
            bossBar.SetActive(true);
            noise.Play();
        }
        if (hp != maxhp && Timer<=0)
        {

            rb.velocity = Vector3.zero;
            //Check for sight and attack range



            cowrainTimer -= Time.deltaTime;
                AttackPlayer();
                //Debug.Log("Atk");
            
        }
    }
    

    
   
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move


        if (cowrainTimer <= 0)
        {
            cowrainTimer = cowTimer;
            Rigidbody rb= Instantiate(projectile, transform.position + new Vector3(Random.Range(-300, 300), 50f, Random.Range(-300, 300)), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, -20, 0);

            rb = Instantiate(projectile, transform.position + new Vector3(Random.Range(-300, 300), 50f, Random.Range(-300, 300)), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, -20, 0);
            rb = Instantiate(projectile, transform.position + new Vector3(Random.Range(-300, 300), 50f, Random.Range(-300, 300)), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, -20, 0);
            rb = Instantiate(projectile, transform.position + new Vector3(Random.Range(-300, 300), 50f, Random.Range(-300, 300)), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, -20, 0);
            rb = Instantiate(projectile, transform.position + new Vector3(Random.Range(-300, 300), 50f, Random.Range(-300, 300)), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, -20, 0);
        }

        //transform.LookAt(player);
        CowDir = new Vector3(player.transform.position.x, 120f, player.transform.position.z);
        go = Vector3.Slerp(transform.position, CowDir, Time.deltaTime * 0.1f);
        transform.rotation = Quaternion.LookRotation(player.transform.position);

        transform.position = go;


        if (!alreadyAttacked && Time.timeScale != 0f)
        {

            ///Attack code here
            Rigidbody rb = Instantiate(projectile, player.transform.position + new Vector3(0,30,0), Quaternion.Euler(0,0,0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0,-40,0);
            rb = Instantiate(projectile, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(20, -40, 0);
            rb = Instantiate(projectile, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(-20, -40, 0);
            rb = Instantiate(projectile, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, -40, 20);
            rb = Instantiate(projectile, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, -40, -20);
            rb = Instantiate(projectile, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(20*(Mathf.Sqrt(2)/2.0f), -40, 20*(Mathf.Sqrt(2) / 2.0f));
            rb = Instantiate(projectile, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(-20 * (Mathf.Sqrt(2) / 2.0f), -40, 20 * (Mathf.Sqrt(2) / 2.0f));
            rb = Instantiate(projectile, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(20 * (Mathf.Sqrt(2) / 2.0f), -40, -20 * (Mathf.Sqrt(2) / 2.0f));
            rb = Instantiate(projectile, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody>();
            rb.velocity = new Vector3(-20 * (Mathf.Sqrt(2) / 2.0f), -40, -20 * (Mathf.Sqrt(2) / 2.0f));
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), Random.Range(timeBetweenAttacks-0.2f,timeBetweenAttacks+0.2f));
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }




}
