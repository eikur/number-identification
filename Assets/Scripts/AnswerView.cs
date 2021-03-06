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
        if (_answerConfig.IsCorrect)
        {
            _button.image.color = Color.green;
        }
        else
        {
            _button.image.color = Color.red;
        }

        OnClicked?.Invoke(_answerConfig);
    }

}
