using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    int count = 2*(int)Difficult.slideVal;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {

            while (count > 0)
            {
                count--;
                Instantiate(enemy, transform.position - transform.up * 0.5f, Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
