using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Culling Detector. Attach this script to a game object with a renderer.
/// @thehighestend
/// </summary>
public class CullingDetector : MonoBehaviour
{
    [SerializeField] VideoPlayer _videoPlayer;
    private bool _videoPlayerFound = false;

    // Start is called before the first frame update
    void Start()
    {
        if( _videoPlayer == null )
            _videoPlayer = GetComponentInChildren<VideoPlayer>();

        if( _videoPlayer != null )
            _videoPlayerFound = true;
    }

    void OnBecameVisible()
    {
        if(_videoPlayerFound)
            _videoPlayer.Play();
    }

    private void OnBecameInvisible()
    {
        if (_videoPlayerFound)
            _videoPlayer.Stop();
    }
}
