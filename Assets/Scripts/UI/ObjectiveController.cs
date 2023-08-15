using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _objectives;

    private bool FirstObjectiveComplete = false;

    public void CheckObjectiveStatus()
    {
        if (!FirstObjectiveComplete && !NPCManager.Instance.CheckNPCObjectiveStatus())
            return;

        FirstObjectiveComplete = true;

        _objectives[0].color = Color.gray;
        _objectives[0].text = $"<s>{_objectives[0].text}</s>";

        _objectives[1].gameObject.SetActive(true);
    }
}
