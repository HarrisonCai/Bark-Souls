using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollection : MonoBehaviour
{
    [SerializeField] AudioSource itemget;
    [SerializeField] AudioSource candyget;
    //tomato, bun, patty, lettuce, cheese, sauce, onion
    string[] ing = { "Tomato", "Burger Bun", "Patty", "Lettuce", "Cheese", "Sauce", "Onion", "Pickles" };
    public static bool[] ingredients = new bool[8];
    private int candy = 0;
    /* Ingredients:
     * tomato
     * burger bun
     * patty
     * lettuce
     * cheese
     * sauce
     * onion
     * Extra:
     * blue candy (inventory space) (speed bonus if used)
     * 
     */
    bool done = false;
    bool win = false;
    bool inWater = false;
    private float timeLimit = 2100f;
    bool nextToCar = false;

    public int hp=1;
    public PauseMenu pauseState;
    public AudioClip death;
    private void Awake()
    {
        hp = 20-(((int)Difficult.slideVal-1)*6);
        if (Difficult.slideVal == 4)
        {
            hp = 1;
        }
        pauseState = GameObject.Find("Pause").GetComponent<PauseMenu>();
    }
    public float Timer
    {
        get { return timeLimit; }
    }
    public bool Win
    {
        get { return win; }
    }
    public int Health
    {
        get { return hp; }
        set {
            if (hp == 1)
            {
                AudioSource.PlayClipAtPoint(death, transform.position);
            }
            hp = value; }
    }
    public bool Water
    {
        get { return inWater; }
    }
    public bool Cars
    {
        get { return nextToCar; }
        set
        {
            nextToCar = value;
        }
    }
    public SimpleSampleCharacterControl store;
    public void OnGUI()
    {
        GUI.Box(new Rect(200, 10, 180, 25), "Time Remaining : " + (timeLimit));
        GUI.Box(new Rect(20, 10, 140, 25), "Candy : " + Candy );
        //GUI.Box(new Rect(Screen.width-100, 10, 100, 25), "Health : " + Health);
        string s = "";
        string label = "Missing Components: ";
        float count = 0;
        float mult = 18f;
        bool check = false;
        foreach (bool i in ingredients)
        {
            if (!i)
            {
                check = true;
                
                break;
            }
        }
        if (!check)
        {
            s = "All ingredients obtained!\nHead to the kitchen!";
            done = true;
            count = 2;
        }
        else
        {
            for(int j=0;j<ingredients.Length;j++)
            {
                if (!ingredients[j])
                {
                    s +=  ing[j]+ "\n";
                    count++;
                }
            }
            if (count == 1)
            {
                count += 0.2f;
            }else if (count == ingredients.Length)
            {
                count -= 0.2f; 
            }
        }
        GUI.Box(new Rect(20, 40, 150, 30), "<b>"+label+"</b>");
        GUI.Box(new Rect(20,75,150,count*mult), s);
        if (nextToCar&& !pauseState.Paused&& hp>0 && Timer>0)
        {
            GUI.Box(new Rect(Screen.width/2, Screen.height-30, 200, 30), "Press [g] to get in car");
        }
    }
    private void Update()
    {
        timeLimit -= Time.deltaTime;
    }
    public int Candy
    {
        get { return candy; }
        set
        {
            candy = value;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Candy"))
        {
            collision.gameObject.SetActive(false);
            Candy++;
            candyget.Play();
        }
        else if (collision.gameObject.CompareTag("Tomato"))
        {
            collision.gameObject.SetActive(false);
            ingredients[0] = true;
            itemget.Play();
        }
        else if (collision.gameObject.CompareTag("Bun"))
        {
            collision.gameObject.SetActive(false);
            ingredients[1] = true;
            itemget.Play();
        }
        else if (collision.gameObject.CompareTag("Patty"))
        {
            collision.gameObject.SetActive(false);
            ingredients[2] = true;
            itemget.Play();
        }
        else if (collision.gameObject.CompareTag("Lettuce"))
        {
            collision.gameObject.SetActive(false);
            ingredients[3] = true;
            itemget.Play();
        }
        else if (collision.gameObject.CompareTag("Cheese"))
        {
            collision.gameObject.SetActive(false);
            ingredients[4] = true;
            itemget.Play();
        }
        else if (collision.gameObject.CompareTag("Sauce"))
        {
            collision.gameObject.SetActive(false);
            ingredients[5] = true;
            itemget.Play();
        }
        else if (collision.gameObject.CompareTag("Onion"))
        {
            collision.gameObject.SetActive(false);
            ingredients[6] = true;
            itemget.Play();
        }
        else if (collision.gameObject.CompareTag("Pickle"))
        {
            collision.gameObject.SetActive(false);
            ingredients[7] = true;
            itemget.Play();
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = true;
        }
        if (collision.gameObject.CompareTag("Car") && !store.nCar)
        {
            nextToCar = true;
        }
        if (collision.gameObject.CompareTag("Kitchen") && done)
        {
            win = true;
        }
        if (collision.gameObject.CompareTag("Death"))
        {
            Health = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            nextToCar = false;
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = false;
        }
    }
}
