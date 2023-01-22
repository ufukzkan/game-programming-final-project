using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingLand : MonoBehaviour
{


    public GameObject landGameObject, buyGameObject;
    public ParticleSystem partyParticle;

    [HideInInspector] private Coin coin;

    int priceOfLand = 150;
    float buyingTime;

    float startTime = 0f;
    float counter = 0f;

    PlayerController playerController;
    GlobalVariables globalVariables;

    bool isItOn = false;
    [HideInInspector] public bool isItSelled = false;

    // Start is called before the first frame update
    void Start()
    {
        coin = GameObject.Find("Coin").GetComponent<Coin>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        globalVariables = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();

        buyingTime = globalVariables.takingTime;
    }

    //if time is done and we are on trigger then buy the support
    void Update()
    {
        counter = Time.time - startTime;
        if (buyingTime < counter && isItOn)
        {
            TimeIsDone();
        }

        //if our player moved then the timer reset
        if (playerController.horizontal != 0 || playerController.vertical != 0)
            startTime = Time.time;
    }
    //when we enter trigger timer is on
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isItOn = true;
            startTime = Time.time;
        }
    }
    //when we exit trigger, the boolean variable which is helping us to understand is it on the  trigger is getting false
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isItOn = false;
        }
    }

    //if time is done then buy it and reduce drawing time
    public void TimeIsDone()
    {

        {
            if (!isItSelled)
            {
                if (coin.coinAmount >= priceOfLand)
                {
                    Debug.Log("You buyed the new land");
                    coin.AddValue(-priceOfLand);
                    isItSelled = true;

                    partyParticle.Play();


                    buyGameObject.SetActive(false);
                    landGameObject.SetActive(true);
                    this.enabled = false;
                }
            }

            else
            {
                Debug.Log("Your coins are not enough");
            }
        }

    }







}