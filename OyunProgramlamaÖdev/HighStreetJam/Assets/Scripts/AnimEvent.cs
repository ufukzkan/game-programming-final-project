using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{

    PlayerController playerController;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.transform.parent.GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void controllerEnabled()
    {
        playerController.enabled = true;
    }
    public void animFalse()
    {
        animator.SetBool("Drop", false);
    }
}
