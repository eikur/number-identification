using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _label;
    Action _onClicked; // most likely will need some params

    public void Init(string textToDisplay, Action OnClicked)
    {
        _label.text = textToDisplay;
        _onClicked = OnClicked;
    }

    public void Clicked()
    {
        Debug.Log("button clicked");
        _onClicked?.Invoke();
    }

    // will need changes of color + animations in / out
}
