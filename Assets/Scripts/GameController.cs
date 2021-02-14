using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Cinemachine;

public class GameController : MonoBehaviour
{
    private int stage = 0;

    [SerializeField] private Transform[] playerStartTransforms;
    [SerializeField] private PlayerController player;

    [SerializeField] private GameObject[] stage0GameObjects;

    [SerializeField] private Animator cinematicBars;
    private PlayableDirector currentTimeline = null;
    [SerializeField] private Slider skipSlider;
    [SerializeField] private Animator skipAnim;
    [SerializeField] private float skipLength;
    private float currentSkip = 0;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private CinemachineVirtualCamera enemyLookCamera;
    [SerializeField] private CinemachineFreeLook thirdPersonCamera;

    //Stage 0
    private bool introScene = false;
    [SerializeField] private PlayableDirector introSceneTimeline;

    public bool caughtScreen;
    [HideInInspector] public Transform checkPoint;

    private float cameraXSpeed;
    private float cameraYSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = playerStartTransforms[stage].position;
        player.transform.rotation = playerStartTransforms[stage].rotation;
        stage = PlayerPrefs.GetInt("Stage");
        setGameObjectsActive();
        checkPoint = playerStartTransforms[stage];

        cameraXSpeed = thirdPersonCamera.m_XAxis.m_MaxSpeed;
        cameraYSpeed = thirdPersonCamera.m_YAxis.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTimeline && Input.GetKey(KeyCode.Space))
        {
            currentSkip += Time.deltaTime;
            skipAnim.SetBool("Active", true);
            skipSlider.value = currentSkip / skipLength;
            if (currentSkip >= skipLength)
            {
                endCutScene();
            }
        }
        else if(currentTimeline)
        {
            currentSkip = 0;
            skipAnim.SetBool("Active", false);
        }

        if(stage == 0 && !introScene)
        {
            currentTimeline = introSceneTimeline;
            cinematicBars.gameObject.SetActive(true);
            cinematicBars.SetBool("NoAnim", true);
            player.canMove = false;
            introSceneTimeline.Play();
            StartCoroutine("waitEndCutScene");
            introScene = true;
        }
    }

    private void LateUpdate()
    {
        if (caughtScreen)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("hideCinematicBars");
                player.transform.position = checkPoint.position;
                thirdPersonCamera.m_YAxis.Value = 0.5F;
                thirdPersonCamera.m_XAxis.Value = checkPoint.eulerAngles.y;
                setCameraRotate(true);
                caughtScreen = false;
                player.canMove = true;
                enemyLookCamera.Priority = 0;
            }
        }
    }

    public void setCameraRotate(bool rotate)
    {
        if (rotate)
        {
            thirdPersonCamera.m_XAxis.m_MaxSpeed = cameraXSpeed;
            thirdPersonCamera.m_YAxis.m_MaxSpeed = cameraYSpeed;
        }
        else
        {
            thirdPersonCamera.m_XAxis.m_MaxSpeed = 0;
            thirdPersonCamera.m_YAxis.m_MaxSpeed = 0;
        }
    }

    private void setGameObjectsActive()
    {
        if (stage == 0)
        {
            for (int i = 0; i < stage0GameObjects.Length; i++)
            {
                stage0GameObjects[i].SetActive(true);
            }
        }
    }

    public IEnumerator hideCinematicBars()
    {
        cinematicBars.SetTrigger("Exit");
        if (gameOverText.activeSelf)
        {
            gameOverText.GetComponent<Animator>().SetTrigger("Exit");
        }
        yield return new WaitForSeconds(1.5F);
        cinematicBars.gameObject.SetActive(false);
        gameOverText.SetActive(false);
    }

    private IEnumerator waitEndCutScene()
    {
        yield return new WaitForSeconds((float)currentTimeline.duration);
        if (currentTimeline)
        {
            endCutScene();
        }
    }

    private void endCutScene()
    {
        StopCoroutine("waitEndCutScene");
        StartCoroutine("hideCinematicBars");
        currentTimeline.Stop();
        player.canMove = true;
        currentTimeline = null;
    }

    public void playerCaught()
    {
        cinematicBars.gameObject.SetActive(true);
        gameOverText.SetActive(true);
    }
}
