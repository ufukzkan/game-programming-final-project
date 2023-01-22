using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [HideInInspector] private GameObject player;
    private PlayerController playerController;

    [HideInInspector] Vector3 DestinationPosition;

    public Vector3 DestinationRotation = new Vector3(45,45,0);
    public Vector3 DestinationRotationLook = new Vector3(45,0,0);

    public Vector3 SprayCameraPosOffset = new Vector3(0, -4, 0);

    [HideInInspector] Vector3 OffSet;

    public int CamerasXpos = -9;
    public int CamerasYpos = 9;
    public int CamerasZpos = -9;

    public float CamMoveSpeed = 4.5f;

    public float CamFocusSize = 4;

    Camera cam;

    [HideInInspector] public bool sprayable = false;


    // Start is called before the first frame update
    void Start()
    {
        OffSet = new Vector3(CamerasXpos, CamerasYpos, CamerasZpos);
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        DestinationPosition = player.transform.position;
        transform.position = player.transform.position + OffSet;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        OffSet = new Vector3(CamerasXpos, CamerasYpos, CamerasZpos);

        if (!playerController.isItOn || (playerController.WhichWallTrigger != null && playerController.WhichWallTrigger.didIEarn))
        {
            DestinationPosition = player.transform.position + OffSet;
            transform.position = Vector3.Lerp(transform.position, DestinationPosition, CamMoveSpeed * Time.deltaTime);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, DestinationRotation, CamMoveSpeed * Time.deltaTime);
            cam.orthographicSize = cam.orthographicSize + ((10 - cam.orthographicSize) / 2);
            if(Vector3.Distance(transform.position, DestinationPosition) < 0.8f)
            {
                sprayable = true;
            }
            else
            {
                sprayable = false;
            }
        }

        else if(playerController.WhichWallTrigger != null && !playerController.WhichWallTrigger.didIEarn)
        {
            DestinationPosition = playerController.WhichWall.transform.position + new Vector3(SprayCameraPosOffset.x, CamerasYpos + SprayCameraPosOffset.y, CamerasZpos + SprayCameraPosOffset.z);
            transform.position = Vector3.Lerp(transform.position, DestinationPosition, CamMoveSpeed * Time.deltaTime);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, DestinationRotationLook, CamMoveSpeed * Time.deltaTime);
            cam.orthographicSize = cam.orthographicSize + ((CamFocusSize - cam.orthographicSize) / 2);
            if (Vector3.Distance(transform.position, DestinationPosition) < 0.8f)
            {
                sprayable = true;
            }
            else
            {
                sprayable = false;
            }
        }
        else if(playerController.WhichWallTrigger == null)
        {
            DestinationPosition = playerController.WhichWall.transform.position + new Vector3(SprayCameraPosOffset.x, CamerasYpos + SprayCameraPosOffset.y, CamerasZpos + SprayCameraPosOffset.z);
            transform.position = Vector3.Lerp(transform.position, DestinationPosition, CamMoveSpeed * Time.deltaTime);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, DestinationRotationLook, CamMoveSpeed * Time.deltaTime);
            cam.orthographicSize = cam.orthographicSize + ((CamFocusSize - cam.orthographicSize) / 2);
            if (Vector3.Distance(transform.position, DestinationPosition) < 0.8f)
            {
                sprayable = true;
            }
            else
            {
                sprayable = false;
            }
        }
    }
}
