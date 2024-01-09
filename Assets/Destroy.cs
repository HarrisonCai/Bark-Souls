using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    SimpleSampleCharacterControl ch;
    void Start()
    {
        ch = GameObject.Find("Player").GetComponent<SimpleSampleCharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomato"))
        {
            collision.gameObject.SetActive(false);
            ObjectCollection.ingredients[0] = true;

        }
        else if (collision.gameObject.CompareTag("Bun"))
        {
            collision.gameObject.SetActive(false);
            ObjectCollection.ingredients[1] = true;

        }
        else if (collision.gameObject.CompareTag("Patty"))
        {
            collision.gameObject.SetActive(false);
            ObjectCollection.ingredients[2] = true;

        }
        else if (collision.gameObject.CompareTag("Lettuce"))
        {
            collision.gameObject.SetActive(false);
            ObjectCollection.ingredients[3] = true;

        }
        else if (collision.gameObject.CompareTag("Cheese"))
        {
            collision.gameObject.SetActive(false);
            ObjectCollection.ingredients[4] = true;

        }
        else if (collision.gameObject.CompareTag("Sauce"))
        {
            collision.gameObject.SetActive(false);
            ObjectCollection.ingredients[5] = true;

        }
        else if (collision.gameObject.CompareTag("Onion"))
        {
            collision.gameObject.SetActive(false);
            ObjectCollection.ingredients[6] = true;

        }
        else if (collision.gameObject.CompareTag("Pickle"))
        {
            collision.gameObject.SetActive(false);
            ObjectCollection.ingredients[7] = true;

        }else if (collision.gameObject.CompareTag("Car"))
        {
            collision.gameObject.SetActive(false);
            ch.nCar = false;
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
        }

    }
}
