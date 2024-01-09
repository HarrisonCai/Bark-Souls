using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisePlay : MonoBehaviour
{
    public AudioSource dmg, death, main;

    public AudioSource battle;
    public static bool inBattle = false;
    bool battleplay = false;
    bool wait = false;
    private void Awake()
    {
        
        main = GameObject.Find("GameEnd").GetComponent<gameoverscreen>().MainTheme;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(inBattle);
        if (inBattle)
        {
            main.volume = 0;
            if (!battleplay)
            {
                battle.Play();
                battleplay = true;
            }
        }
        else
        {
            if (battleplay)
            {

                battleplay = false;
                main.volume = 1;
                battle.Stop();
            }
        }
    }
    
}
