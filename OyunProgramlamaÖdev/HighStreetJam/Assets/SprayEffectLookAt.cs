using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayEffectLookAt : MonoBehaviour
{
    Vector3 StartRot;
    Vector3 LookAtPos;
    private ScratchScript scratchScript;

    PlayerController controller;
    bool mouseDown = false;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        StartRot = transform.localEulerAngles;
        controller = GameObject.Find("Player").gameObject.GetComponent<PlayerController>();
        scratchScript = GameObject.Find("Player").gameObject.GetComponent<ScratchScript>();
        cam = GameObject.Find("Camera").gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(scratchScript.HitPoint);
    }
}
