using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyArea : MonoBehaviour
{

    public GameObject landGameObject, buyGameObject;
    public float cost, currentCoin, progress;

    public void Buy(int goldAmount)
    {
        currentCoin += goldAmount;
        progress = currentCoin / cost;
        if(progress >= 1)
        {
            buyGameObject.SetActive(false);
            landGameObject.SetActive(true);
            this.enabled = false; 
        }
    }

}
