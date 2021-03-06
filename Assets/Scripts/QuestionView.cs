using TMPro;
using UnityEngine;

public class QuestionView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _label;

    public void ShowQuestion(string questionText)
    {
        _label.text = questionText;
        // animate!
        // also, synchronize!
    }
}
