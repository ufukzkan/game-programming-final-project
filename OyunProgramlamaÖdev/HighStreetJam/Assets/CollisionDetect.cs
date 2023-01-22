using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionDetect : MonoBehaviour
{

    GameObject police;
    bool isItOn = false;
    float t;
    string name2;
    PoliceAI policeAI;
    ChaseRange range;
    float length = 0;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        string name1 = gameObject.name.ToString();

        int counter = 0;
        foreach (char char1 in name1)
        {
            counter++;
            if (counter > 6)
                name2 += Char.ToString(char1);
        }

        police = GameObject.Find("police"+ name2);
        policeAI = police.GetComponent<PoliceAI>();
        range = police.GetComponent<ChaseRange>();
    }

    private void Update()
    {
        if (t < 0)
        {
            t = 0.05f;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);

            foreach (Collider collider in hitColliders)
            {
                if (collider.name != "Player" && collider.name != "SupportCharacter" && !collider.CompareTag("police") && !collider.CompareTag("halk")) { length++; }
            }

            if (length > 1)
            {
                if(!range.isChasing)
                    policeAI.RandomWaypoint();
            }
            length = 0;
        }
        t -= Time.deltaTime;
    }
}
