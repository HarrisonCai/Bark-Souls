using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SenseY : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public Slider slide;
    public static float sensitivityy=1;
    public TextMeshProUGUI text;
    void Start()
    {
        slide.value = sensitivityy;
        text.text = sensitivityy.ToString("0.00");
        slide.onValueChanged.AddListener((v) =>
        {
            sensitivityy = v;
            text.text = v.ToString();
        });
    }
}
