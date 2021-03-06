using UnityEngine;

public class ProblemController : MonoBehaviour
{
    [SerializeField] ProblemsConfig _problemsConfig;

    [SerializeField] QuestionView _questionView;
    [SerializeField] ScoreView _scoreView;
    [SerializeField] Transform _answersContainer;
    [SerializeField] AnswerView _answerPrefab;

    int _currentProblemIndex = 0;
    ProblemConfig _currentProblemConfig;

    private void Awake()
    {
        if (_problemsConfig != null) 
        {
            Init();
        }
        else
        {
            ShowMisconfiguration();
        }
    }

    void Init()
    {
        _scoreView.Reset();
        ShowCurrentProblem();
    }

    void ShowMisconfiguration()
    {
        _questionView.Show("There are no problems configured", false);
    }

    void ClickedAnswer(AnswerConfig answerConfig)
    {
        if (answerConfig.IsCorrect)
        {
            TryShowNextProblem();
        }
    }


    void TryShowNextProblem()
    {
        _currentProblemIndex++;
        if (_currentProblemIndex >= _problemsConfig.Problems.Count)
        {
            // reached the end! 
            CleanAnswers();
            _questionView.Show("Reached the end!");
        }
        else
        {
            ShowCurrentProblem();
        }
    }

    void ShowCurrentProblem()
    {
        CleanAnswers();

        _currentProblemConfig = _problemsConfig.Problems[_currentProblemIndex];
        _questionView.Show(_currentProblemConfig.QuestionLiteral);

        foreach (var answerConfig in _currentProblemConfig.Answers)
        {
            var answerButtonGo = Instantiate(_answerPrefab, _answersContainer);
            var answerButton = answerButtonGo.GetComponent<AnswerView>();
            answerButton.Init(answerConfig);
            answerButton.OnClicked += ClickedAnswer;
        }
    }

    void CleanAnswers()
    {
        int children = _answersContainer.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(_answersContainer.GetChild(i).gameObject);
        }
    }
}
