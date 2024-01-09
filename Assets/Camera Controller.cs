using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float fov=90;
    float zoom = 30;
    float norm = 90;
    private readonly float m_interpolation = 10;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    Vector2 turn;
    [SerializeField]
    [Range(0f, 20f)]
    float sensX;
    [SerializeField]
    [Range(0f, 20f)]
    float sensY;
    // Update is called once per frame
    [SerializeField]
    GameObject obj;
    public float multiplier = 3;
     float xRotation;
    public float yRotation;

    void Update()
    {
        transform.position = obj.transform.position;
        transform.position += new Vector3(0, 1f);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= new Vector3(0, 0.33f);
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        yRotation += mouseX * multiplier;

        xRotation -= mouseY * multiplier;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        
 
        
    }
}
