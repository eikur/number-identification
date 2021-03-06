using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreModel", menuName = "NumberIdentification/ScoreModel")]
public class ScoreModel : ScriptableObject
{
    public int Correct;
    public int SecondTry;
    public int Incorrect;

    public void Reset()
    {
        Correct = 0;
        SecondTry = 0;
        Incorrect = 0;
    }
}
