
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Instr_menu : MonoBehaviour
{
    public Button back;

    // Start is called before the first frame update void Start()
    private void Start()
    {
        Button b = back.GetComponent<Button>();

    }

    public void go_back_main()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


}