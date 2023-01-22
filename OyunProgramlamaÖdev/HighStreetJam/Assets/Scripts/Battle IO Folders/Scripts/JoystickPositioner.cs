using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class JoystickPositioner : MonoBehaviour
{
    [SerializeField] InputAction firstTouch;
    [SerializeField] InputAction endTouch;
    [SerializeField] InputAction touchScreenPos;
    CustomStick stick;
    RectTransform stickRT;
    //PlayerAnimatorController playerAnimatorController;
    // Start is called before the first frame update
    void Awake()
    {
        stick = FindObjectOfType<CustomStick>();
        stickRT = stick.GetComponent<RectTransform>();
        stickRT.anchoredPosition = new Vector3(Screen.width / 2f, Screen.height / 6f, 0f);
        stick.GetComponent<Image>().raycastPadding = new Vector4(Screen.width, Screen.height, Screen.width, Screen.height);
    }

    private void Start()
    {
        firstTouch.started += StartTouch;
        endTouch.performed += EndTouch;
    }

    private void OnEnable()
    {
        firstTouch.Enable();
        endTouch.Enable();
        touchScreenPos.Enable();
    }

    private void OnDisable()
    {
        firstTouch.Disable();
        endTouch.Disable();
        touchScreenPos.Disable();
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        //Debug.Log("start touch "+ touchScreenPos.ReadValue<Vector2>());
        ////movePad.anchoredPosition = playerInput.PlayerMain.FirstTouch.ReadValue<Vector2>();
        ////stick.movementRange = 100;
        ////stickRT.anchoredPosition = playerInput.PlayerMain.FirstTouch.ReadValue<Vector2>();
        //stick.m_StartPos = touchScreenPos.ReadValue<Vector2>();
        StartCoroutine(WaitAndReadValue());
    }

    void EndTouch(InputAction.CallbackContext context)
    {
        //Debug.Log("end touch " + touchScreenPos.ReadValue<Vector2>());
        //GameObject playerGun = GameObject.FindGameObjectWithTag("PlayerGun");
        //if (playerGun != null)
        //{
        //    if (!playerGun.transform.Find("Gun Shot Projectile").gameObject.activeInHierarchy)
        //    {
        //        playerGun.transform.Find("Gun Shot Projectile").gameObject.SetActive(true);
        //    }
        //}
        stick.m_StartPos = new Vector3(Screen.width / 2f, Screen.height / 6f, 0f);
        stickRT.anchoredPosition = new Vector3(Screen.width / 2f, Screen.height / 6f, 0f);
        //if (playerAnimatorController == null)
        //{
        //    playerAnimatorController = FindObjectOfType<PlayerAnimatorController>();
        //}
        //playerAnimatorController.AttackSword();
    }

    IEnumerator WaitAndReadValue()
    {
        yield return new WaitForEndOfFrame();
        //Debug.Log("start touch " + touchScreenPos.ReadValue<Vector2>());
        //movePad.anchoredPosition = playerInput.PlayerMain.FirstTouch.ReadValue<Vector2>();
        //stick.movementRange = 100;
        //stickRT.anchoredPosition = playerInput.PlayerMain.FirstTouch.ReadValue<Vector2>();
        stick.m_StartPos = touchScreenPos.ReadValue<Vector2>();
    }

    //void Update()
    //{
    //    if (firstTouch.WasPressedThisFrame())
    //    {
    //        Debug.Log(touchScreenPos.ReadValue<Vector2>());
    //        //movePad.anchoredPosition = playerInput.PlayerMain.FirstTouch.ReadValue<Vector2>();
    //        //stick.movementRange = 100;
    //        //stickRT.anchoredPosition = playerInput.PlayerMain.FirstTouch.ReadValue<Vector2>();
    //        stick.m_StartPos = touchScreenPos.ReadValue<Vector2>();

    //    }
    //    if (firstTouch.WasReleasedThisFrame())
    //    {
    //        //stick.movementRange = 10000;
    //        stick.m_StartPos = new Vector3(Screen.width / 2f, Screen.height / 6f, 0f);
    //        stickRT.anchoredPosition = new Vector3(Screen.width / 2f, Screen.height / 6f, 0f);

    //    }
    //}
}
