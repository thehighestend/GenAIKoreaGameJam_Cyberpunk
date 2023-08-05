using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    List<AudioSource> audioSource = new List<AudioSource>();
    [SerializeField]
    private AudioClipsScriptableObject clips = null;

    public List<AudioSource> audioSources { get { return audioSource; } }
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                AudioManager manager = go.AddComponent<AudioManager>();
                for(int i = 0; i < 5; i++)
                {
                    GameObject sourceGo = new GameObject(string.Format("source{0}", i));
                    sourceGo.transform.SetParent(go.transform);
                    AudioSource source = sourceGo.AddComponent<AudioSource>();
                    manager.audioSources.Add(source);
                }

                // manager.SetClips(Resources.LoadAll<AudioClip>("Audio/"));
            }

            return instance;
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        instance = null;
    }


    public void Play(string fileName)
    {
        AudioSource source = null;
        foreach(AudioSource aSource in audioSource)
        {
            if(!aSource.isPlaying)
            {
                source = aSource;
                break;
            }
        }

        if(source == null)
        {
            source = audioSource[0];
            source.Stop();
        }

        bool found = false;
        foreach(AudioClip clip in clips.clips)
        {
            if(string.Equals(clip.name, fileName))
            {
                found = true;
                source.clip = clip;
                source.Play();
                break;
            }
        }
        
        if(!found)
        {
            AudioClip clip = Resources.Load<AudioClip>(string.Format("Audio/{0}", fileName));
            if(!object.ReferenceEquals(clip, null))
            {
                clips.clips.Add(clip);
            }
        }
    }

    public void Pause(int index)
    {
        audioSource[index].Pause();
    }

    public void Stop(int index)
    {
        audioSource[index].Stop();
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
