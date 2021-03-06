using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _correctText;
    [SerializeField] TextMeshProUGUI _secondTryText;
    [SerializeField] TextMeshProUGUI _incorrectText;

    // I'm going to need the actual VALUE, good enough for now

    public void Reset()
    {
        _correctText.text = "0";
        _secondTryText.text = "0";
        _incorrectText.text = "0";
    }


}
