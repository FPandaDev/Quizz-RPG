using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGeneral : Question
{
    private List<QuestionData> questions;

    private QuestionData questionData;

    private int indexQuestion = 0;

    public QuestionGeneral(QuizDataScriptable _quizDataScriptable)
    {
        questions = new List<QuestionData>(_quizDataScriptable.questions);
    }

    public override void GenerateQuestion()
    {
        indexQuestion = Random.Range(0, questions.Count);
        
        questionData = questions[indexQuestion];
        questions.RemoveAt(indexQuestion);

        question = string.Format(questionData.question);

        GenerateOptions();
    }

    public override void GenerateOptions()
    {
        correctAnswerIndex = Random.Range(0, options.Length);

        options[correctAnswerIndex] = questionData.correctAnswer;

        int newIndex = 0;

        for (int i = 0; i < options.Length; i++)
        {
            if (i != correctAnswerIndex)
            {
                options[i] = questionData.options[newIndex];
                newIndex++;
            }
        }
    }
}
