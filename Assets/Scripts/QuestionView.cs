using TMPro;
using UnityEngine;

public class QuestionView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _label;

    public void Show(string questionText, bool animate = true)
    {
        _label.text = questionText;
        if (animate)
        {
            // animate!
            // also, synchronize!
        }
    }
}
