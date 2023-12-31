using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using Unity.VisualScripting;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Video")]
    [SerializeField] ObjectiveController _objectiveController;
    [SerializeField] Animation _videoScreenAnimation;
    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] AnimationClip _openVideo;
    [SerializeField] AnimationClip _closeVideo;
    [SerializeField] TextMeshProUGUI _closedCaption;

    [Header("Misc")]
    [SerializeField] GameObject _returnToMissionArea;
    [SerializeField] GameObject _exitPopup;

    private Coroutine _videoPlaybackCoroutine = null;

    public static UIManager Instance = null;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    IEnumerator VideoPlayback(VideoClip videoClip, string caption, Action finalCallback = null)
    {
        _videoPlayer.Stop();
        _videoPlayer.clip = videoClip;

        _videoScreenAnimation.Stop();
        _videoScreenAnimation.Play("VideoOpen");

        _videoPlayer.Play();

        _closedCaption.text = $"<mark=#000000AA>{caption}</mark>";

        while (!_videoPlayer.isPrepared || _videoPlayer.isPlaying)
        {
            yield return null;
        }

        yield return null;

        _videoScreenAnimation.Stop();
        _videoScreenAnimation.Play("VideoClose");
        _closedCaption.text = string.Empty;

        _videoPlaybackCoroutine = null;
        finalCallback?.Invoke();
    }

    public void StartVideoPlayback(VideoClip clip, string line, Action finalCallback = null)
    {
        _videoPlaybackCoroutine = StartCoroutine(VideoPlayback(clip, line, finalCallback));
    }

    public void ForceStopVideoPlayback()
    {
        StopCoroutine(_videoPlaybackCoroutine);
        _videoPlaybackCoroutine = null;
    }

    public void UpdateObjectiveUI()
    {
        _objectiveController.CheckObjectiveStatus();
    }

    public void SetRTMActive(bool active)
    {
        _returnToMissionArea.SetActive(active);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _exitPopup.SetActive(!_exitPopup.activeSelf);
        }
        else if(_exitPopup.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
