using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class enemyHpDisplay : MonoBehaviour
{
    float timeToDrain = 0.25f;
    public Gradient HpGrad;
    public Transform player;
    public Enemy Ehp;
    public Image image;
    float maxhp;
    float target = 1;
    float currentFill;

    // Start is called before the first frame update
    void Start()
    {
        
        maxhp = Ehp.hp;
        colorCheck();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        target = Ehp.hp / maxhp;
        currentFill = image.fillAmount;
        image.fillAmount = Mathf.Lerp(currentFill, target, Time.deltaTime * 10);
        colorCheck();

    }

    private void colorCheck()
    {
        image.color = HpGrad.Evaluate(image.fillAmount);
    }
}
