using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TelescopeController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera telescopeCamera;
    [SerializeField] GameObject instructionsUI;
    [SerializeField] UILookAt pressUI;
    private Animator pressUIAnim;
    private bool isNear = false;
    private bool isUsingTelescope = false;

    [SerializeField] PlayerController playerMovement;

    [SerializeField] float sensitivity;
    [SerializeField] float maximumRotation;
    float rotationX = 0F;
    float rotationY = 0F;
    float initialX;
    float initialY;

    [SerializeField] float zoomSensitivity;
    [SerializeField] float maxZoom ;
    [SerializeField] float minZoom;

    // Start is called before the first frame update
    void Start()
    {
        initialX = telescopeCamera.transform.localEulerAngles.x;
        initialY = telescopeCamera.transform.localEulerAngles.y;
        pressUIAnim = pressUI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNear)
        {
            //pressUI.gameObject.SetActive(true);
            pressUI.lookAT = true;
            pressUIAnim.SetBool("Active", true);
        }
        else
        {
            //pressUI.gameObject.SetActive(false);
            pressUI.lookAT = false;
            pressUIAnim.SetBool("Active", false);
        }

        if(isNear && !isUsingTelescope && Input.GetKeyDown(KeyCode.E))
        {
            isUsingTelescope = true;
            mainCamera.enabled = false;
            telescopeCamera.enabled = true;
            playerMovement.canMove = false;
            instructionsUI.SetActive(true);
        }
        else if (isUsingTelescope && Input.GetKeyDown(KeyCode.E))
        {
            isUsingTelescope = false;
            mainCamera.enabled = true;
            telescopeCamera.enabled = false;
            playerMovement.canMove = true;
            instructionsUI.SetActive(false);
        }
        else if (isUsingTelescope)
        {
            rotationX -= Input.GetAxis("Mouse X") * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -maximumRotation, maximumRotation);
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, -maximumRotation, maximumRotation);
            telescopeCamera.transform.rotation = Quaternion.Euler(initialX + rotationY, initialY + rotationX, 0);

            if (Input.GetKey(KeyCode.W) && telescopeCamera.fieldOfView > maxZoom)
            {
                telescopeCamera.fieldOfView -= zoomSensitivity * Time.deltaTime;
            }else if (Input.GetKey(KeyCode.S) && telescopeCamera.fieldOfView < minZoom)
            {
                telescopeCamera.fieldOfView += zoomSensitivity * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }
}
