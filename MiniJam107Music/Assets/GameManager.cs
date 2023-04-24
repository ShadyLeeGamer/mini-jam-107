using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Fader fader;
    [SerializeField] CanvasGroup darknessCanvas;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        fader = FindObjectOfType<Fader>();
        darknessCanvas.alpha = 1;

    }

    private void Start()
    {
        StartCoroutine(fader.FadeOutElement(darknessCanvas, 1));
    }

    public void Play()
    {
        StartCoroutine(LoadMainScene());
    }

    public void Won()
    {
        StartCoroutine(LoadWinScene());
    }

    public void Title()
    {
        StartCoroutine(LoadTitleScene());
    }

    public IEnumerator LoadMainScene()
    {
        yield return StartCoroutine(fader.FadeInElement(darknessCanvas, 1));
        SceneManager.LoadScene(1);
    }

    public IEnumerator LoadWinScene()
    {
        yield return StartCoroutine(fader.FadeInElement(darknessCanvas, 1));
        SceneManager.LoadScene(2);
    }

    public IEnumerator LoadTitleScene()
    {
        yield return StartCoroutine(fader.FadeInElement(darknessCanvas, 1));
        SceneManager.LoadScene(0);
    }
}
