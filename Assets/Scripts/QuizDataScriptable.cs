using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "QuestionData")]

public class QuizDataScriptable : ScriptableObject
{
    public List<Question2> questions;
}

[System.Serializable]
public class Question2
{
    public string question;
    public string correctAnswer;

    public List<string> options;
}