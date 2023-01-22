using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KoyluAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform low;
    public Transform high;
    public Transform target;
    private NavMeshPath path;
    public Material[] materials;
    SkinnedMeshRenderer meshRenderer;

    Vector3 newTarget;
    string name2;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = transform;
        string name1 = gameObject.name.ToString();

        int counter = 0;
        foreach (char char1 in name1)
        {
            counter++;
            if (counter > 4)
                name2 += Char.ToString(char1);
        }


        low = GameObject.Find("lowHalk" + name2).GetComponent<Transform>();
        high = GameObject.Find("highHalk" + name2).GetComponent<Transform>();
        target = GameObject.Find("targetHalk" + name2).GetComponent<Transform>();

        newTarget = target.position;

        //meshRenderer = gameObject.transform.Find("model").transform.Find("materailModel").GetComponent<SkinnedMeshRenderer>();
        //Material[] materialss2 = meshRenderer.materials;
        //materialss2[0] = materials[UnityEngine.Random.Range(0, materials.Length)];
        //meshRenderer.materials = materialss2;

        path = new NavMeshPath();
        RandomWaypoint();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < 1)
        {
            RandomWaypoint();
        }


        while (!NavMesh.CalculatePath(agent.gameObject.transform.position, target.position, NavMesh.AllAreas, path)) { RandomWaypoint(); }
        agent.SetPath(path);
    }
    public void RandomWaypoint()
    {

        newTarget = new Vector3(UnityEngine.Random.Range(low.position.x, high.position.x), 1f, UnityEngine.Random.Range(low.position.z, high.position.z));

        target.position = newTarget;
    }
}
