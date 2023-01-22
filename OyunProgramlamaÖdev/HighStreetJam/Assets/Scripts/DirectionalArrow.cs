using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] private Transform target;
    CollectManager collectManager;
    private MeshRenderer ShowZone;

    private Coin coin;
    public GameObject UnlockTarget;
    public bool isItFirstUnlock;

    private void Start()
    {
        if (PlayerPrefs.GetInt("isItFirstUnlock", 1) >= 1)
            isItFirstUnlock = true;
        else
            isItFirstUnlock = false;

        coin = GameObject.FindGameObjectWithTag("Coin").GetComponent<Coin>();
        ShowZone = gameObject.transform.Find("Arrow").GetComponent<MeshRenderer>();
        collectManager = GameObject.Find("Player").transform.GetComponent<CollectManager>();
    }
    private void Update()
    {
        if(isItFirstUnlock && coin.coinAmount >= 100)
        {
            Vector3 targetPosition = UnlockTarget.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);

            ShowZone.enabled = true;
        }

        else if (collectManager.sprayListCursor <= -1)
        {
            Vector3 targetPosition = target.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);

            ShowZone.enabled = true;
        }

        else
        {
            ShowZone.enabled = false;
        }





        transform.eulerAngles = new Vector3(180f, transform.eulerAngles.y, 0f);
    }
}