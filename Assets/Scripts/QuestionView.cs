using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _label;

    [SerializeField] float _appearTime = 2f;
    [SerializeField] float _holdTime = 2f;
    [SerializeField] float _disappearTime = 2f;

    public event Action OnAnimationFinished;

    public void Show(string questionText)
    {
        _label.text = questionText;
        StartCoroutine(Animate());
    }

    // Intended to display the end or show that something went wrong.
    // It should be split into a separate component down the line
    public void ShowMessage(string textToDisplay)
    {
        _label.text = textToDisplay;
        _label.alpha = 1f;
    }

    IEnumerator Animate()
    {
        float elapsedTime = 0f;
        while (elapsedTime <_appearTime)
        {
            _label.alpha = elapsedTime / _appearTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _label.alpha = 1f;

        while (elapsedTime < _appearTime + _holdTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        while (elapsedTime < _appearTime + _holdTime + _disappearTime)
        {
            _label.alpha = 1 - (elapsedTime - (_appearTime + _holdTime ))/ _disappearTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _label.alpha = 0f;

        OnAnimationFinished?.Invoke();
    }
}
