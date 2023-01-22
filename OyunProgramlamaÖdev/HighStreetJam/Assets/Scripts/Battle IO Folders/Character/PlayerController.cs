using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Lofelt.NiceVibrations;

public class PlayerController : MonoBehaviour
{
    CharacterController charController;
    Rigidbody rb;
    [SerializeField] InputAction joystick;
    [SerializeField] float speedInspector;

    public ParticleSystem particlPrefabSup;
    public ParticleSystem particlPrefab;
    public SprayingTrigger[] sprayingTriggers;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Animator supportAnimator;
    BuyingSupport buyingSupport;

    public static float speed = 10f;
    public float drawingTime = 10.0f;
    [SerializeField] float turnSmoothCoef = 0.1f;

    public float speedIncreaseCoefficient = 0.5f;
    public int SpeedUpgrade { get; set; }
    private float turnSmoothVelocity;

    [HideInInspector] public float vertical=0f;
    [HideInInspector] public float horizontal=0f;

    Coin coin;
    private string spraySpeedUpgradeID = "SpraySpeedUpgrade";
    private string runningSpeedUpgradeID = "RunningSpeedUpgrade";


    Vector3 direction;
    Vector3 gravityForce;
    Vector3 currentGravityForce = Vector3.zero;

    public bool isSpraying = false;
    public bool isItOn = false;
    public bool isItLocked = false;
    public bool isItFirstSpraying = true;
    public bool move = true;
    [HideInInspector] public bool isPressed;

    [HideInInspector] public GameObject WhichWall;
    [HideInInspector] public SprayingTrigger WhichWallTrigger;

    int priceOfspraySpeedUpgrade = 75;
    int priceOfrunningSpeedUpgrade = 75;
    string priceOfspraySpeedUpgradeID = "priceSpeedSpray";
    string priceOfrunningSpeedUpgradeID = "priceSpeedRunning";

    float sprayingSpeedMultiplier = 1;
    string sprayingSpeedMultiplierID = "sprayingSpeedMultiplier";

    ChaseRange RandomPolice;

    [HideInInspector] public float speedRb;

    public float animSpeedCoef;

    Text textOfPrice1;
    Text textOfPrice2;

    public static float staticYPos = 0.0f;
    public float PlayerSpeed 
    {
        get 
        { 
            return direction.magnitude; 
        }
    }


    private void Awake()
    {
        textOfPrice1 = GameObject.Find("SpeedOfSprayText").GetComponent<Text>();
        textOfPrice1.text = priceOfspraySpeedUpgrade.ToString();
        textOfPrice2 = GameObject.Find("SpeedText").GetComponent<Text>();
        textOfPrice2.text = priceOfrunningSpeedUpgrade.ToString();

        drawingTime = PlayerPrefs.GetFloat(spraySpeedUpgradeID, 6);
        SpeedUpgrade = PlayerPrefs.GetInt(runningSpeedUpgradeID, 1);
        priceOfspraySpeedUpgrade = PlayerPrefs.GetInt(priceOfspraySpeedUpgradeID, 75);
        priceOfrunningSpeedUpgrade = PlayerPrefs.GetInt(priceOfrunningSpeedUpgradeID, 75);
        sprayingSpeedMultiplier = PlayerPrefs.GetFloat(sprayingSpeedMultiplierID, 1);
        gravityForce = Physics.gravity;
        charController = GetComponent<CharacterController>();
        animator = transform.Find("Mesh").gameObject.GetComponent<Animator>();
        supportAnimator = GameObject.Find("SupportCharacter").transform.Find("DwarfIdle").gameObject.GetComponent<Animator>();
        sprayingTriggers = GameObject.FindObjectsOfType<SprayingTrigger>();
        rb = GetComponent<Rigidbody>();
        coin = GameObject.Find("Coin").gameObject.GetComponent<Coin>();
        RandomPolice = GameObject.FindGameObjectWithTag("police").GetComponent<ChaseRange>();
        buyingSupport = GameObject.Find("SupportCharacter").GetComponent<BuyingSupport>();
        animator.SetFloat("SprayingSpeed", sprayingSpeedMultiplier);
        supportAnimator.SetFloat("SprayingSpeed", sprayingSpeedMultiplier);
    }
    private void Start()
    {
        GameObject.Find("SpeedOfSprayText").GetComponent<Text>().text = priceOfspraySpeedUpgrade.ToString();
        GameObject.Find("SpeedText").GetComponent<Text>().text = priceOfrunningSpeedUpgrade.ToString();
        speed = speedInspector;
        
    }
    private void OnEnable()
    {
        joystick.Enable();
    }

    private void OnDisable()
    {
        joystick.Disable();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { isPressed = true; }
        else if (Input.GetMouseButtonUp(0)) { isPressed = false; }
        //if (!(GameManager.isGameStarted))
        //{
        //    return;
        //}
        if (move)
        {
            ControlCharacter();
        }
        move = true;

        HandleGravity();
        transform.position = new Vector3(transform.position.x, staticYPos, transform.position.z);

        isItOn = false;
        isItLocked = false;

        foreach (var trigger in sprayingTriggers)
        {
            if (trigger.isSpraying)
            {
                isSpraying = true;
            }
            if (trigger.isItOn)
            {
                isItOn = true;
                WhichWall = trigger.gameObject.transform.parent.transform.Find("Sprite(Image)").gameObject;
                WhichWallTrigger = trigger;
            }
            if (trigger.isItLocked)
            {
                isItLocked = true;
            }
        }

        if (horizontal != 0 || vertical != 0 && !isItLocked)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        if (isSpraying)
        {
            if (isItFirstSpraying && !animator.GetBool("IsSpraying"))
            {
                particlPrefabSup.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                particlPrefab.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                isItFirstSpraying = false;
            }
            animator.SetBool("IsSpraying", true);
            if (!particlPrefab.isPlaying)
                particlPrefab.Play();
            if (buyingSupport.isItSelled)
                if (!particlPrefabSup.isPlaying)
                    particlPrefabSup.Play();
            isSpraying = false;
        }
        else
        {
            isItFirstSpraying = true;
            if(!isPressed)
                animator.SetBool("IsSpraying", false);
            if (particlPrefab.isPlaying && !isPressed)
            {
                particlPrefabSup.Stop();
                particlPrefab.Stop();
            }
        }
        if (isItLocked)
        {
            animator.SetBool("IsRunning", false);
        }

    }

    //private void FixedUpdate()
    //{
    //    transform.position = new Vector3(transform.position.x, staticYPos, transform.position.z);
    //}

    private void ControlCharacter()
    {
        horizontal = joystick.ReadValue<Vector2>().x;
        vertical = joystick.ReadValue<Vector2>().y;
        float magnitude = new Vector2(horizontal, vertical).magnitude;
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDir = Vector3.zero;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg)+45f;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothCoef);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            charController.Move(magnitude*moveDir.normalized * (speed +(SpeedUpgrade*speedIncreaseCoefficient))* Time.deltaTime);
        }

        speedRb = (magnitude * moveDir.normalized * (speed + (SpeedUpgrade * speedIncreaseCoefficient)) / 60f * animSpeedCoef).magnitude;
        animator.SetFloat("runSpeed", speedRb);
    }

    private void HandleGravity()
    {
        if (!charController.isGrounded)
        {
            currentGravityForce += gravityForce * Time.deltaTime;
            charController.Move(currentGravityForce * Time.deltaTime);
        }
        else
        {
            currentGravityForce = Vector3.zero;
        }
    }

    public void LookAt(Transform targetPos=null)
    {
        if (targetPos != null)
        {
            transform.LookAt(targetPos);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg);//+20f;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothCoef);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    public static float map(float value, float min, float max)
    {
        return (value - min) * 1f / (max - min);
    }

    public void spraySpeedUpgrade()
    {
        if (coin.coinAmount >= priceOfspraySpeedUpgrade)
        {
            drawingTime = drawingTime / 1.1f;
            coin.AddValue(-priceOfspraySpeedUpgrade);
            PlayerPrefs.SetFloat(spraySpeedUpgradeID, drawingTime);
            priceOfspraySpeedUpgrade *= 2;
            PlayerPrefs.SetInt(priceOfspraySpeedUpgradeID, priceOfspraySpeedUpgrade);
            textOfPrice1.text = priceOfspraySpeedUpgrade.ToString();

            sprayingSpeedMultiplier *= 1.1f;
            animator.SetFloat("SprayingSpeed", sprayingSpeedMultiplier);
            supportAnimator.SetFloat("SprayingSpeed", sprayingSpeedMultiplier);
            PlayerPrefs.SetFloat(sprayingSpeedMultiplierID, sprayingSpeedMultiplier);
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
        }
        
    }
    
    public void runningSpeedUpgrade()
    {
        if (coin.coinAmount >= priceOfrunningSpeedUpgrade)
        {
            SpeedUpgrade += 1;
            coin.AddValue(-priceOfrunningSpeedUpgrade);
            PlayerPrefs.SetInt(runningSpeedUpgradeID, SpeedUpgrade);
            priceOfrunningSpeedUpgrade *= 2;
            PlayerPrefs.SetInt(priceOfrunningSpeedUpgradeID, priceOfrunningSpeedUpgrade);
            textOfPrice2.text = priceOfrunningSpeedUpgrade.ToString();

            PlayerPrefs.SetFloat(RandomPolice.policeRunningSpeedUpgradeID, RandomPolice.chasingSpeed*1.035f);

            foreach(GameObject police in GameObject.FindGameObjectsWithTag("police")){
                police.GetComponent<ChaseRange>().chasingSpeed = PlayerPrefs.GetFloat(RandomPolice.policeRunningSpeedUpgradeID);
                police.GetComponent<ChaseRange>().walkSpeed = PlayerPrefs.GetFloat(RandomPolice.policeRunningSpeedUpgradeID) / 2f;
            }
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
        }
    }
}
