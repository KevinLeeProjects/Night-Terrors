using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Gravity variables 
    private float gravityOriginal = -9.81f;
    private float gravityModifier = 2f;

    //Photon variables
    private PhotonView pv;

    //Character variables
    //private CharacterController myCC;

    //Movement variables
    public float movementSpeed;
    //public float movementDirection; //0 = forwards, 1 = backwards, 2 = left, 3 = right
    //private KeyCode leftMove = KeyCode.A;
    //private KeyCode rightMove = KeyCode.D;
    //private KeyCode upMove = KeyCode.W;
    //private KeyCode downMove = KeyCode.S;
    private KeyCode shiftKey = KeyCode.LeftShift;
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode climbKey = KeyCode.F;
    private KeyCode jumpScareKey = KeyCode.F;
    private bool enableClimb = false;
    //private bool canJump = true;
    //private KeyCode jumpKey = KeyCode.Space;
    private float jumpForce = 500;
    private bool grounded;
    Rigidbody rb;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    private float sprintSpeed = 50;
    private float smoothTime = 0;
    private GameObject otherPlayerToClimb;

    //private float gravity = 30.0f;
    //private float jumpLimit = 10;

    //PickingUpWeapons
    //public static float pickUpWeaponCode = 0;

   // private bool shouldJump => Input.GetKeyDown(jumpKey) && myCC.isGrounded;

    //Rotation variables
    private float rotationSpeed = 50;

    //Camera variables
    public GameObject cam;
    [SerializeField]
    private GameObject target;
    private float targetOrigin;
    float rotateSpeed = 2;
    float xRotation = 0f;
    //Animator variables
    Animator animator;

    //Attack variables
    private KeyCode leftClick = KeyCode.Mouse0;
    [SerializeField]
    private GameObject playerHand;
    private Animation playerBasicAttackAnim;

    //Weapon variables


    //Microphone variables
    //private List<string> listOfMics = new List<string>();
    //public static bool isMicDetected;
    //private bool restartMic = true;
    //AudioSource mainMic;
    //private float[] samplesBuffer;
    //private string micName;
    //[SerializeField]
    //GameObject template;
    //GameObject samplesRenderer;

    //private float soundScale;

    void Start()
    {
        Physics.gravity = new Vector3(0, gravityOriginal * gravityModifier, 0);
        //soundScale = 90000;
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        //myCC = GetComponent<CharacterController>();
        if (gameObject.tag == "Player")
        {
            movementSpeed = 10;
            playerBasicAttackAnim = playerHand.GetComponent<Animation>();
            //GameObject weaponSpawnerGO = GameObject.FindGameObjectWithTag("WeaponSpawner");
            //PhotonView weaponSpawnPV = weaponSpawnerGO.GetComponent<weaponSpawn>().pv;
            //if(weaponSpawnPV.IsMine)
            //{
            //    //asd.text = "1";
            //}
            //->
            //mainMic = GetComponent<AudioSource>();

            //<-
        }
        else
        {
            movementSpeed = 20;
            rotateSpeed *= 5;
        }

        if(!pv.IsMine)
        {
            cam.SetActive(false);
            Destroy(rb);
        }
    }

    //Using this format because the code is only one line, so we don't have to use brackets
    void Awake() => animator = GetComponent<Animator>();

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            //->
            //listOfMics.Clear();
            //noMicDetectedCode();
            //<-
            Cursor.visible = false;
            BasicMovement();
            BasicRotation();
            BasicJump();
            BasicAttack();
            JumpScare();
        }
    }

    
    //->
    //MIC CHECK
    //public bool checkMic()
    //{
    //    for (int i = 0; i < Microphone.devices.Length; i++)
    //    {
    //        listOfMics.Add(Microphone.devices[i]);
    //    }

    //    if (listOfMics.Count == 0)
    //    {
    //        isMicDetected = false;
    //        return false;
    //    }
    //    else
    //    {
    //        isMicDetected = true;
    //        return true;
    //    }
    //}
    //<-

    //->
    //public void noMicDetectedCode()
    //{
    //    if (checkMic())
    //    {
    //        if (restartMic)
    //        {
    //            int micLengthMinusOne = listOfMics[0].Length - 1;
    //            micName = listOfMics[0].Substring(0, micLengthMinusOne);
    //            micName = micName.Substring(12);
    //            Debug.Log(micName);

    //            //Unity mic system Uncomment if going to use again from here ->

    //            //mainMic.clip = Microphone.Start(listOfMics[0], true, 1, 44100);
    //            ////
    //            //mainMic.loop = true;
    //            //while (!(Microphone.GetPosition(null) > 0)) { }
    //            //mainMic.Play();
    //            //Debug.Log(Microphone.IsRecording(listOfMics[0]));
    //            //samplesBuffer = new float[mainMic.clip.channels];
    //            ////samplesRenderer = new GameObject[samplesBuffer.Length];
    //            ////for(int i = 0; i < samplesRenderer.Length; i++)
    //            ////{
    //            ////    samplesRenderer[i] = Instantiate(template, Vector3.right * i, Quaternion.identity);
    //            ////}
    //            //samplesRenderer = Instantiate(template, new Vector3(0,0,0), Quaternion.Euler(0,-90,90));

    //            // <- until here
    //            restartMic = false;
    //        }
    //        //Uncomment this also ->
    //        //int micPosition = Microphone.GetPosition(micName);
    //        //mainMic.clip.GetData(samplesBuffer, micPosition);
    //        //<-

    //        //for(int i = 0; i < samplesBuffer.Length; i++)
    //        //{
    //        //    sum += samplesBuffer[i];
    //        //}
            
    //        //Uncomment ->
    //        //for (int i = 0; i < samplesBuffer.Length; i++)
    //        //{
    //        //    float bufferSize = samplesBuffer[i] * soundScale;
    //        //    if (bufferSize < 0)
    //        //    {
    //        //        bufferSize *= -1;
    //        //    }
    //        //    samplesRenderer.transform.localScale = new Vector3(bufferSize, bufferSize, bufferSize);
    //        //}
    //        // <-

    //        Cursor.visible = false;
    //        BasicMovement();
    //        BasicRotation();
    //    }
    //    else
    //    {
    //        restartMic = true;
    //    }
    //}

    //<-

    //MOVEMENT
    void BasicMovement()
    {
        //https://www.youtube.com/watch?v=AZRdwnBJcfg&ab_channel=RugbugRedfern
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(shiftKey) ? sprintSpeed : movementSpeed), ref smoothMoveVelocity, smoothTime);





        //if (Input.GetKey(KeyCode.W))
        //{
        //    myCC.Move(transform.forward * Time.deltaTime * movementSpeed);
        //    movementDirection = 0;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    myCC.Move(-transform.right * Time.deltaTime * movementSpeed);
        //    movementDirection = 2;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    myCC.Move(-transform.forward * Time.deltaTime * movementSpeed);
        //    movementDirection = 1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    myCC.Move(transform.right * Time.deltaTime * movementSpeed);
        //    movementDirection = 3;
        //}



        //=>
        //if(Input.GetKey(KeyCode.Space))
        //{
        //    if (jumpForce == 0)
        //    {
        //        Debug.Log("Jump");
        //        while(jumpForce <= jumpLimit)
        //        {
        //            jumpForce += 1;
        //            gameObject.transform.position = new Vector3(transform.position.x, jumpForce, transform.position.z);
        //        }
        //    }
        //}
        //if (jumpForce >= jumpLimit)
        //{
        //    Debug.Log("Here");
        //    StartCoroutine(waitForGravity());
        //}
        //if (jumpForce < 0)
        //{
        //    jumpForce = 0;
        //}
        //<=

        //vertical = Mathf.Clamp(vertical, -600, 600);

        //cam.transform.localEulerAngles = new Vector3(-vertical, 0, 0);
        //Debug.Log(cam.transform.rotation);
        //cam.transform.Rotate(-xRotation, 0, 0);
        //float desiredAngleV = cam.transform.eulerAngles.x;
        //Quaternion rotation = Quaternion.Euler(-desiredAngleV, 0, 0);
        //transform.position = cam.transform.position - (rotation * 100);
        //transform.LookAt(cam.transform);
        targetOrigin = transform.position.y;



        if(enableClimb && Input.GetKey(climbKey))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(otherPlayerToClimb.transform.position.x, otherPlayerToClimb.transform.position.y + 5, otherPlayerToClimb.transform.position.z), 5 * Time.deltaTime);
        }
    }

    void BasicJump()
    {
        if (gameObject.tag == "Player" && Input.GetKeyDown(jumpKey) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
            //target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 10, target.transform.position.z);
        }
    }
     
    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine)
            return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Monster" && collision.gameObject.tag == "Wall")
        {
            Debug.Log("ENter");
            movementSpeed = 10;
        }
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Wall")
        {
            Debug.Log("ENter");
            movementSpeed = 5;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (gameObject.tag == "Monster" && collision.gameObject.tag == "Wall")
        {
            movementSpeed = 10;
        }
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Wall")
        {
            Debug.Log("ENter");
            movementSpeed = 5;
        }
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "ClimbHitBox")
        {
            otherPlayerToClimb = collision.gameObject;
            enableClimb = true;
        }
        // Debug.Log(collision.gameObject.name);
    }

    void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Layer: " + collision.gameObject.layer);
        if (collision.gameObject.layer == 3)
        {
            enableClimb = false;
        }

        if (gameObject.tag == "Monster" && collision.gameObject.tag == "Wall")
        {
            movementSpeed = 20;
        }
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Wall")
        {
            Debug.Log("ENter");
            movementSpeed = 10;
        }
    }

    //IEnumerator waitForGravity()
    //{
    //    yield return new WaitForSeconds(1);

    //    while (jumpForce != 0)
    //    {
    //        jumpForce -= 1;
    //        gameObject.transform.position = new Vector3(transform.position.x, jumpForce, transform.position.z);
    //    }
    //}


    //ROTATION
    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 59f);

        //cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       // if(grounded)
        target.transform.position = new Vector3(target.transform.position.x, targetOrigin + xRotation, target.transform.position.z);

        //target.transform.RotateAround(transform.position, new Vector3(0, 0, 0), rotateSpeed * Time.deltaTime);
        //target.transform.eulerAngles = new Vector3(target.transform.eulerAngles.x, transform.eulerAngles.y, target.transform.eulerAngles.z);

        //target.transform.rotation = Quaternion.Euler(60, 0, 0);
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    void BasicAttack()
    {
        if (Input.GetKey(leftClick) && gameObject.tag == "Player" && !playerBasicAttackAnim.isPlaying)
        {
            playerBasicAttackAnim.Play();
            GameObject monster = GameObject.Find("PhotonMonster");
            //float distance = Vector3.Distance(monster.transform.position, gameObject.transform.position);
            foreach (Transform child in transform)
            {
                Debug.Log(child);
            }
        }
    }

    void JumpScare()
    {
        if(Input.GetKey(jumpScareKey) && gameObject.tag == "Monster")
        {
            //Debug.Log("Jump Scare");
            GameObject[] closestPlayers = TrackDownClosestPlayer.closestPlayers();
            for(int i = 0; i < closestPlayers.Length; i++)
            {
                Debug.Log(closestPlayers[i]);
                GameObject closestPlayer = closestPlayers[i];
                //gameObject.transform.position = closestPlayers[i].transform.position;
                float frontDist = Vector3.Distance(closestPlayer.GetComponent<CardinalDirections>().getFront(), gameObject.transform.position);
                float backDist = Vector3.Distance(closestPlayer.GetComponent<CardinalDirections>().getBack(), gameObject.transform.position);
                if(frontDist > backDist)
                {
                    gameObject.transform.position = closestPlayer.GetComponent<CardinalDirections>().getFront();
                    gameObject.transform.Rotate(new Vector3(0, 180, 0));
                }
                return;
            }
        }
    }
}
