
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    public Button start_button, close_button, instr_button;

    // Start is called before the first frame update void Start()
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Button start = start_button.GetComponent<Button>();
        Button close = close_button.GetComponent<Button>();
        Button instructions = instr_button.GetComponent<Button>();
    }

    public void game_start()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestingPlane");

    }
    public void game_istr()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("burger-controls");
    }
    public void game_exit()
    {
        Application.Quit();
    }

}