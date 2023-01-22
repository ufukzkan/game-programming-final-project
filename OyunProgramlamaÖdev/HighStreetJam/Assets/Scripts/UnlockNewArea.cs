using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class UnlockNewArea : MonoBehaviour
{
    public ParticleSystem partyParticle;
    public int PriceOfArea = 100;
    private int tempPrice;
    public GameObject Area;
    PlayerController playerController;
    GameObject player;
    PointerArrow pointerArrow;
    Coin coin;
    Text priceText;
    string priceOfAreaID;

    public GameObject colliders;
    public GameObject coinSprite;
    private NavMeshAgent navMeshAgent;

    string isItUnlockedID;

    private bool isItOn = false;
    private bool isItSelled;
    private bool isCoroutineStart = false;

    float startT = 0;
    float T = 0;
    float cooldownTime = 1f;
    bool isDone = false;
    float t2;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GameObject.Find("SupportCharacter").gameObject.GetComponent<NavMeshAgent>();
        priceOfAreaID = gameObject.name;
        PriceOfArea = PlayerPrefs.GetInt(priceOfAreaID, PriceOfArea);
        cooldownTime = 0.5f;
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        coin = GameObject.FindGameObjectWithTag("Coin").GetComponent<Coin>();
        priceText = gameObject.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        isItUnlockedID = Area.name;
        priceText.text = PriceOfArea.ToString();
        pointerArrow = FindObjectOfType<PointerArrow>();
        tempPrice = PriceOfArea;

        if (PlayerPrefs.GetInt(isItUnlockedID, 0) >= 1)
            isItSelled = true;
        else
            isItSelled = false;
    }

    // Update is called once per frame
    void Update()
    {
        T = Time.time - startT;
        if (isItSelled && !isCoroutineStart)
        {
            isCoroutineStart = true;

            if (colliders != null)
                colliders.SetActive(false);
            
            

            GameObject.FindObjectOfType<DirectionalArrow>().isItFirstUnlock = false;
            PlayerPrefs.SetInt("isItFirstUnlock", 0);
            Area.SetActive(true);
            playerController.sprayingTriggers = GameObject.FindObjectsOfType<SprayingTrigger>();
            pointerArrow.walls = GameObject.FindGameObjectsWithTag("wall");
            isDone = true;
            t2 = 0.01f;
        }

        if (isDone)
        {
            t2 -= Time.deltaTime;
            if (isItSelled && t2 < 0)
            {
                Destroy(gameObject);
            }
        }
        

        if ((playerController.horizontal == 0 || playerController.vertical == 0) && isItOn && !isCoroutineStart && !isItSelled)
        {
            if (PriceOfArea > 0)
            {
                if(coin.coinAmount > 0 && T > cooldownTime && tempPrice>0)
                {
                    StartCoroutine(DropingCoin());
                    startT = Time.time;
                    cooldownTime = cooldownTime / 2f;
                    tempPrice -= 5;
                }
            }
        }
        if(PriceOfArea <= 0)
        {
            isItSelled = true;
            PlayerPrefs.SetInt(isItUnlockedID, 1);
            partyParticle.Play();

        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isItOn = true;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isItOn = false;
        }
    }

    IEnumerator DropingCoin()
    {
        Vector3 Destination = new Vector3(gameObject.transform.position.x - 0.95f, gameObject.transform.position.y, gameObject.transform.position.z);

        GameObject temp = Instantiate(coinSprite, new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z), coinSprite.transform.rotation);

        while (true)
        {
            if (Area.activeSelf)
            {
                Destroy(temp);
                yield break;
            }

            if (Vector3.Distance(temp.transform.position, Destination) < 0.1)
            {
                if (coin.coinAmount > 0 && PriceOfArea > 0)
                {
                    coin.AddValue(-5);
                    PriceOfArea -= 5;
                    priceText.text = PriceOfArea.ToString();
                    PlayerPrefs.SetInt(priceOfAreaID, PriceOfArea);
                }
                Destroy(temp);
                yield break;
            }
            

            temp.transform.position = Vector3.Lerp(temp.transform.position, Destination, Time.deltaTime*4);

            yield return null;
        }
        
    }


}
