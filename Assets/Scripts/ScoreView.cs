using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _correctText;
    [SerializeField] TextMeshProUGUI _secondTryText;
    [SerializeField] TextMeshProUGUI _incorrectText;

    ScoreModel _scoreModel;

    public void Init(ScoreModel scoreModel)
    {
        _scoreModel = scoreModel;
    }

    public void Update()
    {
        _correctText.text = _scoreModel.Correct.ToString();
        _secondTryText.text = _scoreModel.SecondTry.ToString();
        _incorrectText.text = _scoreModel.Incorrect.ToString();
    }
}
