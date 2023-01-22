using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallArrow : MonoBehaviour // aslinda upgrade area arrow
{
    [HideInInspector] private Coin coin;
    CollectManager collectManager;
    public GameObject BaseUnlockControl;
    private MeshRenderer ShowZone;
    public GameObject[] UnlockArea;
    int n;


    private void Start()
    {
       
        coin = GameObject.FindGameObjectWithTag("Coin").GetComponent<Coin>();
        ShowZone = gameObject.GetComponent<MeshRenderer>();
        collectManager = GameObject.Find("Player").transform.GetComponent<CollectManager>();
        
    }
    private void Update()
    {
      
        if (coin.coinAmount >= 2000 && collectManager.sprayListCursor >= 0 )
        {
            Vector3 targetPosition = UnlockArea[n].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);

            ShowZone.enabled = true;
        }

        //else if (BaseUnlockControl.activeInHierarchy == true)
        //{   
        //    ShowZone.enabled = false;
        //}

    transform.eulerAngles = new Vector3(180f, transform.eulerAngles.y, 0f);
    }
    void GenerateRandom()
    {
       
        n = Random.Range(0, UnlockArea.Length); 
    }
}