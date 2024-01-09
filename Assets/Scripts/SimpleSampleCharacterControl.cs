using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSampleCharacterControl : MonoBehaviour
{


    [SerializeField] public float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;


    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;


    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;

    public bool ground
    {
        get { return m_isGrounded; }
        set
        {
            m_isGrounded = value;
        }
    }
    private List<Collider> m_collisions = new List<Collider>();
    public ObjectCollection Storage;
    
    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.CompareTag("Projectile")||collision.gameObject.CompareTag("Candy")|| collision.gameObject.CompareTag("Tomato")|| collision.gameObject.CompareTag("Bun")|| collision.gameObject.CompareTag("Patty")|| collision.gameObject.CompareTag("Lettuce")|| collision.gameObject.CompareTag("Cheese")|| collision.gameObject.CompareTag("Sauce")|| collision.gameObject.CompareTag("Onion")|| collision.gameObject.CompareTag("Pickle")))
        {
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!m_collisions.Contains(collision.collider))
                    {
                        m_collisions.Add(collision.collider);
                    }
                    m_isGrounded = true;
                }
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }

    }
    float mult;
    bool inCar = false;
    public bool nCar
    {
        get { return inCar; }
        set
        {
            inCar = value;
        }
    }
    private float fov;
    private void Update()
    {
        if (!m_jumpInput && Input.GetKey(KeyCode.Space))
        {
            m_jumpInput = true;
        }
        if (Input.GetKeyDown("g"))
        {
            if (Storage.Cars&&!inCar)
            {
                Storage.Cars = false;
                inCar = true;
            }else if(inCar){
                inCar = false;
                transform.position = car.transform.position + car.right * -1 + car.forward * 1.3f + new Vector3(0,0,0);
                m_rigidBody.velocity = new Vector3(0,0,0);
            }
        }
        if (inCar)
        {
            Storage.Cars = false;
        }
        /*if (Input.GetKeyDown("f"))
        {
            Storage.Candy++;
        }*/
        if (Storage.Candy > 0 && Input.GetKeyDown("c") && !hyper)
        {
            Storage.Candy--;
            hyper = true;
            if (Difficult.slideVal != 4)
            {
                Storage.hp += 2;
            }
            prehyper = true;
            StartCoroutine(noHyper(10f));
        }
        if (hyper)
        {
            mult = 4f;
            fov=Camera.main.fieldOfView + 20;
            Camera.main.fieldOfView= Mathf.Lerp(Camera.main.fieldOfView, fov, Time.deltaTime * m_interpolation);
        }
        else
        {
            if (prehyper)
            {
                prehyper = false;
                fov = Camera.main.fieldOfView - 20;
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, Time.deltaTime * m_interpolation);
            }
            mult = 2f;
        }
    }
    public Transform car;
    private void FixedUpdate()
    {
        m_animator.SetBool("Grounded", m_isGrounded);
        if (!inCar)
        {
            DirectUpdate();
        }
        else
        {
            
            transform.position =car.transform.position+ new Vector3(0,0.16f,0)+car.forward*0.85f;
            transform.rotation = car.transform.rotation;
        }
        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    public Transform pivot;
    bool hyper = false;
    bool prehyper = false;
    
    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;
        if (Storage.Water)
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1) )
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }
        //testing method
        
            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);
        
        
        

        Vector3 direction = mult*camera.forward * m_currentV + mult*camera.right *m_currentH;
        
        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);


            if (Input.GetMouseButton(1))
            {
                
                transform.rotation = Quaternion.LookRotation(pivot.forward);
                
            }
            else {
                transform.rotation = Quaternion.LookRotation(m_currentDirection);
            }
            //transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;
            m_rigidBody.velocity = m_currentDirection*2+new Vector3(0,m_rigidBody.velocity.y,0);
            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }
    private IEnumerator noHyper(float time)
    {
        yield return new WaitForSeconds(time);
        hyper = false;
    }
    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;
        
        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {

            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
}
