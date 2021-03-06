
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

[CreateAssetMenu(fileName = "ProblemsConfig", menuName = "NumberIdentification/ProblemsConfig")]
public class ProblemsConfig : ScriptableObject
{
    public List<ProblemConfig> Problems;
}