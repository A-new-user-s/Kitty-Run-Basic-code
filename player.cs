using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 currentPosition;

    private int laneNum = 1;
    private float swipeRange = 100;
    private bool stopTouch = false;
    [Header("Intersection")]


    [Header("Player Settings")]
    public bool menuActive;
    public float laneDistance;
    public float playerLerp;
    public float jumpForce;
    public float smoothSpeed;
    public int boostCount = 0;
    public GameObject cat;

    [Header("References")]
    [SerializeField] Animator anim;
    [SerializeField] Transform bottom;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] [Range(0, 1)] float radius = 0.1f;
    [SerializeField] LayerMask groundCheck;
    [SerializeField] LayerMask aboveCheck;
    [SerializeField] LayerMask obstacle;
    [SerializeField] ParticleSystem lighting;

    [Header("Audio")]
    [SerializeField] AudioSource playerSound;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip slip;
    [SerializeField] AudioClip bump;

    [Header("Functions")]
    [SerializeField] gameManager gameManager;
    [SerializeField] speedControl speedControl;
    [SerializeField] camera Camera;
    [SerializeField] float Upping = 10;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isRising = false;
    private bool isJumping = false;
    private bool outOfCar = false;
    public bool isColision = false;

    speedControl SpeedControl;


    
   

    private void Awake()
    {
        SpeedControl= GameObject.Find("gameManager").GetComponent<speedControl>();
    }
    void Start()
    {
        lighting.Pause();
        rb = GetComponent<Rigidbody>();
        anim.SetBool("isBumped", false);
    }


    void Update()
    {
        OutofCar();
        DedicatedSpeed();
        PlayerControl();
        IsGrounded();
    }

    void IsGrounded()
    {
        if (Physics.CheckSphere(bottom.position, radius, groundCheck) || Physics.CheckSphere(bottom.position, radius, aboveCheck))
        {
            isGrounded = true;
            cat.transform.position = new Vector3(cat.transform.position.x, cat.transform.position.y, 0);
        }
        else 
        {
            isGrounded = false;
            cat.transform.position = new Vector3(cat.transform.position.x, cat.transform.position.y, 0);
        
        }
    }

    private void OutofCar()
    {
        if (Physics.CheckSphere(bottom.position, radius, aboveCheck))
        {
            outOfCar = true;
        }
        if (Physics.CheckSphere(bottom.position, radius, groundCheck))
        {
            Invoke("SetCarBack", 1f);
        }
    }
    private void SetCarBack()
    {
        outOfCar = false;
    }

    void LateUpdate()
    {

        if(Physics.CheckSphere(bottom.position, radius, aboveCheck))
        {
            Vector3 desiredPosition1 = new Vector3(cat.transform.position.x, 12.1f, -4.1f);
            Vector3 smoothedPosition1 = Vector3.Lerp(Camera.transform.position, desiredPosition1, smoothSpeed * Time.deltaTime);
            Camera.transform.position = smoothedPosition1;
        }
        else if (Physics.CheckSphere(bottom.position, radius, groundCheck))
        {
            Vector3 desiredPosition = new Vector3(cat.transform.position.x, 9.3f, -4.1f);
            Vector3 smoothedPosition = Vector3.Lerp(Camera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            Camera.transform.position = smoothedPosition;
        }
        else
        {
            Vector3 desiredPosition2 = new Vector3(cat.transform.position.x, Camera.transform.position.y, -4.1f);
            Vector3 smoothedPosition2 = Vector3.Lerp(Camera.transform.position, desiredPosition2, smoothSpeed * Time.deltaTime);
            Camera.transform.position = smoothedPosition2;
        }
    }

    private void PlayerControl()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startPosition;

            if (!stopTouch && !menuActive)
            {
                if (Distance.x < -swipeRange)
                {
                    if (Physics.CheckSphere(left.position, radius, obstacle) || Physics.CheckSphere(left.position, radius, aboveCheck))
                    {
                        return;
                    }
                    else
                    {
                        laneNum--;
                        playerSound.clip = slip;
                        playerSound.Play();
                        if (laneNum == -1)
                            laneNum = 0;
                        stopTouch = true;
                    }
                }
                else if (Distance.x > swipeRange)
                {
                    if (Physics.CheckSphere(right.position, radius, obstacle) || Physics.CheckSphere(right.position, radius, aboveCheck))
                    {
                        return;
                    }
                    else
                    {
                        laneNum++;
                        playerSound.clip = slip;
                        playerSound.Play();
                        if (laneNum == 3)
                            laneNum = 2;
                        stopTouch = true;
                    }
                }
                else if (Distance.y > swipeRange && isGrounded == true && isRising == false)
                {
                    anim.Play("metarig_Rolling");
                    playerSound.clip = jump;
                    playerSound.Play();
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                    stopTouch = true;

                    Jumping();

                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;


        if (laneNum == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (laneNum == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }


        cat.transform.position = Vector3.Lerp(transform.position, targetPosition, playerLerp * Time.deltaTime);
    }

    private void Jumping()
    {
        isJumping = true;
        Invoke("SetBoolBack", 1f);
        
        
    }
    private void SetBoolBack()
    {
        isJumping = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            isColision = true;
            playerSound.clip = bump;
            playerSound.Play();        
            gameManager.GameOver();
            speedControl.SpeedZero();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            
            anim.SetBool("isBumped", true);
            StartCoroutine(Camera.Shake(.07f, .2f));
        }
        if (other.gameObject.tag == "Items")
        {
            lighting.Play();
        }
        if (other.gameObject.tag == "adjust")
        {
            rb.useGravity = true;
            isRising = false;
        }

    }
    private void DedicatedSpeed()
    {
        float speed = SpeedControl.GlobalSpeed;
        if (speed >= 6 && speed < 9)
        {
            GroundCheck(4, 10);
        }
        if (speed >= 9 && speed < 14)
        {
            GroundCheck(5, 10); 
        }      
        if (speed >= 14 && speed < 17)
        {
            GroundCheck(6, 10); 
        }
        if (speed == 17)
        {
            GroundCheck(7, 10); 
        }
        if (speed >= 18)
        {
            GroundCheck(8, 10);
        }

        void GroundCheck(float a, float b )
        {
            if (isGrounded == true)
            {
                Upping = a;
            }
            if (isJumping == true || outOfCar == true)
            {
                Upping = b;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rising")
        {

            Vector3 targetPosition = new Vector3(cat.transform.position.x, 8.87f, 0);
            cat.transform.position = Vector3.Lerp(transform.position, targetPosition, Upping * Time.deltaTime);

            isRising = true;
            rb.useGravity = false;                   

        }
        else if (other.gameObject.tag == "adjust")
        {
            isRising = false;
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = true;
            isRising = false;
        }
      
    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Items")
        {
            lighting.Stop();
        }
    }

}
