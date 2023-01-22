using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Lofelt.NiceVibrations;

public class CollectManager : MonoBehaviour
{

    public GameObject[] sprayList;
    [HideInInspector] public int sprayListCursor;

    public GameObject sprayPrefab;
    PlayerController playerController;
    public ParticleSystem fallDownParticle;
    Animator animator;
    public Transform collectPoint;
    Coin coin;

    private string LimitUpgradeID = "Limit";

    public float SprayOffsetRotationX;
    public float SprayOffsetRotationY;
    public float SprayOffsetRotationZ;

    private List<IEnumerator> co;
    private int coCursor;

    int priceOfsprayLimitUpgrade = 75;
    string priceOfsprayLimitUpgradeID = "priceLimit";
    Text textOfPrice;

    int sprayLimit = 10;
    public float offSetSpeedY = 0.0025f;
    public float offSetSpeed = 1.5f;
    public float offSetY = -1;

    [HideInInspector] public Material[] materialss2;

    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        animator = gameObject.transform.Find("Mesh").GetComponent<Animator>();
        textOfPrice = GameObject.Find("CapacityText").GetComponent<Text>();
        priceOfsprayLimitUpgrade = PlayerPrefs.GetInt(priceOfsprayLimitUpgradeID, 75);
        co = new List<IEnumerator>();
        sprayList = new GameObject[100];
        coCursor = -1;
        sprayListCursor = -1;
        coin = GameObject.Find("Coin").GetComponent<Coin>();
        sprayLimit = PlayerPrefs.GetInt(LimitUpgradeID, 10);
        textOfPrice.text = priceOfsprayLimitUpgrade.ToString();
    }

    public void GetSpray(GameObject sprayFac, Material materialOfSpray)
    {
        SprayFactory factory = sprayFac.GetComponent<SprayFactory>();
        if (factory.SprayList.Count > 0)
        {
            if (sprayListCursor < sprayLimit)
            {
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);

                GameObject temp = Instantiate(sprayPrefab, sprayFac.transform.Find("Exit").transform.position, new Quaternion(0, 0, 0, 0));

                materialss2 = temp.GetComponent<MeshRenderer>().materials;
                materialss2[0] = materialOfSpray;
                temp.GetComponent<MeshRenderer>().materials = materialss2;


                sprayListCursor++;
                sprayList[sprayListCursor] = temp;

                if (factory.SprayList.Count > 0)
                {
                    factory.RemoveLast();
                }
                coCursor++;

                co.Insert(coCursor, pickSpray(temp));
                StartCoroutine(co[coCursor]);
            }
        }
        
    }

    public GameObject LastSpray()
    {
        return sprayList[sprayListCursor];
    }

    public void RemoveSpray()
    {
        if(sprayListCursor < 0) { return; }
        GameObject Temp = sprayList[sprayListCursor];
        sprayListCursor--;
        Destroy(Temp);
    }

    public bool isListEmpty()
    {
        return sprayListCursor < 0;
    }

    //for animation
    public void DropingSprays()
    {
        fallDownParticle.Play();
        playerController.particlPrefabSup.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        playerController.particlPrefab.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        animator.SetBool("Drop", true);
        animator.SetBool("isSprayinh", false);
        playerController.enabled = false;
        while (sprayListCursor > -1) { 
        GameObject Temp = sprayList[sprayListCursor];
        DropingSpray vars = Temp.GetComponent<DropingSpray>();
        sprayListCursor--;
        vars.isDropped = true;
        StopCoroutine(co[coCursor]);
        coCursor--;
        }
    }

    public void sprayLimitUpgrade() 
    {
        if (coin.coinAmount >= priceOfsprayLimitUpgrade)
        {
            sprayLimit += 5;
            coin.AddValue(-priceOfsprayLimitUpgrade);
            PlayerPrefs.SetInt(LimitUpgradeID, sprayLimit);
            priceOfsprayLimitUpgrade *= 2;
            PlayerPrefs.SetInt(priceOfsprayLimitUpgradeID, priceOfsprayLimitUpgrade);
            textOfPrice.text = priceOfsprayLimitUpgrade.ToString();
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
        }
        
    }

    IEnumerator pickSpray(GameObject temp)
    {
        

        float index = sprayListCursor;
        //higher sprays move slow
        float speed = 1 / (index + 15);

        float speedOfY = - temp.transform.position.y;

        DropingSpray vars = temp.GetComponent<DropingSpray>();

        Transform offset = gameObject.transform.Find("CollectPoint").transform;

        while (temp != null)
        {
            if (vars.isDropped == true) { yield break; }


            Vector3 tempDest = new Vector3(collectPoint.position.x, index / offSetY + offset.position.y, collectPoint.position.z);
            Vector3 Destination = tempDest - temp.transform.right * index * 0.03f;


            //we use this extra lerp beacuse sin wave effect

            if (Destination.y - temp.transform.position.y > 0.3)
                temp.transform.position = Vector3.Lerp(temp.transform.position, new Vector3(temp.transform.position.x, Destination.y, temp.transform.position.z), Mathf.Sin(speed * Mathf.PI) * speedOfY * offSetSpeedY * Time.deltaTime);

            temp.transform.position = Vector3.Lerp(temp.transform.position, Destination, (Mathf.Sin(speed * Mathf.PI) / 15) * offSetSpeed * Time.deltaTime);

            Quaternion rotation = Quaternion.Euler(new Vector3(SprayOffsetRotationX + gameObject.transform.eulerAngles.x, SprayOffsetRotationY + gameObject.transform.eulerAngles.y, SprayOffsetRotationZ + gameObject.transform.eulerAngles.z));
            temp.transform.rotation = Quaternion.Lerp(temp.transform.rotation,rotation, 0.12f * Time.deltaTime * 60); 

            yield return null;
        }

    }

}