
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnswerConfig
{
    public string AnswerLiteral;
    public bool IsCorrect;
}

[Serializable]
public class ProblemConfig
{
    public string Id;
    public string QuestionLiteral;
    public List<AnswerConfig> Answers;
}

[Serializable]
public class NumberConfig
{
    public int Number;
    public string Literal;
}

[CreateAssetMenu(fileName = "ProblemsConfig", menuName = "NumberIdentification/ProblemsConfig")]
public class ProblemsConfig : ScriptableObject
{
    [Header("Problem generation section")]
    public List<NumberConfig> Numbers;
    public int responsesAvailable;

    [Header("Custom Problems - Cool for testing - NOT infinite!")]
    public bool UseCustomProblems;
    public List<ProblemConfig> CustomProblems;

    public ProblemConfig GetRandomlyGeneratedProblem()
    {
        ProblemConfig problemConfig = new ProblemConfig();
        problemConfig.Answers = new List<AnswerConfig>();

        int selectedNumberIndex = UnityEngine.Random.Range(0, Numbers.Count);
        problemConfig.QuestionLiteral = Numbers[selectedNumberIndex].Literal;

        List<int> shownIndices = new List<int> { selectedNumberIndex };
        int extraAnswers = Mathf.Min(responsesAvailable - 1, Numbers.Count - 1);
        int indexToAdd = selectedNumberIndex;
        for(int i = 0; i < extraAnswers; ++i)
        {
            indexToAdd = UnityEngine.Random.Range(0, Numbers.Count);
            while (shownIndices.Contains(indexToAdd))
            {
                indexToAdd = UnityEngine.Random.Range(0, Numbers.Count);
            }
            shownIndices.Add(indexToAdd);
        }
        shownIndices.Sort();

        foreach (int index in shownIndices)
        {
            AnswerConfig answerConfig = new AnswerConfig();
            answerConfig.AnswerLiteral = Numbers[index].Number.ToString();
            answerConfig.IsCorrect = index == selectedNumberIndex;
            problemConfig.Answers.Add(answerConfig);
        }

        return problemConfig;
    }


}