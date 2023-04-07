using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger SChanger { get; private set; }

    public bool IsFading { get; private set; }

    [SerializeField] private Animator _animator;

    private Action _fadeInCallBack;
    private Action _fadeOutCallBack;

    private bool _isLoading;

    public void LoadScene(int sceneNumber)
    {
        if (_isLoading)
            return;
        StartCoroutine(LoadSceneRoutine(sceneNumber));
    }

    public void LoadScreen(GameObject window, bool state)
    {
        if (_isLoading)
            return;
        StartCoroutine(LoadScreenRoutine(window, state));
    }
    
    private void FadeIn(Action fadeInCallBack)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadeInCallBack = fadeInCallBack;
        _animator.SetBool("faded", true);
    }

    private void FadeOut(Action fadeOutCallBack)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadeOutCallBack = fadeOutCallBack;
        _animator.SetBool("faded", false);
    }

    private void Handle_FadeInAnimationOver()
    {
        _fadeInCallBack?.Invoke();
        _fadeInCallBack = null;
        IsFading = false;
    }

    private void Handle_FadeOutAnimationOver()
    {
        _fadeOutCallBack?.Invoke();
        _fadeOutCallBack = null;
        IsFading = false;
    }

    private IEnumerator LoadScreenRoutine(GameObject gameObject, bool state)
    {
        _isLoading = true;

        bool waitFading = true;
        FadeIn(() => waitFading = false);
        while (waitFading)
            yield return null;
        
        gameObject.SetActive(state);

        waitFading = true;
        FadeOut(() => waitFading = false);

        while (waitFading)
            yield return null;
        
        _isLoading = false;
    }

    private IEnumerator LoadSceneRoutine(int sceneNumber)
    {
        _isLoading = true;
        
        bool waitFading = true;
        FadeIn(() => waitFading = false);
        while (waitFading)
            yield return null;

        var async = SceneManager.LoadSceneAsync(sceneNumber);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            yield return null;

        async.allowSceneActivation = true;

        waitFading = true;
        FadeOut(() => waitFading = false);

        while (waitFading)
            yield return null;

        _isLoading = false;
    }

    private void Awake()
    {
        if (SChanger == null)
        {
            SChanger = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
