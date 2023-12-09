using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private int minRange;
    [SerializeField] private int maxRange;

    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI[] optionsText;

    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;

    [Header("BUTTONS")]   
    [SerializeField] private Button buttonAttack;
    [SerializeField] private Button buttonHeal;
    [SerializeField] private Button buttonQuit;

    [Header("FADE")]
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private Image fadeImage;

    [Header("REFERENCES")]
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private LevelLoader levelLoader;

    [Header("PANELS")]
    [SerializeField] private GameObject panelStart;
    [SerializeField] private GameObject disableButtons;

    private float timerTimeOut;
    public float TimerTimeOut { get { return timerTimeOut / (timer + 10f); } }

    private bool isAnswering;
    private bool isCorrect;

    private int countQuestion;
    private int countCorrectAnswers;
    private int counWrongAnswers;

    private Animator canvasAnimator;
    private Question currentQuestion;

    #region SINGLETON
    public static QuizManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    #endregion

    void Start()
    {
        canvasAnimator = GetComponent<Animator>();
        panelStart.SetActive(true);

        CreateQuestionClass();
    }

    void Update()
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

            case "General":
                currentQuestion = new QuestionGeneral(GameManager.instance.LevelData.quizData);
                break;
        }
    }  

    private void UpdateTimer()
    {
        timerTimeOut -= Time.deltaTime;
        timerTimeOut = Mathf.Clamp(timerTimeOut, 0, timer);

        timerText.text = Mathf.Round(timerTimeOut).ToString();

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

    public void OnClickAttack()
    {
        SetButtonsDesactive();
        ResetColorText();

        timerTimeOut = timer;
        isAnswering = true;

        disableButtons.SetActive(false);      
        canvasAnimator.SetTrigger("Question");

        currentQuestion.GenerateQuestion();
        questionText.text = currentQuestion._Question;

        for (int i = 0; i < optionsText.Length; i++)
        {
            optionsText[i].text = currentQuestion.Options[i];
        }
    }

    public void OnClickAnswer(int index)
    {
        disableButtons.SetActive(true);
        isAnswering = false;

        if (currentQuestion.CheckAnswer(index))
        {           
            optionsText[index].color = correctColor;
            isCorrect = true;
        }
        else
        {
            optionsText[index].color = wrongColor;
            optionsText[currentQuestion.CorrectAnswerIndex].color = correctColor;
            isCorrect = false;
        }

        StartCoroutine(ShowAnswers());
    }

    private IEnumerator ShowAnswers()
    {
        yield return new WaitForSecondsRealtime(1f);
        canvasAnimator.SetTrigger("Answer");

        yield return new WaitForSecondsRealtime(0.5f);
        AnimationAnswer();
    }

    private void AnimationAnswer()
    {
        if (isCorrect)
        {
            player.TriggerAnimation("Attack");
        }
        else
        {
            player.CanDefend = player.HasShieldUses;
            enemy.TriggerAnimation("Attack");
        }
    }

    public void SetButtonsActive()
    {
        buttonAttack.interactable = true;
        buttonHeal.interactable = player.CanUsePotions;
        buttonQuit.interactable = true;
    }

    public void SetButtonsDesactive()
    {
        buttonAttack.interactable = false;
        buttonHeal.interactable = false;
        buttonQuit.interactable = false;
    }

    public void SetGameOver()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        fadeImage.gameObject.SetActive(true);

        float r = fadeImage.color.r;
        float g = fadeImage.color.g;
        float b = fadeImage.color.b;

        fadeImage.color = new Color(r, g, b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(r, g, b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        levelLoader.LoadLevel();
    }
}