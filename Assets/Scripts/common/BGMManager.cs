using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClipsScriptableObject clips;
    public AudioSource source;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    private static BGMManager m_instance;
    public static BGMManager Instance { 
        get 
        {
            if(m_instance == null)
            {
                var instance = FindObjectOfType(typeof(BGMManager));
                if(instance != null)
                    m_instance = (BGMManager)instance;
                else
                {
                    var playList = Resources.Load<AudioClipsScriptableObject>("BGMClips");
                    var instanceObject = new GameObject("BGMManager");
                    m_instance = instanceObject.AddComponent<BGMManager>();
                    m_instance.clips = playList;
                    m_instance.source = instanceObject.AddComponent<AudioSource>();
                    m_instance.source.playOnAwake = false;
                    m_instance.source.loop = true;
                }
            }

            return m_instance;
        } 
    }

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (source == null)
            source = GetComponent<AudioSource>();
    }

    void Start()
    {
        if(source == null)
            source = GetComponent<AudioSource>();
    }

    public void Play(int track, float volume = 1f, bool fadeIn = false)
    {
        StopAllCoroutines();
        source.clip = clips.clips[track];
        source.Play();
        StartCoroutine(FadeInMusicCoroutine(volume, fadeIn));
    }

    public void Play(string name, float volume = 1f, bool fadeIn = false)
    {
        if (clips is null || clips.clips.Count == 0)
            return;

        int index = -1;

        for (int i = 0; i < clips.clips.Count; i++)
        {
            if (string.CompareOrdinal(clips.clips[i].name, name) == 0)
            {
                index = i;
                break;
            }
        }

        if (index < 0)
            return;

        Play(index, volume, fadeIn);
    }

    public void SetVolume(float volume)
    {
        source.volume = volume;
    }

    public void Stop()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine(1f));
    }

    IEnumerator FadeInMusicCoroutine(float volume, bool fadeIn = true)
    {
        if (!fadeIn)
            source.volume = volume;
        else
        {
            for (float i = 0f; i <= 1.0f; i += 0.01f)
            {
                source.volume = i * volume;
                yield return waitTime;
            }   
        }
    }

    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine(source.volume));
    }

    IEnumerator FadeOutMusicCoroutine(float changeTime = 1.0f)
    {
        for (float i = changeTime; i >= 0f; i -= 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }

    public void ChangeMusic(int t, float changeTime = 1.0f)
    {
        StartCoroutine(ChangeMusicCoroutine(track: t, changeTime));
    }

    IEnumerator ChangeMusicCoroutine(int track, float changeTime = 1.0f)
    {
        yield return StartCoroutine(FadeOutMusicCoroutine(changeTime));
        Play(track);
    }
}