using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MovementController : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }
    public Image dashimg, majoraimg;
    public TextMeshProUGUI dashpercent, majorapercent;
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    public CameraController cameraC;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;
    
    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;

    private List<Collider> m_collisions = new List<Collider>();

    private void Awake()
    {


        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Melee Hitbox")))
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
    public float Health = 80;
    float Meteorcooldown = 30f;
    float MeteorTimeRemain;
    public GameObject Meteorprefab;
    bool ult = false;
    float Dashatkcooldown = 1f;
    float DashTimeRemain;
    float AtkTime;
    float speed;
    public bool parryState = false;
    float perfectParryWindow = 0.2f;
    float perfectParryTimer;
    public bool perfectparryattempt = false;
    public bool parryHit = false;
    float parryCooldown = 0.5f;
    float parryTimeRemain;
    Vector3 AtkDir = Vector3.zero;
    RaycastHit hit;
    bool DashAtk = false;
    bool Atak = false;
    public GameObject Dash, Atk1, Atk2, Atk3,Drop,DropLand;
    bool nrmlAtk = false;
    int nrmlAtkNum = 1;
    float nextAtkWindow = 0.45f;
    float nrmlAtkTimer;
    bool Atk3rd = false;
    float DelayTimer;
    public int points = 0;
    bool DropAtk=false;
    public MeleeAtk dropkick;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            points++;
        }
        if (points > 10)
        {
            points = 10;
        }
        //store atk buttons
        if (!Atak)
        {
            Parry();
        }
        
        if (!parryState)
        {
            Atk();
        }
        if (nrmlAtk && nrmlAtkNum == 1)
        {
            if (DelayTimer < 0)
            {
                Atk1.SetActive(true);
            }
            if (AtkTime < 0)
            {
                Atak = false;
                nrmlAtk = false;
                Atk1.SetActive (false);
                nrmlAtkNum = 2;
                nrmlAtkTimer = nextAtkWindow;
            }
        }
        if (nrmlAtk && nrmlAtkNum == 2)
        {
            if (DelayTimer < 0)
            {
                Atk2.SetActive(true);
            }
            if (AtkTime < 0)
            {
                Atak = false;
                nrmlAtk = false;
                Atk2.SetActive(false);
                nrmlAtkNum = 3;
                nrmlAtkTimer = nextAtkWindow;
            }
        }
        if (nrmlAtk && nrmlAtkNum == 3)
        {
            if (!Atk3rd&&DelayTimer < 0)
            {
                Atk3.SetActive(true);
                Atk3rd = true;
                DelayTimer = 0.4f;
            }
            if (Atk3rd && DelayTimer < 0)
            {
                Atk3.SetActive(false);
            }
            if (AtkTime < 0)
            {
                Atak = false;
                nrmlAtk = false;
                Atk3rd = false;
                nrmlAtkNum = 1;
                    
            }
        }

        if (nrmlAtkNum!=1&&!nrmlAtk && nrmlAtkTimer < 0)
        {
            nrmlAtkNum = 1;
        }
        if (DashAtk)
        {
            transform.position = Vector3.Slerp(transform.position, AtkDir, Time.deltaTime *20);
            Dash.SetActive(true);

            //m_rigidBody.velocity = Vector3.Slerp(m_rigidBody.velocity, AtkDir, Time.deltaTime * 10);
            if (AtkTime < 0)
            {
                DashTimeRemain = Dashatkcooldown;
                Dash.SetActive(false);
                Atak = false;
                AtkDir = Vector3.zero;
                DashAtk = false;
                m_rigidBody.velocity = Vector3.zero;
            }
        }
        if (ult)
        {
            if (AtkTime < 0)
            {
                Atak = false;
                MeteorTimeRemain = Meteorcooldown;
                ult = false;
            }
        }
        if (DropAtk)
        {
            Drop.SetActive(true);
            dropkick.damage += Time.deltaTime * 6;
            if (m_isGrounded)
            {
                Drop.SetActive(false);
                DropAtk = false;
                Atak = false;
                DropLand.GetComponent<MeleeAtk>().damage = 0.3f * dropkick.damage;
                Instantiate(DropLand, transform.position - transform.up * 0.4f, Quaternion.Euler(0, 0, 0)) ;
                m_isGrounded = false;
            }
        }
        parryTimeRemain -= Time.deltaTime;
        
            perfectParryTimer -= Time.deltaTime;
        DelayTimer -= Time.deltaTime;
        nrmlAtkTimer-=Time.deltaTime;
        if (DashTimeRemain > 0)
        {
            DashTimeRemain -= Time.deltaTime;
        }
        if (MeteorTimeRemain > 0)
        {
            MeteorTimeRemain -= Time.deltaTime;
        }
        AtkTime -= Time.deltaTime;
        if (!m_jumpInput && Input.GetKey(KeyCode.Space))
        {
            m_jumpInput = true;
        }
        if (DashTimeRemain <= 0)
        {
            DashTimeRemain = 0;
        }
        if (MeteorTimeRemain <= 0)
        {
            MeteorTimeRemain = 0;
        }
        if (DashTimeRemain == 0)
        {
            dashpercent.text = "";
        }
        else
        {
            dashpercent.text = (DashTimeRemain).ToString("#.0");
        }
        if(MeteorTimeRemain==0)
        {
            majorapercent.text = "";
        }
        else
        {
            majorapercent.text = MeteorTimeRemain.ToString("#.0");
        }
        dashimg.fillAmount = DashTimeRemain / Dashatkcooldown;
        majoraimg.fillAmount = MeteorTimeRemain / Meteorcooldown;
    }
    
    
    private void FixedUpdate()
    {




        
            DirectUpdate();
        

        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    
    private void Parry()
    {
        
        if (Input.GetMouseButton(1) && parryTimeRemain <= 0 && !parryState)
        {
            parryState = true;
        }
        if(parryState && Input.GetMouseButtonDown(0)&& !perfectparryattempt)
        {
            perfectparryattempt = true;
            perfectParryTimer = perfectParryWindow;
        }
        if(Input.GetMouseButtonUp(1) && parryState&& !perfectparryattempt)
        {
            parryState = false;
            parryTimeRemain = parryCooldown;
        }
        if (perfectparryattempt && perfectParryTimer <= 0)
        {
          
            parryState = false;
            perfectparryattempt = false;
            parryTimeRemain = parryCooldown*2;
        }
        if (parryHit)
        {
            points++;
            parryState = false;
            perfectparryattempt = false;
            parryTimeRemain = 0;
            parryHit = false;
        }
    }
    private void Atk()
    {
        Transform camera = Camera.main.transform;
        if (Input.GetKeyDown(KeyCode.E) && DashTimeRemain <=0&& !Atak &&points>=1)
        {
            points -= 1;
            
            AtkDir = transform.position+camera.forward * 25f;
            for (int i = -50; i < 51; i++)
            {
                if (Physics.Raycast(transform.position + Vector3.up * 0.01f*i, camera.forward, out hit, 25f))
                {
                    AtkDir = hit.point + Vector3.up * 0.5f;
                }
            }
            
            //AtkDir = m_rigidBody.velocity +camera.forward * 50f;
            
            DashAtk = true;
            Atak = true;
            AtkTime = 0.2f;
            //transform.position += AtkDir * 100 * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q) && MeteorTimeRemain <= 0 && !Atak && points >= 10)
        {
            points -= 10;
            AtkDir = transform.position + camera.forward * 115f;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, camera.forward, out hit, 115f))
            {
                AtkDir = hit.point;
            }
            transform.rotation= Quaternion.Euler(0, cameraC.yRotation, 0);
            Rigidbody rb = Instantiate(Meteorprefab, AtkDir + transform.up * 1000f, Quaternion.Euler(90,0,0)).GetComponent<Rigidbody>();
            m_rigidBody.velocity = Vector3.zero;
            AtkDir = Vector3.zero;
            //AtkDir = m_rigidBody.velocity +camera.forward * 50f;

            ult = true;
            Atak = true;
            AtkTime = 5f;
            //transform.position += AtkDir * 100 * Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0) && !Atak && !DashAtk&&!nrmlAtk&& m_isGrounded)
        {
            NormalAtk();
        }
        if (Input.GetMouseButtonDown(0) && !Atak && !DashAtk && !DropAtk && !m_isGrounded)
        {
            Atak = true;
            DropAtk = true;
            m_rigidBody.velocity = Vector3.zero;
            dropkick = Drop.GetComponent<MeleeAtk>();
            dropkick.damage = 0;
        }

    }

    private void NormalAtk()
    {
        if (nrmlAtkNum == 1)
        {
            nrmlAtk = true;
            Atak = true;
            AtkTime = 0.3f;
            DelayTimer = 0.05f;
        }
        if (nrmlAtkNum == 2)
        {
            nrmlAtk = true;
            Atak = true;
            AtkTime = 0.35f;
            DelayTimer = 0.1f;
        }
        if (nrmlAtkNum == 3)
        {
            nrmlAtk = true;
            Atak = true;
            AtkTime = 1.1f;
            DelayTimer = 0.3f;
        }
    }


    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift)||parryState||Atak)
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = 2f * camera.forward * m_currentV + 2f * camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);



            transform.rotation = Quaternion.LookRotation(m_currentDirection);

            m_rigidBody.velocity = m_currentDirection * m_moveSpeed + transform.up*m_rigidBody.velocity.y;
            
 
        }

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }


    }
}
