using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    public bool shieldUp = false;
    public float shieldCooldown;
    public float shieldTimer;
    public GameObject shieldprefab, projectile;
    public GameObject bossBar;
    public bool bossFight = false;
    public float hp;
    public float maxhp;
    public Transform player;
    bool alreadyAttacked;
    public float timeBetweenAttacks;
    public TransportGod transition;
    public AudioSource darksouls;
    public LayerMask playermask;
    public bool dropCows = false;
    public float cowDropTimer;
    public float cowDrop;
    public float cowDuration;
    
    public GameObject cowprefab;
    Vector3 RelativePos;
    // Start is called before the first frame update
    void Start()
    {
        cowDropTimer = cowDrop;
        hp = maxhp;
        shieldTimer = shieldCooldown;

    }

    // Update is called once per frame
    void Update()
    {
        if (!bossFight && Physics.CheckSphere(transform.position, 3000, playermask) && transition.done)
        {
            bossFight = true;
            bossBar.SetActive(true);

            darksouls.Play();
        }
        if (bossFight)
        {
            cowDropTimer -= Time.deltaTime;
            cowDuration -= Time.deltaTime;
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0 && !shieldUp)
            {

                shieldUp = true;
                Instantiate(shieldprefab, transform.position , Quaternion.identity);
            }
            transform.LookAt(player.transform.position);
            if (!alreadyAttacked && Time.timeScale != 0f)
            {

                ///Attack code here
                Rigidbody rb = Instantiate(projectile, transform.position + transform.forward * 10f, this.transform.rotation).GetComponent<Rigidbody>();
                RelativePos = player.transform.position - (transform.position + transform.forward * 10);
                rb.velocity = (RelativePos.normalized) * 20f;
                ///End of attack code

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), Random.Range(timeBetweenAttacks - 1f, timeBetweenAttacks + 1f));
            }
            if(cowDropTimer<=0 && !dropCows)
            {
                dropCows = true;
                cowDuration = 10f;
            }
            if (dropCows)
            {
                Instantiate(cowprefab, player.transform.position + new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0));
                if (cowDuration <= 0)
                {
                    cowDropTimer = cowDrop;
                    dropCows = false;
                }
            }
        }


    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
