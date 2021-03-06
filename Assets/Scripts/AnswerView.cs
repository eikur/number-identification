using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class AnswerView : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _label;
    AnswerConfig _answerConfig;

    public event Action<AnswerConfig> OnClicked;

    public void Init(AnswerConfig answerConfig) 
    {
        _answerConfig = answerConfig;
        _label.text = _answerConfig.AnswerLiteral;
    }

    public void Clicked()
    {
        _button.interactable = false;

        if (_answerConfig.IsCorrect)
        {
            HighlightCorrect();
        }
        else
        {
            _button.image.color = Color.red;
        }

        OnClicked?.Invoke(_answerConfig);
    }

    public void HighlightCorrect()
    {
        _button.image.color = Color.green;
    }

    void HighlightIncorrect()
    {
        _button.image.color = Color.red;
    }

    public void SetInteractable(bool value)
    {
        _button.interactable = value;
    }
}
