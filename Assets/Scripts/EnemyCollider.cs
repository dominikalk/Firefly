using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] int proxLevelCollider;
    [SerializeField] EnemyController enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (proxLevelCollider)
            {
                case 1:
                    enemy.proxLevel1 = true;
                    break;
                case 2:
                    enemy.proxLevel2 = true;
                    break;
                case 3:
                    enemy.proxLevel3 = true;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (proxLevelCollider)
            {
                case 1:
                    enemy.proxLevel1 = false;
                    break;
                case 2:
                    enemy.proxLevel2 = false;
                    break;
                case 3:
                    enemy.proxLevel3 = false;
                    break;
                default:
                    break;
            }
        }
    }
}
