using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KoyluColisionDetect : MonoBehaviour
{
    GameObject koylu;
    float t;
    string name2;
    public KoyluAI koyluAI;
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
            if (counter > 10)
                name2 += Char.ToString(char1);
        }

        koylu = GameObject.Find("Halk" + name2);
        koyluAI = koylu.GetComponent<KoyluAI>();
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

            if (length > 0)
            {
                koyluAI.RandomWaypoint();
            }
            length = 0;
        }
        t -= Time.deltaTime;
    }
}
