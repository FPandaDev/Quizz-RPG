using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question Data", menuName = "Question Data")]
public class QuizDataScriptable : ScriptableObject
{
    public List<QuestionData> questions;
}

[System.Serializable]
public class QuestionData
{
    public string question;
    public string correctAnswer;

    public List<string> options;
}