using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizzManager : MonoBehaviour
{
    [SerializeField] private Animator canvasAnimator;

    [SerializeField] private int minRange;
    [SerializeField] private int maxRange;

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI[] optionsText;

    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;

    [SerializeField] private GameObject disableButtons;

    public GameObject panelRespuesta;
    public GameObject panelResultado;

    public TextMeshProUGUI resultadoText;

    private int countQuestion;
    private int countCorrectAnswers;
    private int counWrongAnswers;

    private Question currentQuestion;

    private void Start()
    {
        currentQuestion = new QuestionAddition(minRange, maxRange);
    }

    public void OnClickSetQuestion()
    {
        ResetColorText();

        disableButtons.SetActive(false);
        canvasAnimator.SetTrigger("Question");

        currentQuestion.GenerateQuestion();
        questionText.text = currentQuestion._Question;

        for (int i = 0; i < optionsText.Length; i++)
        {
            optionsText[i].text = currentQuestion._Options[i].ToString();
        }
    }

    public void OnClickCheckAnswer(int _optionIndex)
    {
        disableButtons.SetActive(true);

        if (currentQuestion.CheckAnswer(currentQuestion._Options[_optionIndex]))
        {           
            optionsText[_optionIndex].color = correctColor;
        }
        else
        {
            optionsText[_optionIndex].color = wrongColor;
            optionsText[currentQuestion._correctAnswerIndex].color = correctColor;
        }

        canvasAnimator.SetTrigger("Answer");

        SceneManager.LoadScene("GameOver");
    }

    private void ResetColorText()
    {
        for (int i = 0;i < optionsText.Length; i++)
        {
            optionsText[i].color = Color.white;
        }
    }
}
