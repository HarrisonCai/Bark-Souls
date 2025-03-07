using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroy : MonoBehaviour
{
    public bool noExplode;
    public string obj;
    public string enemy;
    public float damage = 4;
    public MovementController store;
    public Rigidbody rb;
    bool parry = false;
    public Transform camera;
    public float Timer;
    bool explode = false;
    public GameObject mainExplode;
    public gameoverscreen win;
    private void Awake()
    {
        store = GameObject.Find("Player").GetComponent<MovementController>();
        rb = GetComponent<Rigidbody>();
        camera = Camera.main.transform;
        win = GameObject.Find("GameEnd").GetComponent<gameoverscreen>();
    }
    private void Update()
    {
        if (!noExplode)
        {
            Timer += Time.deltaTime;
            if (Timer > 30)
            {
                Destroy(this.gameObject);
            }
        }
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obj)|| collision.gameObject.CompareTag("Projectile"))
        {
            
        }
        else
        {
            if (collision.gameObject.CompareTag(enemy))
            {
                if (enemy.Equals("Enemy"))
                {
                    if (collision.gameObject.GetComponent<Enemy>() != null)
                    {
                        collision.gameObject.GetComponent<Enemy>().hp -= (damage * 0.5f);

                        if (collision.gameObject.GetComponent<Enemy>().hp <= 0)
                        {
                            store.points += 2;
                            Destroy(collision.gameObject);
                        }
                    }
                    if (collision.gameObject.GetComponent<RangedEnemy>() != null)
                    {
                        collision.gameObject.GetComponent<RangedEnemy>().hp -= (damage * 0.5f);

                        if (collision.gameObject.GetComponent<RangedEnemy>().hp <= 0)
                        {
                            store.points += 2;
                            Destroy(collision.gameObject);
                        }
                    }
                    if (collision.gameObject.GetComponent<MeleeEnemy>() != null)
                    {
                        collision.gameObject.GetComponent<MeleeEnemy>().hp -= (damage * 0.5f);

                        if (collision.gameObject.GetComponent<MeleeEnemy>().hp <= 0)
                        {
                            store.points += 2;
                            Destroy(collision.gameObject);
                        }
                    }
                    if (collision.gameObject.GetComponent<cow>() != null)
                    {
                        collision.gameObject.GetComponent<cow>().hp -= (damage * 0.5f);

                        if (collision.gameObject.GetComponent<cow>().hp <= 0)
                        {
                            Destroy(collision.gameObject);
                        }
                    }
                    if (collision.gameObject.GetComponent<Gandolf>() != null)
                    {
                        collision.gameObject.GetComponent<Gandolf>().hp -= (damage * 0.5f);

                        if (collision.gameObject.GetComponent<Gandolf>().hp <= 0)
                        {
                            store.points += 10;
                            collision.gameObject.GetComponent<Gandolf>().transition.SetActive(true);
                            Destroy(collision.gameObject);
                        }
                    }
                    if (collision.gameObject.GetComponent<God>() != null    )
                    {
                        collision.gameObject.GetComponent<God>().hp -= damage;
                        Debug.Log("enter");
                        if (collision.gameObject.GetComponent<God>().hp <= 0)
                        {
                            
                            Destroy(collision.gameObject);
                            win.winCon = true;
                        }
                    }
                }
                if (enemy.Equals("Player"))
                {
                    if (store.parryState && store.perfectparryattempt)
                    {
                        
                        store.parryHit = true;

                        transform.position = camera.position+camera.forward;
                        rb.velocity = camera.forward * Vector3.Magnitude(rb.velocity)*0.7f;
                 
                        parry = true;
                        obj = "Player";
                        enemy = "Enemy";
                    }
                    else if (store.parryState && !store.perfectparryattempt)
                    {
                        collision.gameObject.GetComponent<MovementController>().Health -= (0.25f * damage);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<MovementController>().Health -= damage;
                    }
                    if (collision.gameObject.GetComponent<MovementController>().Health <= 0)
                    {

                        collision.gameObject.GetComponent<MovementController>().Health = 0;
                        //Destroy(collision.gameObject);
                    }
                }
                
            }
            if (!parry)
            {
                if (!noExplode&& !explode)
                {
                    Debug.Log("among");
                    Instantiate(mainExplode, transform.position , Quaternion.Euler(0, 0, 0));
                    explode = true;
                    this.gameObject.SetActive(false);
                    Destroy(this.gameObject);

                }
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
                
            }
            parry = false;
        }
    }
 
}
