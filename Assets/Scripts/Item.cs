using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //public ParticleSystem sparkle;
    Vector3 pos;
    Rigidbody rb;
    public LayerMask whatIsGround;
    bool floor = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /* sparkle.Stop();
        sparkle.Clear();
        sparkle.Play();
        sparkle.enableEmission = true; */
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, 50f, whatIsGround))
            rb.velocity = new Vector3(0, -10, 0);
        if (Physics.Raycast(transform.position, -transform.up, 5f, whatIsGround))
            rb.velocity = new Vector3(0, -1, 0);
        if (Physics.Raycast(transform.position, -transform.up, 0.5f, whatIsGround))
            rb.velocity = new Vector3(0, -0.1f, 0);
        if (floor)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        //sparkle.Play();
        if (Time.timeScale != 0f)
        {
            this.transform.Rotate(1, 1, 1);

            //gameObject.transform.position = new Vector3(pos.x, pos.y + .24f * Mathf.Sin(Time.fixedTime), pos.z);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            floor = true;
        }
    }

}
