using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFix : MonoBehaviour
{
    // Start is called before the first frame update
    static int count = 0;
    void Start()
    {
        if (count == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        count++;
    }

    // Update is called once per frame

}
