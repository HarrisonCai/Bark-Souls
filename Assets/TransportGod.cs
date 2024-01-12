using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransportGod : MonoBehaviour
{
    public Image white;
    public TextMeshProUGUI one, two, three;
    public float FadeIn_out, words, Delay;
    float Timer;

    float DelayTimer;
    bool first, second, third;
    bool flashbang = false;
    bool stopTimers = false;
    Color colorTempw,colorTemp1,colorTemp2,colorTemp3;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        colorTempw = white.color;
        colorTemp1 = one.color;
        colorTemp2 = two.color;
        colorTemp3 = three.color;
        DelayTimer = Delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (!flashbang)
            {
                Timer += Time.deltaTime;
                if (Timer >= FadeIn_out)
                {
                    Timer = FadeIn_out;
                    flashbang = true;
                }
                colorTempw.a = Timer / FadeIn_out;
                white.color = colorTempw;
                if (flashbang)
                {
                    Timer = 0;
                }

            }
            if (flashbang && !first)
            {
                if (DelayTimer > 0 && !stopTimers)
                {
                    DelayTimer -= Time.deltaTime;
                }
                if (DelayTimer <= 0)
                {
                    stopTimers = true;
                    Timer += Time.deltaTime;
                    if (Timer >= words)
                    {
                        Timer = words;
                        first = true;
                    }
                    colorTemp1.a = Timer / words;
                    one.color = colorTemp1;
                    if (first)
                    {
                        Timer = 0;
                        DelayTimer = Delay;
                        stopTimers = false;
                    }
                }

            }
            if (flashbang && first && !second)
            {
                if (DelayTimer > 0 && !stopTimers)
                {
                    DelayTimer -= Time.deltaTime;
                }
                if (DelayTimer <= 0)
                {
                    stopTimers = true;
                    Timer += Time.deltaTime;
                    if (Timer >= words)
                    {
                        Timer = words;
                        second = true;
                    }
                    colorTemp2.a = Timer / words;
                    two.color = colorTemp2;
                    if (second)
                    {
                        Timer = 0;
                        DelayTimer = Delay;
                        stopTimers = false;
                    }
                }

            }
            if (flashbang && first && second&& !third)
            {
                if (DelayTimer > 0 && !stopTimers)
                {
                    DelayTimer -= Time.deltaTime;
                }
                if (DelayTimer <= 0)
                {
                    stopTimers = true;
                    Timer += Time.deltaTime;
                    if (Timer >= words*2)
                    {
                        Timer = words*2;
                        third = true;
                    }
                    colorTemp3.a = Timer / (2*words);
                    three.color = colorTemp3;
                    if (third)
                    {
                        Timer = 0;
                        DelayTimer = Delay;
                        stopTimers = false;
                    }
                }

            }
        }
    }
}
