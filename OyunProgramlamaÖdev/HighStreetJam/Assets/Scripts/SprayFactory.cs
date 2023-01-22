using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayFactory : MonoBehaviour
{
    public List<GameObject> SprayList = new List<GameObject>();
    public GameObject sprayPrefab;
    public Transform exitPoint;
    bool isWorking;
    public Material SprayCanMaterial;
    private float t;


    [HideInInspector] public  Material[] materialss;
  
  

    void Start()
    {
        t = 0.4f;
        StartCoroutine(PrintSpray());
    }
    public void RemoveLast()
    {
        if (SprayList.Count > 0)
        {
            Destroy(SprayList[SprayList.Count - 1]);
            SprayList.RemoveAt(SprayList.Count - 1);
        }
    }
    IEnumerator PrintSpray()
    {


           


           while (true)
           {
                float sprayCount = SprayList.Count;
           
           if (isWorking == true)
           {
                GameObject temp = Instantiate(sprayPrefab);

                materialss = temp.GetComponent<MeshRenderer>().materials;
                materialss[0] = SprayCanMaterial;
                temp.GetComponent<MeshRenderer>().materials = materialss;

                temp.transform.position = new Vector3(exitPoint.position.x, (float)SprayList.Count/5.0f + exitPoint.position.y, exitPoint.position.z);
                SprayList.Add(temp);
                if(SprayList.Count >= 10)
                {
                    isWorking = false;
                }

           }
           else if(SprayList.Count<10) 
           {
                isWorking = true;
           }
            t -= Time.deltaTime;
           if(t<0)
                yield return new WaitForSeconds(Random.Range(3, 15));
           else
                yield return new WaitForSeconds(0.1f);
        }
     }
        


    
}



