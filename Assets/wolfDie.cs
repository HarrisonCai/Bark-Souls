using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfDie : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(death), 5);
    }
    private void death()
    {
        rb.AddForce(new Vector3(2, 50, -20), ForceMode.Impulse);
    }
    // Update is called once per frame
    
}
