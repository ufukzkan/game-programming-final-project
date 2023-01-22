using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Lofelt.NiceVibrations;
using UnityEngine.AI;

public class SprayingTrigger : MonoBehaviour
{
    [HideInInspector]   private GameObject player;
    [HideInInspector]   private GameObject sprite;
    [HideInInspector]   public SpriteRenderer spriteRenderer;
    [HideInInspector] private GameObject support;
    [HideInInspector]   private Coin coin;

    GameObject Arrow;
    private Spraying sprayingScript;

    private Slider slider;
    private PlayerController playerController;
    private Image colorr;
    private BuyingSupport buyingSupport;
    private Animator supportAnimator;
    private CollectManager collectManager;
    private CameraFollower cameraFollower;
    private GameObject awsomeSprite;
    private Image awsomeSpriteRenderer;
    private ScratchScript scratchScript;
    private GameObject CanvasJoystick;
    private NavMeshObstacle obstacle;
    private bool isAwsomeSpriteOn = false;
    public Sprite[] awsomeSprites;
    public float fadeOutSpeed;
    private float fadeOutSpeedTemp;
    public float fadeOutSpeedMultiplier;
    public float SpawnTime = 60f;
    Vector3 temp;

    public int EarningCoinAmount = 25;

    private float progress = 0f;

    [HideInInspector] public bool isItOn = false;
    [HideInInspector] public bool isItLocked = false;
    [HideInInspector] public bool isSpriteChanged = false;
    [HideInInspector] public bool didIEarn = false;
    [HideInInspector] public bool isPressed;
    public bool isSpraying = false;
    private SpriteRenderer OutOfStock;

    string sprayingSpeedMultiplierID = "sprayingSpeedMultiplier";


    private void Awake()
    {
        OutOfStock = GameObject.Find("OutOfStock").GetComponent<SpriteRenderer>();
        awsomeSprites = Resources.LoadAll("reward", typeof(Sprite)).Cast<Sprite>().ToArray();
        cameraFollower = GameObject.FindObjectOfType<CameraFollower>();
    }


    void Start()
    {
        fadeOutSpeedTemp = fadeOutSpeed;
        Arrow = gameObject.transform.parent.transform.gameObject.transform.Find("Arrow").gameObject;
        coin = FindObjectOfType<Coin>();
        player = GameObject.Find("Player");
        sprite = transform.parent.transform.Find("Sprite(Image)").gameObject;
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        playerController = player.GetComponent<PlayerController>();
        slider = transform.parent.transform.Find("Canvas").Find("Slider").gameObject.GetComponent<Slider>();
        sprayingScript = player.GetComponent<Spraying>();
        colorr = slider.gameObject.transform.Find("FillArea").gameObject.transform.Find("Fill").GetComponent<Image>();
        support = GameObject.Find("SupportCharacter");
        buyingSupport = support.GetComponent<BuyingSupport>();
        supportAnimator = GameObject.Find("SupportCharacter").transform.Find("DwarfIdle").GetComponent<Animator>();
        collectManager = player.GetComponent<CollectManager>();
        awsomeSprite = gameObject.transform.parent.gameObject.transform.Find("CanvasReward").gameObject.transform.Find("awsome").gameObject;
        awsomeSpriteRenderer = awsomeSprite.GetComponent<Image>();
        temp = awsomeSprite.transform.position;
        scratchScript = player.GetComponent<ScratchScript>();
        CanvasJoystick = GameObject.Find("GamePad Canvas").transform.Find("Image").gameObject;
        obstacle = GetComponent<NavMeshObstacle>();

        slider.value = 0.0f;

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) { isPressed = true; }
        else if (Input.GetMouseButtonUp(0)) { isPressed = false; }
        if (!isSpriteChanged && isItOn)
        {
            sprayingScript.SprayToSprite(sprite);
            isSpriteChanged = sprayingScript.isItSuccsesful;
            
        }

        obstacle.enabled = false;
        isItLocked = false;
        if (isItOn && !didIEarn && isSpriteChanged)
        {
            if (isPressed || scratchScript.SprayablePos)
            {
                Vector3 targetDir = scratchScript.HitPoint - player.transform.position;
                targetDir.y = 0;
                float step = 5 * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(player.transform.forward, targetDir, step, 0.0F);
                player.transform.rotation = Quaternion.LookRotation(newDir);
                if (buyingSupport.isItSelled)
                {
                    targetDir = scratchScript.HitPoint - support.transform.position;
                    targetDir.y = 0;
                    step = 5 * Time.deltaTime;
                    newDir = Vector3.RotateTowards(support.transform.forward, targetDir, step, 0.0F);
                    support.transform.rotation = Quaternion.LookRotation(newDir);
                }
            }
            else
            {
                Vector3 targetDir = transform.position - player.transform.position;
                targetDir.y = 0;
                float step = 5 * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(player.transform.forward, targetDir, step, 0.0F);
                player.transform.rotation = Quaternion.LookRotation(newDir);
                if (buyingSupport.isItSelled)
                {
                    targetDir = transform.position - support.transform.position;
                    targetDir.y = 0;
                    step = 5 * Time.deltaTime;
                    newDir = Vector3.RotateTowards(support.transform.forward, targetDir, step, 0.0F);
                    support.transform.rotation = Quaternion.LookRotation(newDir);
                }
            }

            player.transform.position = Vector3.Slerp(player.transform.position, new Vector3(transform.position.x - 1.9f, player.transform.position.y, transform.position.z - 1f), 5f * Time.deltaTime * PlayerPrefs.GetFloat(sprayingSpeedMultiplierID, 1));
            playerController.move = false;
            playerController.animator.SetBool("IsRunning", false);
            if (buyingSupport.isItSelled)
            {
                support.transform.position = Vector3.Slerp(support.transform.position, new Vector3(transform.position.x + 1.9f, support.transform.position.y, transform.position.z - 1f), 5f * Time.deltaTime * PlayerPrefs.GetFloat(sprayingSpeedMultiplierID, 1));
            }
            CanvasJoystick.SetActive(false);
            //supportAnimator.SetBool("IsRunning", false);
            obstacle.enabled = true;
            isItLocked = true;
            if (isPressed)
            {
                isSpraying = true;
            }
        }

        if (progress<1f && isItOn && !didIEarn && isPressed && cameraFollower.sprayable && scratchScript.SprayablePos && scratchScript.SprayablePosEmpty)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + 1.5f * Time.deltaTime);
            slider.value = progress;

            isSpraying = true;
            progress = scratchScript.cursor / 60f;
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
        }
        else if (isSpriteChanged && !didIEarn && progress >= 1f)
        {
            isSpraying = false;
            GraffitiIsDone();
            sprite.transform.parent.transform.Find("Canvas").gameObject.SetActive(false);
            isAwsomeSpriteOn = true;
            awsomeSpriteRenderer.sprite = awsomeSprites[Random.Range(0, awsomeSprites.Length)];
            fadeOutSpeed = fadeOutSpeedTemp;
            awsomeSprite.transform.position = temp;
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
        }
        else
        {
            isSpraying = false;
        }

        if (didIEarn)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (Time.deltaTime / SpawnTime));
            if (spriteRenderer.color.a <= 0)
            {
                isSpriteChanged = false;
                didIEarn = false;
                sprite.transform.parent.transform.Find("Canvas").gameObject.SetActive(false);
            }
        }
        if (isSpriteChanged)
        {
            Arrow.SetActive(false);
        }
        else
        {
            Arrow.SetActive(true);
        }




        if (collectManager.sprayListCursor < 0)
        {
            Arrow.SetActive(false);
        }

        //its fade out effect for rewarding player
        if (isAwsomeSpriteOn)
        {
            awsomeSpriteRenderer.enabled = true;
            awsomeSprite.transform.localScale = Vector3.Lerp(awsomeSprite.transform.localScale, new Vector3(7f, 7f, 7f), Time.deltaTime*4f);

            awsomeSprite.transform.position = Vector3.Lerp(awsomeSprite.transform.position, new Vector3(awsomeSprite.transform.position.x, awsomeSprite.transform.position.y + 3, awsomeSprite.transform.position.z), Time.deltaTime*1.75f);

            awsomeSpriteRenderer.color = new Color(1, 1, 1, awsomeSpriteRenderer.color.a - (Time.deltaTime/fadeOutSpeed));
            fadeOutSpeed *= fadeOutSpeedMultiplier;
            if(awsomeSpriteRenderer.color.a <= 0)
            {
                awsomeSprite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                awsomeSpriteRenderer.color = new Color(1, 1, 1, 1);
                isAwsomeSpriteOn = false;
                awsomeSpriteRenderer.enabled = false;
            }
        }

    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isItOn = true;
            if (!didIEarn)
            {
                if (collectManager.sprayListCursor > -1)
                {
                    //playerController.animator.SetTrigger("SprayStart");
                    //if(buyingSupport.isItSelled)
                    //    supportAnimator.SetTrigger("SprayStart");
                }
            }
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isItOn = false;
            isSpraying = false;
            OutOfStock.enabled = false;
        }
    }
    public void GraffitiIsDone()
    {
        coin.AddValue(EarningCoinAmount*sprayingScript.levelOfEXP);
        didIEarn = true;
        CanvasJoystick.SetActive(true);
        progress = 0;
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        spriteRenderer.gameObject.transform.Find("SilikImage").gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void UpdateSlider(float value)
    {
        slider.value = value;
        colorr.color = new Color(1 - value, value, 0, 1);
    }
}
