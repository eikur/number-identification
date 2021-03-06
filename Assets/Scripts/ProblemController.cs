using UnityEngine;

public class ProblemController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] ScoreModel _scoreModel;
    [SerializeField] ProblemsConfig _problemsConfig;

    [Header("Scene setup")]
    [SerializeField] QuestionView _questionView;
    [SerializeField] ScoreView _scoreView;
    [SerializeField] AnswerViewContainer _answerContainer;
    [SerializeField] AnswerView _answerPrefab;

    ProblemConfig _currentProblemConfig;
    AnswerView _correctAnswerView;
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
        _questionView.OnAnimationFinished += QuestionAnimationFinished;
        _answerContainer.OnAnimationInFinished += AnswersAnimationInFinished;
        _answerContainer.OnAnimationOutFinished += AnswersAnimationOutFinished;

        _scoreModel.Reset();

        _scoreView.Init(_scoreModel);
        _scoreView.Update();

        ShowCurrentProblemQuestion();
    }

    void ClickedAnswerView(AnswerView answerView)
    {
        if (answerView == _correctAnswerView)
        {
            _scoreModel.Correct++;
            if (!_isFirstTry)
            {
                _scoreModel.SecondTry++;
            }

            _answerContainer.Hide();
            return;
        }

        if (!_isFirstTry)
        {
            _scoreModel.Incorrect++;
            _correctAnswerView.HighlightCorrect();
            _answerContainer.Hide();
            return;
        }

        answerView.Hide();
        _isFirstTry = false;
    }


    void TryShowNextProblem()
    {
        _scoreView.Update();
        if (!_problemsConfig.UseCustomProblems)
        {
            ShowCurrentProblemQuestion();
            return;
        }

        _currentProblemIndex++;
        if (_currentProblemIndex >= _problemsConfig.CustomProblems.Count)
        {
            _questionView.ShowMessage("Enhorabuena! Has acabado los problemas custom!");
            CleanAnswers();
        }
        else
        {
            ShowCurrentProblemQuestion();
        }
    }

    void ShowCurrentProblemQuestion()
    {
        if (_problemsConfig.UseCustomProblems)
        {
            _currentProblemConfig = _problemsConfig.CustomProblems[_currentProblemIndex];
        }
        else
        {
            _currentProblemConfig = _problemsConfig.GetRandomlyGeneratedProblem();
        }
        
        _isFirstTry = true;

        _questionView.Show(_currentProblemConfig.QuestionLiteral);
    }

    void ShowCurrentProblemAnswers()
    {
        CleanAnswers();

        foreach (var answerConfig in _currentProblemConfig.Answers)
        {
            var answerViewGo = Instantiate(_answerPrefab, _answerContainer.transform);
            var answerView = answerViewGo.GetComponent<AnswerView>();
            answerView.Init(answerConfig);
            answerView.OnClicked += ClickedAnswerView;

            if (answerConfig.IsCorrect)
            {
                if (_correctAnswerView != null)
                {
                    Debug.LogError($"Hay por lo menos dos respuestas marcadas como correctas en el problema custom {_currentProblemConfig.Id}");
                }
                _correctAnswerView = answerView;
            }
        }

        _answerContainer.Show();
    }

    void CleanAnswers()
    {
        int children = _answerContainer.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(_answerContainer.transform.GetChild(i).gameObject);
        }

        _correctAnswerView = null;
    }

    void ShowMisconfiguration()
    {
        _questionView.ShowMessage("There are no problems configured");
        CleanAnswers();
    }

    void QuestionAnimationFinished()
    {
        ShowCurrentProblemAnswers();
    }

    void AnswersAnimationInFinished()
    {
        // added and left blank on purpose
    }

    void AnswersAnimationOutFinished()
    {
        TryShowNextProblem();
    }

    private void OnDestroy()
    {
        _questionView.OnAnimationFinished -= QuestionAnimationFinished;
        _answerContainer.OnAnimationInFinished -= AnswersAnimationInFinished;
        _answerContainer.OnAnimationOutFinished -= AnswersAnimationOutFinished;
    }
}
