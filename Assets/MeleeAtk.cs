using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtk : MonoBehaviour
{

    public float damage;
    public MovementController store;
    public string Enemy;


    private void Awake()
    {
        store = GameObject.Find("Player").GetComponent<MovementController>();


    }
    // Start is called before the first frame update
    private void Update()
    {
        Debug.Log(damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Enemy.Equals("Enemy"))
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (other.gameObject.GetComponent<Enemy>() != null)
                {
                    other.gameObject.GetComponent<Enemy>().hp -= damage;
                    Debug.Log("enter");
                    if (other.gameObject.GetComponent<Enemy>().hp <= 0)
                    {
                        Destroy(other.gameObject);
                    }
                }
                if (other.gameObject.GetComponent<cow>() != null)
                {
                    other.gameObject.GetComponent<cow>().hp -= damage;
                    Debug.Log("enter");
                    if (other.gameObject.GetComponent<cow>().hp <= 0)
                    {
                        Destroy(other.gameObject);
                    }
                }
            }
        }
        if (Enemy.Equals("Player"))
        {
            if (Enemy.Equals("Player"))
            {
                if (store.parryState && store.perfectparryattempt)
                {

                    store.parryHit = true;
                    //rb.velocity = new Vector3(-1.5f*rb.velocity.x,rb.velocity.y,-1.5f*rb.velocity.z);
                    //rb.velocity = camera.forward * 20f;

                }
                else if (store.parryState && !store.perfectparryattempt)
                {
                    other.gameObject.GetComponent<MovementController>().Health -= (0.25f * damage);
                }
                else
                {
                    other.gameObject.GetComponent<MovementController>().Health -= damage;
                }
                if (other.gameObject.GetComponent<MovementController>().Health <= 0)
                {

                    other.gameObject.GetComponent<MovementController>().Health = 0;
                    //Destroy(collision.gameObject);
                }
            }
        }
    }

}
