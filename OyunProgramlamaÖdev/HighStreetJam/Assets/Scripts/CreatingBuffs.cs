using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingBuffs : MonoBehaviour
{
    public GameObject[] spawnlanacakObjeler;


    private void Start()
    {
        StartCoroutine(CreateBuff());
    
    }
    IEnumerator CreateBuff()
    {
        Vector3 spawnBuffArea = new Vector3(1, 1, 1);
        Vector3 spawnBuffArea1 = new Vector3(2, 2, 1);

      
        Instantiate(spawnlanacakObjeler[Random.Range(0, 1)], spawnBuffArea, Quaternion.identity);
        Instantiate(spawnlanacakObjeler[Random.Range(0, 1)], spawnBuffArea1, Quaternion.identity);
        yield return new WaitForSeconds(3f);
    }
}
