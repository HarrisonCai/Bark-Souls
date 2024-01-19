using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

using Unity.AI.Navigation;
public class Gandolf : MonoBehaviour
{
    public bool shieldUp = false;
    public float shieldCooldown;
    public float shieldTimer;
    public GameObject shieldprefab,projectile;
    public GameObject bossBar;
    public bool bossFight = false;
    public float hp;
    public float maxhp;
    public Transform player;
    bool alreadyAttacked;
    public float timeBetweenAttacks;
    public GameObject TheWalls;
    public AudioSource darksouls;
    public LayerMask playermask;
    public GameObject transition;
    Vector3 RelativePos;
    
    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        shieldTimer = shieldCooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossFight && Physics.CheckSphere(transform.position,75, playermask))
        {
            bossFight = true;
            bossBar.SetActive(true);
            TheWalls.SetActive(true);
      
            darksouls.Play();
        }
        if (bossFight)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0 && !shieldUp)
            {

                shieldUp = true;
                Instantiate(shieldprefab, transform.position + new Vector3(-0.480011f, -1.5f, -0.25f), Quaternion.identity);
            }
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            if (!alreadyAttacked && Time.timeScale != 0f)
            {

                ///Attack code here
                Rigidbody rb = Instantiate(projectile, transform.position + transform.forward * 15 + transform.up * 1f, this.transform.rotation).GetComponent<Rigidbody>();
                RelativePos = player.transform.position - (transform.position+transform.forward*15);
                rb.velocity = (RelativePos.normalized)*20f;
                ///End of attack code

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), Random.Range(timeBetweenAttacks - 1f, timeBetweenAttacks + 1f));
            }
        }
        
 
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
