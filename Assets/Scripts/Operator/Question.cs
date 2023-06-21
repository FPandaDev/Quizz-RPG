using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    protected int minNumber;
    protected int maxNumber;
    
    protected int correctAnswerIndex;
    protected int result;
    protected string question;

    protected int[] options = new int[4];


    public Question(int _minNumber, int _maxNumber) { }


    public virtual void GenerateQuestion() { }
    public virtual void GenerateOptions() { }


    public int _correctAnswerIndex { get { return correctAnswerIndex;  } }
    public string _Question { get { return question; } }
    public int[] _Options { get { return options; } }


    public bool CheckAnswer(int userResponse)
    {
        return result == userResponse;
    }
}
