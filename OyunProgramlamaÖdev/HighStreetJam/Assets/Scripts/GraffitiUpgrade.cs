using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Lofelt.NiceVibrations;

public class GraffitiUpgrade : MonoBehaviour
{
    [HideInInspector] private Coin coin;

    int priceOfStyle = 200;
    string priceOfStyleID = "priceOfStyle";

    PlayerController playerController;
    GlobalVariables globalVariables;
    Image UpgradeBg;
    GameObject UpgradeTable;
    Text textOfPrice;

    bool isItOn = false;
    [HideInInspector] public bool isItSelled = false;

    // Start is called before the first frame update
    void Start()
    {
        priceOfStyle = PlayerPrefs.GetInt(priceOfStyleID, priceOfStyle);
        textOfPrice = GameObject.Find("StyleText").GetComponent<Text>();
        textOfPrice.text = priceOfStyle.ToString();
        coin = GameObject.FindGameObjectWithTag("Coin").GetComponent<Coin>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        globalVariables = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
        UpgradeBg = GameObject.Find("ImageBg").gameObject.GetComponent<Image>();
        UpgradeTable = GameObject.Find("Upgrade").gameObject;


        if (PlayerPrefs.GetInt("SelledGraffiti", 0) >= 1)
            isItSelled = true;
        else
            isItSelled = false;
    }

    //if time is done and we are on trigger then buy the support
    void Update()
    {
        if (isItOn)
        {
            UpgradeTable.transform.localScale = Vector3.Lerp(UpgradeTable.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 7);
            if (UpgradeBg.color.a < 0.5)
                UpgradeBg.color = new Color(UpgradeBg.color.r, UpgradeBg.color.g, UpgradeBg.color.b, UpgradeBg.color.a + (3 * Time.deltaTime));

        }
        else
        {
            UpgradeTable.transform.localScale = Vector3.Lerp(UpgradeTable.transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * 7);
            if(UpgradeBg.color.a > 0)
                UpgradeBg.color = new Color(UpgradeBg.color.r, UpgradeBg.color.g, UpgradeBg.color.b, UpgradeBg.color.a - (3 * Time.deltaTime));
        }

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isItOn = true;
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
    public void buying()
    {
        if (!isItSelled)
        {
            if (coin.coinAmount >= priceOfStyle)
            {
                PlayerPrefs.SetInt("SelledGraffiti", 1);
                Debug.Log("You buyed the Upgrade");
                coin.AddValue(-priceOfStyle);
                GameObject.Find("Player").GetComponent<Spraying>().UpgradeLevel();
                isItSelled = true;
                priceOfStyle *= 2;
                PlayerPrefs.SetInt(priceOfStyleID, priceOfStyle);
                textOfPrice.text = priceOfStyle.ToString();
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
            }
            else
            {
                Debug.Log("Your coins are not enough");
            }
            
        }
    }

    public void Out()
    {
        isItOn = false;
    }
}
