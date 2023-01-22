using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Lofelt.NiceVibrations;

public class BuyingSupport : MonoBehaviour
{


    [HideInInspector] private Coin coin;

    string priceOfSupportID = "priceSupport";
    int priceOfSupport = 300;

    Text textOfPrice;

    PlayerController playerController;
    SupportNavMesh supportNavMesh;

    [HideInInspector] public bool isItSelled = false;

    void Start()
    {
        priceOfSupport = PlayerPrefs.GetInt(priceOfSupportID, priceOfSupport);
        textOfPrice = GameObject.Find("StaffText").GetComponent<Text>();
        textOfPrice.text = priceOfSupport.ToString();
        coin = GameObject.Find("Coin").GetComponent<Coin>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        supportNavMesh = transform.GetComponent<SupportNavMesh>();

        if (PlayerPrefs.GetInt("Selled", 0) >= 1)
            isItSelled = true;
        else
            isItSelled = false;

        if (isItSelled)
        {
            supportNavMesh.enabled = true;
        }
        else
        {
            supportNavMesh.enabled = false;
        }
    }

    
    public void buying()
    {
        gameObject.SetActive(true);
        if (!isItSelled)
        {
            if (coin.coinAmount >= priceOfSupport)
            {
                PlayerPrefs.SetInt("Selled", 1);
                Debug.Log("You buyed the Support");
                coin.AddValue(-priceOfSupport);
                playerController.drawingTime = playerController.drawingTime / 2;
                isItSelled = true;
                priceOfSupport *= 2;
                PlayerPrefs.SetInt(priceOfSupportID, priceOfSupport);
                textOfPrice.text = priceOfSupport.ToString();
                supportNavMesh.enabled = true;
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
            }
            else
            {
                Debug.Log("Your coins are not enough");
            }
            
        }
       

    }
        

    

}
