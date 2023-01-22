using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class PointerArrow : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    private RectTransform pointerRectTransform2;
    private RectTransform pointerRectTransform3;

    private Image Image1;
    private Image Image2;
    private Image Image3;

    private Camera cam;
    private GameObject Player;
    public GameObject Target;
    public GameObject Target2;
    public GameObject Target3;
    public GameObject[] walls;
    
    CollectManager collectManager;

    float counter = 0f;
    float borderSize = 100f;


    private void Awake()
    {
        pointerRectTransform = transform.Find("a1").GetComponent<RectTransform>();
        pointerRectTransform2 = transform.Find("a2").GetComponent<RectTransform>();
        pointerRectTransform3 = transform.Find("a3").GetComponent<RectTransform>();

        Image1 = transform.Find("a1").GetComponent<Image>();
        Image2 = transform.Find("a2").GetComponent<Image>();
        Image3 = transform.Find("a3").GetComponent<Image>();

        cam = GameObject.Find("Camera").GetComponent<Camera>();
        Player = GameObject.Find("Player");
        walls = GameObject.FindGameObjectsWithTag("wall");
        collectManager = Player.GetComponent<CollectManager>();
    }

    void Update()
    {
        if (collectManager.sprayListCursor >= 0)
        {
            Image1.enabled = true;
            Image2.enabled = true;
            Image3.enabled = true;


            float oldDistance = Vector3.Distance(Target.transform.position, Player.transform.position);
            float oldDistance2 = Vector3.Distance(Target2.transform.position, Player.transform.position);
            float oldDistance3 = Vector3.Distance(Target3.transform.position, Player.transform.position);

            counter -= Time.deltaTime;
            if (counter < 0f)
            {
                counter = 0.4f;
                foreach (GameObject wall in walls)
                {
                    if (Target != null && Target.transform.Find("Trigger(Script)").GetComponent<SprayingTrigger>().isSpriteChanged) { oldDistance += 1000000000; }
                    if (Target2 != null && Target2.transform.Find("Trigger(Script)").GetComponent<SprayingTrigger>().isSpriteChanged) { oldDistance2 += 1000000000; }
                    if (Target3 != null && Target3.transform.Find("Trigger(Script)").GetComponent<SprayingTrigger>().isSpriteChanged) { oldDistance3 += 1000000000; }

                    if (!wall.transform.Find("Trigger(Script)").GetComponent<SprayingTrigger>().isSpriteChanged)
                    {
                        if (Vector3.Distance(wall.transform.position, Player.transform.position) < oldDistance && !GameObject.ReferenceEquals(wall, Target2) && !GameObject.ReferenceEquals(wall, Target3))
                        {
                            Target = wall;
                        }
                        else if (Vector3.Distance(wall.transform.position, Player.transform.position) < oldDistance2 && !GameObject.ReferenceEquals(wall, Target) && !GameObject.ReferenceEquals(wall, Target3))
                        {
                            Target2 = wall;
                        }
                        else if (Vector3.Distance(wall.transform.position, Player.transform.position) < oldDistance3 && !GameObject.ReferenceEquals(wall, Target) && !GameObject.ReferenceEquals(wall, Target2))
                        {
                            Target3 = wall;
                        }
                    }
                }
            }


            Vector3 targetPosScreenPoint = cam.WorldToScreenPoint(Target.transform.position);
            Vector3 toPosition = targetPosScreenPoint;
            Vector3 fromPosition = pointerRectTransform.position;
            Vector3 dir = toPosition - fromPosition;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

            bool isOffScreen = targetPosScreenPoint.x <= borderSize || targetPosScreenPoint.x >= Screen.width - borderSize || targetPosScreenPoint.y <= borderSize || targetPosScreenPoint.y >= Screen.height - borderSize;

            if (isOffScreen)
            {
                if (!Image1.enabled)
                {
                    Image1.enabled = true;
                }
                Vector3 cappedTargetScreenPosition = targetPosScreenPoint;
                if (cappedTargetScreenPosition.x <= borderSize)
                {
                    cappedTargetScreenPosition.x = borderSize;
                }
                if (cappedTargetScreenPosition.x >= Screen.width - borderSize)
                {
                    cappedTargetScreenPosition.x = Screen.width - borderSize;
                }
                if (cappedTargetScreenPosition.y <= borderSize)
                {
                    cappedTargetScreenPosition.y = borderSize;
                }
                if (cappedTargetScreenPosition.y >= Screen.height - borderSize)
                {
                    cappedTargetScreenPosition.y = Screen.height - borderSize;
                }

                pointerRectTransform.position = cappedTargetScreenPosition;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

            }
            else
            {
                if (Image1.enabled)
                {
                    Image1.enabled = false;
                }
                return;
            }



            Vector3 targetPosScreenPoint2 = cam.WorldToScreenPoint(Target2.transform.position);
            Vector3 toPosition2 = targetPosScreenPoint2;
            Vector3 fromPosition2 = pointerRectTransform2.position;
            Vector3 dir2 = toPosition2 - fromPosition2;
            float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
            pointerRectTransform2.localEulerAngles = new Vector3(0, 0, angle2);

            bool isOffScreen2 = targetPosScreenPoint2.x <= borderSize || targetPosScreenPoint2.x >= Screen.width - borderSize || targetPosScreenPoint2.y <= borderSize || targetPosScreenPoint2.y >= Screen.height - borderSize;

            if (isOffScreen2)
            {
                if (!Image2.enabled)
                {
                    Image2.enabled = true;
                }
                Vector3 cappedTargetScreenPosition2 = targetPosScreenPoint2;
                if (cappedTargetScreenPosition2.x <= borderSize)
                {
                    cappedTargetScreenPosition2.x = borderSize;
                }
                if (cappedTargetScreenPosition2.x >= Screen.width - borderSize)
                {
                    cappedTargetScreenPosition2.x = Screen.width - borderSize;
                }
                if (cappedTargetScreenPosition2.y <= borderSize)
                {
                    cappedTargetScreenPosition2.y = borderSize;
                }
                if (cappedTargetScreenPosition2.y >= Screen.height - borderSize)
                {
                    cappedTargetScreenPosition2.y = Screen.height - borderSize;
                }

                pointerRectTransform2.position = cappedTargetScreenPosition2;
                pointerRectTransform2.localPosition = new Vector3(pointerRectTransform2.localPosition.x, pointerRectTransform2.localPosition.y, 0f);

            }
            else
            {
                if (Image2.enabled)
                {
                    Image2.enabled = false;
                }
                return;
            }

            Vector3 targetPosScreenPoint3 = cam.WorldToScreenPoint(Target3.transform.position);
            Vector3 toPosition3 = targetPosScreenPoint3;
            Vector3 fromPosition3 = pointerRectTransform3.position;
            Vector3 dir3 = toPosition3 - fromPosition3;
            float angle3 = Mathf.Atan2(dir3.y, dir3.x) * Mathf.Rad2Deg;
            pointerRectTransform3.localEulerAngles = new Vector3(0, 0, angle3);


            bool isOffScreen3 = targetPosScreenPoint3.x <= borderSize || targetPosScreenPoint3.x >= Screen.width - borderSize || targetPosScreenPoint3.y <= borderSize || targetPosScreenPoint3.y >= Screen.height - borderSize;

            if (isOffScreen3)
            {
                if (!Image3.enabled)
                {
                    Image3.enabled = true;
                }
                Vector3 cappedTargetScreenPosition3 = targetPosScreenPoint3;
                if (cappedTargetScreenPosition3.x <= borderSize)
                {
                    cappedTargetScreenPosition3.x = borderSize;
                }
                if (cappedTargetScreenPosition3.x >= Screen.width - borderSize)
                {
                    cappedTargetScreenPosition3.x = Screen.width - borderSize;
                }
                if (cappedTargetScreenPosition3.y <= borderSize)
                {
                    cappedTargetScreenPosition3.y = borderSize;
                }
                if (cappedTargetScreenPosition3.y >= Screen.height - borderSize)
                {
                    cappedTargetScreenPosition3.y = Screen.height - borderSize;
                }

                pointerRectTransform3.position = cappedTargetScreenPosition3;
                pointerRectTransform3.localPosition = new Vector3(pointerRectTransform3.localPosition.x, pointerRectTransform3.localPosition.y, 0f);

            }
            else
            {
                if (Image3.enabled)
                {
                    Image3.enabled = false;
                }
                return;
            }
        }

        else
        {
            Image1.enabled = false;
            Image2.enabled = false;
            Image3.enabled = false;
        }

        if (Target.transform.Find("Trigger(Script)").GetComponent<SprayingTrigger>().isSpriteChanged)
        {
            Image1.enabled = false;
        }
        if (Target2.transform.Find("Trigger(Script)").GetComponent<SprayingTrigger>().isSpriteChanged)
        {
            Image2.enabled = false;
        }
        if (Target3.transform.Find("Trigger(Script)").GetComponent<SprayingTrigger>().isSpriteChanged)
        {
            Image3.enabled = false;
        }

    }
}
