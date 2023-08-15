using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private bool isStartingGame = false;

    // Start is called before the first frame update
    void Start()
    {
        BGMManager.Instance.Play(0);
    }

    public void OpenGitHubTHE()
    {
        Application.OpenURL("https://github.com/thehighestend");
    }

    public void OpenGitHubYDS()
    {
        Application.OpenURL("https://github.com/YoungDevSpace");
    }

    public void OpenBlogPY()
    {
        Application.OpenURL("https://blog.naver.com/paiyang");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStartingGame && Input.GetKeyDown(KeyCode.Return))
        {
            isStartingGame = true;
            BGMManager.Instance.FadeOutMusic();
            FadeManager.Instance.FadeAndLoadScene("MapScene", 2, () => BGMManager.Instance.Play(2, 0.3f));
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
