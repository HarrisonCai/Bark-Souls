using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoverscreen : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool Unalived = false;
    public GameObject gaemoverMenuUI;
    public GameObject WinCondition;
    public GameObject button;
    public GameObject Player;
    public AudioSource MainTheme;
    public AudioSource GameOverTheme;
    public AudioSource winm;
    ObjectCollection check;
    NoisePlay stuff;
    Boolean winner = false;
    Boolean gameoverMusic = false;
    // Update is called once per frame
    private void Awake()
    {
        stuff = GameObject.Find("Play EnemyNoise").GetComponent<NoisePlay>();
        check = Player.GetComponent<ObjectCollection>();
    }
    void Update()
    {
        if (check.Health <= 0||check.Timer<=0) 
        {
            Player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed = 0;
            Unalive();
 
        }else if (check.Win)
        {

            Player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed = 0;
            Win();
        }
    }
    void Win()
    {
        stuff.battle.Stop();
        MainTheme.volume = 0;
        if (!winner)
        {
            winm.Play();
            winner = true;
        }
        WinCondition.SetActive(true);
        button.SetActive(true);
        Time.timeScale = 0f;
    }
    void Unalive()
    {
        MainTheme.volume = 0;
        stuff.battle.Stop();
        if (gameoverMusic == false) 
        {
            GameOverTheme.Play();
            gameoverMusic = true;
        }
        
        gaemoverMenuUI.SetActive(true);
        button.SetActive(true);
        Time.timeScale = 0.1f;
        Wait();
        Time.timeScale = 0f;
        Unalived = true;
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(2);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }
}
