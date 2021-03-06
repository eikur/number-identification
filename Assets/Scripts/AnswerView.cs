using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class AnswerView : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _label;
    [SerializeField] float _disappearTime = 1f;

    bool _isCorrect;

    public event Action<AnswerView> OnClicked;

    public void Init(AnswerConfig answerConfig) 
    {
        _label.text = answerConfig.AnswerLiteral;
        _isCorrect = answerConfig.IsCorrect;
    }

    public void Clicked()
    {
        _canvasGroup.interactable = false;

        if (_isCorrect)
        {
            HighlightCorrect();
        }
        else
        {
            HighlightIncorrect();
        }

        OnClicked?.Invoke(this);
    }

    public void HighlightCorrect()
    {
        _button.image.color = Color.green;
    }

    void HighlightIncorrect()
    {
        _button.image.color = Color.red;
    }

    public void Hide()
    {
        StartCoroutine(AnimateOut());
    }

    IEnumerator AnimateOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _disappearTime)
        {
            _canvasGroup.alpha = 1 - elapsedTime / _disappearTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = 0f;
    }
}
