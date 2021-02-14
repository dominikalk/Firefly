using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
<<<<<<< HEAD
using Cinemachine;

public class EnemyController : MonoBehaviour
{
    private float alertLevel = 0;
=======

public class EnemyController : MonoBehaviour
{
    [Range(0.0f, 10f)] [SerializeField] float alertLevel = 0;
>>>>>>> de9788a82bc808a3787eea5be92a82b30c02d9ee

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

<<<<<<< HEAD
    private PlayerController player;
    private CinemachineVirtualCamera enemyLookCamera;
    private GameObject mainCamera;
    private GameController gameController;

    private bool caughtScreen;
=======
    [SerializeField] GameObject player;
>>>>>>> de9788a82bc808a3787eea5be92a82b30c02d9ee

    // Start is called before the first frame update
    void Start()
    {
        UIAnimator = UICanvas.GetComponent<Animator>();
<<<<<<< HEAD
        player = FindObjectOfType<PlayerController>();
        enemyLookCamera = GameObject.FindGameObjectWithTag("EnemyLookCamera").GetComponent<CinemachineVirtualCamera>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        gameController = FindObjectOfType<GameController>();
=======
>>>>>>> de9788a82bc808a3787eea5be92a82b30c02d9ee
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
<<<<<<< HEAD
=======
            alertLevel = 0;
>>>>>>> de9788a82bc808a3787eea5be92a82b30c02d9ee
        }

        if(alertLevel > 0)
        {
            if(UIAnimator.GetBool("Active") == false)
            {
                UIAnimator.SetBool("Active", true);
            }
            UISlider.value = alertLevel / maxAlertLevel;
<<<<<<< HEAD
            UICanvas.transform.LookAt(new Vector3(mainCamera.transform.position.x, UICanvas.transform.position.y, mainCamera.transform.position.z));
=======
            UICanvas.transform.LookAt(new Vector3(player.transform.position.x, UICanvas.transform.position.y, player.transform.position.z));
>>>>>>> de9788a82bc808a3787eea5be92a82b30c02d9ee
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
<<<<<<< HEAD
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
=======
        Debug.Log("You were caught");
>>>>>>> de9788a82bc808a3787eea5be92a82b30c02d9ee
    }
}
