using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldCollide : MonoBehaviour
{
    public float shieldhp;
    public Gandolf storage;
    // Start is called before the first frame update
    void Start()
    {
        storage = GameObject.Find("Old_Feeble_man").GetComponent<Gandolf>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("hit");
            shieldhp--;
            if (shieldhp <= 0)
            {
                storage.shieldTimer = storage.shieldCooldown;
                storage.shieldUp = false;
                Destroy(this.gameObject);
            }
        }
    }
}
