using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetc : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Transform camera;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    public void ReactToHit()
    {
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        this.transform.Rotate(0, 75, 0);
        rb.AddForce(camera.forward*1000);

        yield return new WaitForSeconds(Mathf.Infinity);
        Destroy(this.gameObject);
    }
}
