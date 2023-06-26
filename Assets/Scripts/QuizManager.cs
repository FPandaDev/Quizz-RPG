using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private Animator canvasAnimator;

    [SerializeField] private float timer;

    [SerializeField] private int minRange;
    [SerializeField] private int maxRange;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI[] optionsText;

    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;

    [SerializeField] private GameObject disableButtons;

    [Header("References Characters")]
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    //public GameObject panelRespuesta;
    //public GameObject panelResultado;

    //public TextMeshProUGUI resultadoText;

    private float timerTimeOut;

    private bool isAnswering;
    private bool isCorrect;

    private int countQuestion;
    private int countCorrectAnswers;
    private int counWrongAnswers;

    private Question currentQuestion;

    private void Start()
    {
        CreateQuestionClass();
    }

    private void Update()
    {
        if (isAnswering)
        {
            UpdateTimer();
        }
    }

    private void CreateQuestionClass()
    {
        string nameLevel = GameManager.instance.LevelData.nameLevel;

        switch (nameLevel)
        {
            case "Suma":
                currentQuestion = new QuestionAddition(minRange, maxRange);
                break;

            case "Resta":
                currentQuestion = new QuestionSubtraction(minRange, maxRange);
                break;
        }
    }  

    private void UpdateTimer()
    {
        timerTimeOut -= Time.deltaTime;
        timerTimeOut = Mathf.Clamp(timerTimeOut, 0, timer);

        timerText.text = MathF.Round(timerTimeOut).ToString();

        if (timerTimeOut <= 0)
        {
            disableButtons.SetActive(true);
            isCorrect = false;
            canvasAnimator.SetTrigger("Answer");
            isAnswering = false;
        }
    }

    private void ResetColorText()
    {
        for (int i = 0; i < optionsText.Length; i++)
        {
            optionsText[i].color = Color.white;
        }
    }

    public void OnClickSetQuestion()
    {
        ResetColorText();

        timerTimeOut = timer;
        isAnswering = true;

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
        isAnswering = false;

        if (currentQuestion.CheckAnswer(currentQuestion._Options[_optionIndex]))
        {           
            optionsText[_optionIndex].color = correctColor;
            isCorrect = true;
        }
        else
        {
            optionsText[_optionIndex].color = wrongColor;
            optionsText[currentQuestion._correctAnswerIndex].color = correctColor;
            isCorrect = false;
        }

        canvasAnimator.SetTrigger("Answer");

        //SceneManager.LoadScene("GameOver");
    }

    public void AnimationAnswer()
    {
        if (isCorrect)
        {
            player.TriggerAnimation("Attack");
        }
        else
        {
            player.CanDefend = true;
            enemy.TriggerAnimation("Attack");
        }
    }   
}