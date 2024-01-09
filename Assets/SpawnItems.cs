using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bun, candy, cheese, lettuce, onion, patty, pickles, sauce, tomato;
    float maxtimer = 30f;
    float timer;
    void Start()
    {
        Instantiate(bun, transform.position + new Vector3(Random.Range(-450, 450), 0, Random.Range(-450, 450)), transform.rotation);
        Instantiate(cheese, transform.position + new Vector3(Random.Range(-450, 450), 0, Random.Range(-450, 450)), transform.rotation);
        Instantiate(lettuce, transform.position + new Vector3(Random.Range(-450, 450), 0, Random.Range(-450, 450)), transform.rotation);
        Instantiate(onion, transform.position + new Vector3(Random.Range(-450, 450), 0, Random.Range(-450, 450)), transform.rotation);
        Instantiate(patty, transform.position + new Vector3(Random.Range(-450, 450), 0, Random.Range(-450, 450)), transform.rotation);
        Instantiate(pickles, transform.position + new Vector3(Random.Range(-450, 450), 0, Random.Range(-450, 450)), transform.rotation);
        Instantiate(sauce, transform.position + new Vector3(Random.Range(-450, 450), 0, Random.Range(-450, 450)), transform.rotation);
        Instantiate(tomato, transform.position + new Vector3(Random.Range(-450, 450), 0, Random.Range(-450, 450)), transform.rotation);

        maxtimer -= Difficult.slideVal * 2.5f;
        timer = maxtimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = maxtimer;
            Instantiate(candy, transform.position+new Vector3(Random.Range(-500,500),0,Random.Range(-500,500)),transform.rotation);
        }
    }
}
