using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using Cinemachine;

public class EnemyController : MonoBehaviour
{
    private float alertLevel = 0;

    [SerializeField] float maxAlertLevel;
    [SerializeField] float fallRate;

    [HideInInspector] public bool proxLevel1 = false;
    [HideInInspector] public bool proxLevel2 = false;
    [HideInInspector] public bool proxLevel3 = false;

    [SerializeField] float prox1Rate;
    [SerializeField] float prox2Rate;
    [SerializeField] float prox3Rate;  // Very high, just so there is an animation for a few frames of the bar going up

    [SerializeField] GameObject UICanvas;
    [SerializeField] Slider UISlider;
    private Animator UIAnimator;

    private PlayerController player;
    private CinemachineVirtualCamera enemyLookCamera;
    private GameObject mainCamera;
    private GameController gameController;

    private bool caughtScreen;

    // Start is called before the first frame update
    void Start()
    {
        UIAnimator = UICanvas.GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        enemyLookCamera = GameObject.FindGameObjectWithTag("EnemyLookCamera").GetComponent<CinemachineVirtualCamera>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alertLevel > 0 && proxLevel1 == false)
        {
            alertLevel -= fallRate * Time.deltaTime;
            if(alertLevel < 0)
            {
                alertLevel = 0;
            }
        }

        if (proxLevel3)
        {
            alertLevel += prox3Rate * Time.deltaTime;
        }
        else if(proxLevel2)
        {
            alertLevel += prox2Rate * Time.deltaTime;
        }
        else if (proxLevel1)
        {
            alertLevel += prox1Rate * Time.deltaTime;
        }

        if(alertLevel >= maxAlertLevel)
        {
            caught();
        }

        if(alertLevel > 0)
        {
            if(UIAnimator.GetBool("Active") == false)
            {
                UIAnimator.SetBool("Active", true);
            }
            UISlider.value = alertLevel / maxAlertLevel;
            UICanvas.transform.LookAt(new Vector3(mainCamera.transform.position.x, UICanvas.transform.position.y, mainCamera.transform.position.z));
        }
        else
        {
            if (UIAnimator.GetBool("Active") == true)
            {
                UIAnimator.SetBool("Active", false);
            }
        }
    }

    private void caught()
    {
        if (!gameController.caughtScreen && !caughtScreen)
        {
            enemyLookCamera.transform.position = mainCamera.transform.position;
            enemyLookCamera.transform.rotation = mainCamera.transform.rotation;
            enemyLookCamera.LookAt = transform;
            enemyLookCamera.Priority = 20;
            player.canMove = false;
            gameController.playerCaught();
            gameController.caughtScreen = true;
            caughtScreen = true;
            gameController.setCameraRotate(false);
        }
        else if(!gameController.caughtScreen)
        {
            alertLevel = 0;
            caughtScreen = false;
        }
    }
}
