using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Your arrow sprite must be pointing right.
// Must be child of the player.
public class ArrowPointer : MonoBehaviour
{

    public Transform Target;
    public float HideToDistance;

    void Update()
    {

        var dir = Target.position - transform.position;
        if (dir.magnitude < HideToDistance)
        {
            SetChildrenActive(false);
        }


        else
        {
            SetChildrenActive(true);

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }




    

    void SetChildrenActive (bool value)
    {

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }


    }


}