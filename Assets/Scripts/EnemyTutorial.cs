using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations;

public class EnemyTutorial : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera enemyCamera;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject cinematicBars;
    [SerializeField] private Animator tutorialText;
    [SerializeField] private GameController gameController;

    private bool tutorialShowing = false;
    private bool tutorialEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (tutorialShowing && !tutorialEnd && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(gameController.hideCinematicBars());
            StartCoroutine("hideTutorialText");
            enemyCamera.Priority = 0;
            player.canMove = true;
            tutorialEnd = true;
            gameController.setCameraRotate(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialShowing && !tutorialEnd)
        {
            // instructions text
            enemyCamera.Priority = 20;
            player.canMove = false;
            cinematicBars.SetActive(true);
            tutorialText.gameObject.SetActive(true);
            tutorialShowing = true;
            gameController.setCameraRotate(false);
        }
    }

    private IEnumerator hideTutorialText()
    {
        tutorialText.SetTrigger("Exit");
        yield return new WaitForSeconds(1.6F);
        tutorialText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
