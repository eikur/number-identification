using UnityEngine;

public class ProblemController : MonoBehaviour
{
    [SerializeField] ScoreModel _scoreModel;
    [SerializeField] ProblemsConfig _problemsConfig;

    [SerializeField] QuestionView _questionView;
    [SerializeField] ScoreView _scoreView;
    [SerializeField] Transform _answersContainer;
    [SerializeField] AnswerView _answerPrefab;

    ProblemConfig _currentProblemConfig;
    int _currentProblemIndex = 0;
    bool _isFirstTry = false;

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
        _scoreModel.Reset();

        _scoreView.Init(_scoreModel);
        _scoreView.Update();

        ShowCurrentProblem();
    }

    void ClickedAnswer(AnswerConfig answerConfig)
    {
        if (answerConfig.IsCorrect)
        {
            if (_isFirstTry)
            {
                _scoreModel.Correct++;
            }
            else
            {
                _scoreModel.SecondTry++;
            }

            TryShowNextProblem();
            return;
        }

        if (!_isFirstTry)
        {
            _scoreModel.Incorrect++;
            TryShowNextProblem();
            return;
        }

        _isFirstTry = false;
    }


    void TryShowNextProblem()
    {
        _scoreView.Update();
        _currentProblemIndex++;

        if (_currentProblemIndex >= _problemsConfig.Problems.Count)
        {
            _questionView.Show("Congrats! You reached the end!", false);
            CleanAnswers();
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

        _isFirstTry = true;
    }

    void CleanAnswers()
    {
        int children = _answersContainer.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(_answersContainer.GetChild(i).gameObject);
        }
    }

    void ShowMisconfiguration()
    {
        _questionView.Show("There are no problems configured", false);
        CleanAnswers();
    }
}
