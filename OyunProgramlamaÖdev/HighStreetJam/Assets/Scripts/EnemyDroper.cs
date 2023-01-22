using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Lofelt.NiceVibrations;

public class EnemyDroper : MonoBehaviour
{
    private CollectManager collectManager;
    private PlayerController playerController;
    private bool isExited;
    [HideInInspector] public bool isPoliceChasing = false; 
    Animator animator;
    bool isKicked = false;
    SpriteRenderer KickSpriteRenderer;
    GameObject KickSprite;
    public float fadeOutSpeed;
    private float fadeOutSpeedTemp;
    public float fadeOutSpeedMultiplier;
    Sprite[] kickSprites;

    // Start is called before the first frame update
    void Start()
    {
        collectManager = GameObject.Find("Player").gameObject.GetComponent<CollectManager>();
        playerController = collectManager.gameObject.GetComponent<PlayerController>();
        isExited = true;
        animator = gameObject.transform.Find("PoliceMesh").GetComponent<Animator>();
        KickSprite = GameObject.Find("HitEffect");
        KickSpriteRenderer = KickSprite.GetComponent<SpriteRenderer>();
        kickSprites = Resources.LoadAll("kick", typeof(Sprite)).Cast<Sprite>().ToArray();
        fadeOutSpeedTemp = fadeOutSpeed;
    }
    private void Update()
    {
        if (isKicked)
        {
            KickSpriteRenderer.enabled = true;
            KickSprite.transform.localScale = Vector3.Lerp(KickSprite.transform.localScale, new Vector3(0.6f, 0.6f, 0.6f), Time.deltaTime * 5f);
            KickSpriteRenderer.color = new Color(KickSpriteRenderer.color.r, KickSpriteRenderer.color.g, KickSpriteRenderer.color.b, KickSpriteRenderer.color.a - (Time.deltaTime / fadeOutSpeed));
            fadeOutSpeed *= fadeOutSpeedMultiplier;
            if (KickSpriteRenderer.color.a <= 0)
            {
                KickSprite.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                KickSpriteRenderer.color = new Color(1, 1, 1, 1);
                KickSpriteRenderer.enabled = false;
                isKicked = false;
            }
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            if (!isExited && !playerController.isItOn || (playerController.WhichWallTrigger != null && playerController.WhichWallTrigger.didIEarn))
            {
                collectManager.DropingSprays();
                isPoliceChasing = true;
                animator.SetTrigger("Punch");
                isKicked = true;
                KickSpriteRenderer.sprite = kickSprites[Random.Range(0, kickSprites.Length)];
                fadeOutSpeed *= fadeOutSpeedTemp;
                foreach (GameObject police in GameObject.FindGameObjectsWithTag("police")) { police.GetComponent<ChaseRange>().lastT = Time.time; police.GetComponent<EnemyDroper>().isPoliceChasing = false; }
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
            }
            
            isExited = false;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isExited = true;
        }
    }
}
