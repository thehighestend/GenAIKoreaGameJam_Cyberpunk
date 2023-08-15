using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    [SerializeField] VideoPlayer _videoPlayer;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _videoPlayer.Play();
        yield return new WaitUntil(() => _videoPlayer.isPlaying);

        while(_videoPlayer.isPlaying)
        {
            yield return null;
        }

        BGMManager.Instance.FadeOutMusic();
        FadeManager.Instance.FadeAndLoadScene("Credits", 2, () => BGMManager.Instance.Play(3, 0.7f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
