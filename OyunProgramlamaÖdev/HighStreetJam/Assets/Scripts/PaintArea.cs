using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintArea : MonoBehaviour
{

    public List<GameObject> sprayList = new List<GameObject>();
    List<GameObject> moneyList = new List<GameObject>();


    public Transform sprayPoint, moneyDropPoint;
    public GameObject sprayPrefab, moneyPrefab;

    private void Start()
    {
        StartCoroutine(GenerateMoney());
    }


    IEnumerator GenerateMoney ()
    {

        while (true)
        {

            if (sprayList.Count > 0)
            {

                GameObject temp = Instantiate(moneyPrefab);
                temp.transform.position = new Vector3(moneyDropPoint.position.x, 0.8f + ((float)moneyList.Count) / 20, sprayPoint.position.z);
                moneyList.Add(temp);
                RemoveLast();

            }

            yield return new WaitForSeconds(0.05f);
        }

    }


    public void GetSpray()
    {
        GameObject temp = Instantiate(sprayPrefab);
        temp.transform.position = new Vector3 (sprayPoint.position.x, 0.8f + ((float)sprayList.Count), sprayPoint.position.z);
        sprayList.Add(temp);
    }

    public void RemoveLast()
    {
        if (sprayList.Count > 0)
        {
            Destroy(sprayList[sprayList.Count - 1]);
            sprayList.RemoveAt(sprayList.Count - 1);
        }
    }
}
