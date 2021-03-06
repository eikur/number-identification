using System;
using System.Collections;
using UnityEngine;

public class AnswerViewContainer : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] float _appearTime = 2f;
    [SerializeField] float _disappearTime = 2f;

    public event Action OnAnimationInFinished;
    public event Action OnAnimationOutFinished;

    private void Awake()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }

    public void Show()
    {
        StartCoroutine(AnimateIn());
    }

    public void Hide()
    {
        StartCoroutine(AnimateOut());
    }


    IEnumerator AnimateIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _appearTime)
        {
            _canvasGroup.alpha = elapsedTime / _appearTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;

        OnAnimationInFinished?.Invoke();
    }

    IEnumerator AnimateOut()
    {
        _canvasGroup.interactable = false;

        float elapsedTime = 0f;
        while (elapsedTime < _appearTime)
        {
            _canvasGroup.alpha = 1 - elapsedTime / _disappearTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = 0f;

        OnAnimationOutFinished?.Invoke();
    }
}
