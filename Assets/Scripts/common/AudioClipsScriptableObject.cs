using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audioclips Data", menuName = "ScriptableObject/Audioclips", order = int.MaxValue)]
[System.Serializable]
public class AudioClipsScriptableObject : ScriptableObject
{
    public List<AudioClip> clips;
}
