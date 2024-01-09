using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{
     float minFov = 5f;
    float maxFov  = 110f;
    float sensitivity = 25f;
    float fov = 90f;
    float currentfov = 90f;
    float interlopation = 10f;
    public SimpleSampleCharacterControl store;
    
    private void Start()
    {
        store = GameObject.Find("Player").GetComponent<SimpleSampleCharacterControl>();
    }
    void Update()
    {
        if (store.nCar)
        {
            maxFov = 140f;
        }
        else
        {
            maxFov = 110f;
        }
        fov -=Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        currentfov = Mathf.Lerp(currentfov, fov, Time.deltaTime*interlopation);
        Camera.main.fieldOfView = currentfov;
    }
}
