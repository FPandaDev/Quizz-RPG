using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSubtraction : Question
{
    public QuestionSubtraction(int _minNumber, int _maxNumber)
    {
        this.minNumber = _minNumber;
        this.maxNumber = _maxNumber;
    }

    public override void GenerateQuestion()
    {
        int number1 = Random.Range(minNumber, maxNumber + 1);
        int number2 = Random.Range(minNumber, number1 - 1);

        result = number1 - number2;

        question = string.Format("¿Cuánto es {0} - {1}?", number1, number2);

        GenerateOptions();
    }

    public override void GenerateOptions()
    {
        correctAnswerIndex = Random.Range(0, options.Length);

        options[correctAnswerIndex] = result.ToString();

        for (int i = 0; i < options.Length; i++)
        {
            if (i != correctAnswerIndex)
            {
                int incorrectOption = Random.Range(minNumber, maxNumber * 2 + 1);

                while (incorrectOption == result)
                {
                    incorrectOption = Random.Range(minNumber, maxNumber * 2 + 1);
                }

                options[i] = incorrectOption.ToString();
            }
        }
    }
}
