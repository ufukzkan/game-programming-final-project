using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{


    public delegate void OnCollectArea();
    public static event OnCollectArea OnSprayCollect;
    public static SprayFactory sprayFactory;

    public delegate void OnFacArea();
    public static event OnFacArea OnSprayGive;
    public static PaintArea paintArea;

    public delegate void OnMoneyArea();
    public static event OnMoneyArea OnMoneyCollected;

    public delegate void OnBuyArea();
    public static event OnBuyArea OnBuyingLand;

    public static BuyArea areaToBuy;

    bool isCollecting = false;
    CollectManager collMan;



    private void Start()
    {
        collMan = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectManager>();
        StartCoroutine(CollectEnum());
    }

    IEnumerator CollectEnum()
    {
        while(true)
        {
            if (isCollecting == true)
            {
                collMan.GetSpray(gameObject, gameObject.GetComponent<SprayFactory>().SprayCanMaterial);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isCollecting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollecting = false;
        }

    }


}


