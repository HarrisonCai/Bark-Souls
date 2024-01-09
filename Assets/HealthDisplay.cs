using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthDisplay : MonoBehaviour
{
    float timeToDrain = 0.25f;
    public Gradient HpGrad;
    public TextMeshProUGUI text;
    public MovementController hp;
    public Image image;
    public float maxhp;
    float target=1;
    float currentFill;
    private Coroutine drain;
    // Start is called before the first frame update
    void Start()
    {
        hp= GameObject.Find("Player").GetComponent<MovementController>();
        maxhp = hp.Health;
        colorCheck();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + hp.Health.ToString("#");
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        target = hp.Health / maxhp;
        currentFill = image.fillAmount;
        image.fillAmount = Mathf.Lerp(currentFill, target, Time.deltaTime*10);
        colorCheck();

    }
    
    private void colorCheck()
    {
        image.color = HpGrad.Evaluate(image.fillAmount);
    }
}
