using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Difficult : MonoBehaviour
{
    public Slider slide;
    public static float slideVal;
    public TextMeshProUGUI text;
    void Start()
    {
        slideVal = slide.value;
        slide.onValueChanged.AddListener((v)=>
        {
            slideVal = v;
        });
    }

    // Update is called once per frame

    void Update()
    {
        switch (slideVal)
        {
            case 1:
                text.text = "Difficulty: Easy";
                break;
            case 2:
                text.text = "Difficulty: Normal";
                break;
            case 3:
                text.text = "Difficulty: Hard";
                break;
            case 4:
                text.text = "Difficulty: Impossible";
                break;
        }
    }
}
