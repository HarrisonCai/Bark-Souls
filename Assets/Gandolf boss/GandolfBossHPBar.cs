using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GandolfBossHPBar : MonoBehaviour
{
    public string title, subtitle;
    public float timeToFill = 3;
    float currentTime;
    public TextMeshProUGUI hppercent, text, subtext;
    public Gandolf health;
    public Image image;
    float maxhp;
    float target = 0;
    float currentFill;

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
        if (health.hp <= 0)
        {
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
