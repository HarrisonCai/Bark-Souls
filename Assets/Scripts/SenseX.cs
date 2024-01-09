using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SenseX : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slide;
    public static float sensitivityx=1;
    public TextMeshProUGUI text;
    void Start()
    {
        slide.value=sensitivityx;
        text.text = sensitivityx.ToString("0.00");
        slide.onValueChanged.AddListener((v) =>
        {
            sensitivityx = v;
            text.text = v.ToString();
        });
    }
}
