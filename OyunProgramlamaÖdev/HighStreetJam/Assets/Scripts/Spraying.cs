using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;
using System.Linq;

public class Spraying : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer spriteRenderer;

    private CollectManager collectManager;



    private Animator animator;
    private SpriteRenderer OutOfStock;

    public MeshRenderer playersCan;
    public ParticleSystem playersSpray;

    public MeshRenderer supportCan;
    public ParticleSystem supportSpray;

    public Sprite[] goodSprites;
    public Sprite[] badSprites;

    PolygonCollider2D polygonCollider;

    private List<Vector2> points = new List<Vector2>();
    private List<Vector2> simplifiedPoints = new List<Vector2>();

    public string levelID = "level";

    int randomNumber;
    public int levelOfEXP = 1;

    [HideInInspector] public bool isItSuccsesful = false;


    private void Awake()
    {
        OutOfStock = GameObject.Find("OutOfStock").gameObject.GetComponent<SpriteRenderer>();
        animator = OutOfStock.GetComponent<Animator>();
    }
    private void Start()
    {
        levelOfEXP = PlayerPrefs.GetInt(levelID, 1);
        collectManager = GetComponent<CollectManager>();
        goodSprites = Resources.LoadAll("GoodGraffiti", typeof(Sprite)).Cast<Sprite>().ToArray();
        badSprites = Resources.LoadAll("BadGraffiti", typeof(Sprite)).Cast<Sprite>().ToArray();
    }

    public void SprayToSprite(GameObject sprite)
    {

        if (collectManager.isListEmpty())
        {
            isItSuccsesful = false;
            OutOfStock.enabled = true;
            OutOfStock.GetComponent<SpriteRenderer>().enabled = true;

            return;
        }

        isItSuccsesful = true;

        sprite.transform.parent.transform.Find("Canvas").gameObject.SetActive(true);
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();


        Material[] materialss2 = playersCan.materials;
        Material[] materialss3 = collectManager.LastSpray().GetComponent<MeshRenderer>().materials;
        materialss2[0] = materialss3[0];
        playersCan.materials = materialss2;

        var playersSprayMain = playersSpray.main;
        playersSprayMain.startColor = materialss3[0].color;

        materialss2 = supportCan.materials;
        materialss3 = collectManager.LastSpray().GetComponent<MeshRenderer>().materials;
        materialss2[0] = materialss3[0];
        supportCan.materials = materialss2;

        var supportSprayMain = supportSpray.main;
        supportSprayMain.startColor = materialss3[0].color;



        collectManager.RemoveSpray();

        switch (levelOfEXP)
        {
            case 1:
                randomNumber = Random.Range(0, badSprites.Length);
                spriteRenderer.sprite = badSprites[randomNumber];
                spriteRenderer.gameObject.transform.Find("SilikImage").gameObject.GetComponentInChildren<SpriteRenderer>().sprite = spriteRenderer.sprite;
                spriteRenderer.gameObject.transform.Find("SilikImage").gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                break;
            case 2:
                randomNumber = Random.Range(0, goodSprites.Length);
                spriteRenderer.sprite = goodSprites[randomNumber];
                spriteRenderer.gameObject.transform.Find("SilikImage").gameObject.GetComponentInChildren<SpriteRenderer>().sprite = spriteRenderer.sprite;
                spriteRenderer.gameObject.transform.Find("SilikImage").gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                break;
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        polygonCollider = spriteRenderer.gameObject.GetComponent<PolygonCollider2D>();
        Destroy(polygonCollider);
        spriteRenderer.gameObject.AddComponent<PolygonCollider2D>();


        //var sprite1 = spriteRenderer.sprite;
        //List<Vector2> points = new List<Vector2>();
        //List<Vector2> simplifiedPoints = new List<Vector2>();
        //UpdatePolygonCollider2D();

    }

    public void UpgradeLevel()
    {
        levelOfEXP++;
        PlayerPrefs.SetInt(levelID, levelOfEXP);
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
    }

    public void UpdatePolygonCollider2D(float tolerance = 0.1f)
    {
        var sprite1 = spriteRenderer.sprite;
        polygonCollider.pathCount = sprite1.GetPhysicsShapeCount();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            sprite1.GetPhysicsShape(i, points);
            LineUtility.Simplify(points, tolerance, simplifiedPoints);
            polygonCollider.SetPath(i, simplifiedPoints);
        }
    }

}
