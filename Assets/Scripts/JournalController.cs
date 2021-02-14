using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    private int stage = 0;
    private

    // Start is called before the first frame update
    void Start()
    {
        stage = PlayerPrefs.GetInt("Stage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
