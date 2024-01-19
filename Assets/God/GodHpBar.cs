using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GodHpBar : MonoBehaviour
{
    public string title, subtitle;
    public float timeToFill = 3;
    float currentTime;
    public TextMeshProUGUI hppercent, text, subtext;
    public God health;
    public Image image;
    float maxhp;
    float target = 0;
    float currentFill;
    public AudioSource main,boss;
    bool yes = false;

    // Start is called before the first frame update
    void Start()
    {
        text.text = title;
        subtext.text = subtitle;
        maxhp = health.maxhp;
        image.fillAmount = 0;
    }


    // Update is called once per frame
    void Update()
    {
        main.Pause();
        if (health.hp <= 0)
        {
            main.Play();
            this.gameObject.SetActive(false);
        }
        if (currentTime < timeToFill)
        {
            currentTime += Time.deltaTime;
            image.fillAmount = (currentTime / timeToFill) * (health.hp / maxhp);
            hppercent.text = "" + (image.fillAmount * 100).ToString("#") + " %";
         
        }
        else
        {
            if (!yes)
            {
                yes = true;
                boss.PlayDelayed(15f);
            }
            hppercent.text = "" + ((health.hp / maxhp) * 100).ToString("#") + " %";
            UpdateHealthBar();
        }
    }
    public void UpdateHealthBar()
    {
        target = health.hp / maxhp;
        currentFill = image.fillAmount;
        image.fillAmount = Mathf.Lerp(currentFill, target, Time.deltaTime * 10);


    }
}
