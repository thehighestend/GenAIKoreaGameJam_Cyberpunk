using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private static FadeManager _instance = null;
    public static FadeManager Instance
    { 
        get
        {
            if(_instance == null)
            {
                var canvas = new GameObject("Canvas-Fade").AddComponent<Canvas>();
                canvas.sortingOrder = 1;
                var scaler = canvas.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.matchWidthOrHeight = 0.5f;
                _instance = new GameObject("FadeImage").AddComponent<FadeManager>();
                _instance.transform.AddComponent<Image>();
                _instance.transform.SetParent(canvas.transform);
                _instance._canvasGroup = _instance.gameObject.AddComponent<CanvasGroup>();
                _instance._canvasGroup.blocksRaycasts = false;
                _instance._canvasGroup.alpha = 0f;
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        DontDestroyOnLoad(_instance.gameObject);
    }

    public void FadeAndLoadScene(string sceneName, float changeTime = 2f)
    {
        StartCoroutine(FadeSceneChange(sceneName, changeTime));
    }

    IEnumerator FadeSceneChange(string sceneName, float changeTime)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        var elapsed = 0f;

        // Fade Out
        _canvasGroup.alpha = 0f;
        while (elapsed < changeTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsed / changeTime));
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        _canvasGroup.alpha = 1f;
        elapsed = 0f;

        // Scene change
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                // Fade In
                _canvasGroup.alpha = 1f;

                asyncOperation.allowSceneActivation = true;

                while (elapsed < changeTime)
                {
                    _canvasGroup.alpha = Mathf.Lerp(1f, 0f, (elapsed / changeTime));
                    elapsed += Time.unscaledDeltaTime;

                    yield return null;
                }
                _canvasGroup.alpha = 0f;
            }
        }
    }

    // Update is called once per frame
     void Update()
    {
        
    }
}
