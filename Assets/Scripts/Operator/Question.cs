using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    protected int minNumber;
    protected int maxNumber;
    
    protected int correctAnswerIndex;
    protected int result;
    protected string question;

    protected string[] options = new string[4];

    public virtual void GenerateQuestion() { }
    public virtual void GenerateOptions() { }

    public int CorrectAnswerIndex { get { return correctAnswerIndex;  } }
    public string _Question { get { return question; } }
    public string[] Options { get { return options; } }

    public bool CheckAnswer(int userResponse)
    {
        return CorrectAnswerIndex == userResponse;
    }
}
