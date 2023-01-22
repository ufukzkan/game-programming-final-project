using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnlockNewLand : MonoBehaviour
{
    public Text costText;
    public GameObject landGameObject, buyGameObject;
    public float cost, progress;



    Coin coin;

    void Start()
    {
        coin = GetComponent<Coin>();
     
       
     

    }


    private void Update()

    {
        costText.text = (cost - coin.coinAmount).ToString();
    }


    public void Unlock(int increaseAmount)
    {
        coin.coinAmount += increaseAmount;
        progress = coin.coinAmount / cost;

        if (progress >= 1)
        {
            buyGameObject.SetActive(false);
            landGameObject.SetActive(true);
            this.enabled = false;
        }
    }
}

