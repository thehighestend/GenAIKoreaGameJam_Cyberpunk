using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTMManager : MonoBehaviour
{
    private bool endingTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!endingTriggered && !NPCManager.Instance.CheckNPCObjectiveStatus())
        {
            UIManager.Instance.SetRTMActive(true);
            return;
        }

        endingTriggered = true;
        BGMManager.Instance.FadeOutMusic();
        FadeManager.Instance.FadeAndLoadScene("Ending", 2, null);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!endingTriggered)
            UIManager.Instance.SetRTMActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
