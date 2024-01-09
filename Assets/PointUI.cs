using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PointUI : MonoBehaviour
{
    public MovementController ps;
    public GameObject p1, p2, p3, p4, p5, p6, p7, p8, p9, p10;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
        p5.SetActive(false);
        p6.SetActive(false);
        p7.SetActive(false);
        p8.SetActive(false);
        p9.SetActive(false);
        p10.SetActive(false);
        if (ps.points >= 1)
        {
            p1.SetActive(true);
        }
        if (ps.points >= 2)
        {
            p2.SetActive(true);
        }
        if (ps.points >= 3)
        {
            p3.SetActive(true);
        }
        if (ps.points >= 4)
        {
            p4.SetActive(true);
        }
        if (ps.points >= 5)
        {
            p5.SetActive(true);
        }
        if (ps.points >= 6)
        {
            p6.SetActive(true);
        }
        if (ps.points >= 7)
        {
            p7.SetActive(true);
        }
        if (ps.points >= 8)
        {
            p8.SetActive(true);
        }
        if (ps.points >= 9)
        {
            p9.SetActive(true);
        }
        if (ps.points >= 10)
        {
            p10.SetActive(true);
        }
    }
    
}
