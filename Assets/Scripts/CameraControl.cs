using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Vector2 turn;

    float sensitivityx=SenseX.sensitivityx;

    public float sensitivityy=SenseY.sensitivityy;
    // Update is called once per frame
    [SerializeField]
    GameObject obj;
    public SimpleSampleCharacterControl store;
    public GameObject car;
    void Start()
    {
            store = GameObject.Find("Player").GetComponent<SimpleSampleCharacterControl>();

    }
    void Update()
    {
        sensitivityy = SenseY.sensitivityy;
        sensitivityx = SenseX.sensitivityx;
        if (Time.timeScale == 0f)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Time.timeScale != 0f)
        {
            turn.x += sensitivityx * Input.GetAxis("Mouse X");
            turn.y += sensitivityy * Input.GetAxis("Mouse Y");
            Mathf.Clamp(turn.y, -90, 90);
            transform.localRotation = Quaternion.Euler(PauseMenu.inverse*turn.y, turn.x, 0);
            if (!store.nCar)
            {
                transform.position = obj.transform.position;
                transform.position += new Vector3(0, 1f);
            }
            else
            {
                transform.position = car.transform.position + new Vector3(0, 2, 0);
            }
        }
            
    }
}
