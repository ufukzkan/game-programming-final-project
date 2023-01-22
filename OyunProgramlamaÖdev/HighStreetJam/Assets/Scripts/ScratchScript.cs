using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchScript : MonoBehaviour
{
    SprayingTrigger sprayingTrigger;
    PlayerController playerController;
    public LayerMask layerMask;
    public LayerMask layerMaskSpriteMask;
    public LayerMask layerForRay;
    public GameObject MaskPrefab;
    private bool isPressed;
    [HideInInspector] public bool SprayablePos;
    [HideInInspector] public bool SprayablePosEmpty;
    private Vector3 mousePos;
    [HideInInspector] public Vector3 HitPoint;
    GameObject[] gameObjects;
    float cooldown = 0;
    [HideInInspector] public int cursor = -1;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameObjects = new GameObject[200];
    }

    void Update()
    {
        sprayingTrigger = playerController.WhichWallTrigger;

        if (Input.GetMouseButtonDown(0)) { isPressed = true; }
        else if (Input.GetMouseButtonUp(0)) { isPressed = false; }

        SprayablePosEmpty = true;

        RaycastHit hit1;
        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray1, out hit1, 100, layerMaskSpriteMask))
        {
            if (hit1.transform.gameObject.CompareTag("SpriteMask"))
            {
                SprayablePosEmpty = false;
            }
        }

        SprayablePos = false;

        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D[] hit2Ds = Physics2D.GetRayIntersectionAll(ray2, 100, layerMask);
        foreach(var hit2D in hit2Ds)
        {
            if (hit2D.collider != null)
            {
                Debug.Log(hit2D.transform.gameObject.name);
                if (hit2D.transform.gameObject.CompareTag("Sprite"))
                {
                    SprayablePos = true;
                    HitPoint = hit2D.point;
                    //HitPoint.z = 
                }
            }
        }
        
        RaycastHit hit3;
        Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray3, out hit3, 100, layerForRay))
        {
            HitPoint.z = hit3.point.z;
        }

        if (sprayingTrigger != null && isPressed && sprayingTrigger.isSpraying && !sprayingTrigger.didIEarn && SprayablePos && SprayablePosEmpty)
        {
            sprayingTrigger.spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            cursor++;
            gameObjects[cursor] = Instantiate(MaskPrefab, HitPoint, Quaternion.identity);
            Debug.Log(HitPoint);
        }

        if (sprayingTrigger != null && sprayingTrigger.didIEarn)
        {
            sprayingTrigger.spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
            if (cursor > -1) 
            {
                foreach (GameObject x in gameObjects)
                {
                    Destroy(x);
                }
                cursor = -1;
            }
        }
    }
}
