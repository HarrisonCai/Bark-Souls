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

    MovementController check;

    bool winner = false;

    public bool winCon=false;
    // Update is called once per frame
    private void Awake()
    {
 
        check = Player.GetComponent<MovementController>();
    }
    void Update()
    {

        if (check.Health <= 0) 
        {
            Player.GetComponent<MovementController>().m_rigidBody.velocity = Vector3.zero;
            Unalive();

        }else if (winCon)
        {
            Player.GetComponent<MovementController>().m_rigidBody.velocity = Vector3.zero;
            Win();
        }
    }
    void Win()
    {


        
        WinCondition.SetActive(true);
        button.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    void Unalive()
    {


        
        
        gaemoverMenuUI.SetActive(true);
        button.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
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
