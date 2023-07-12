using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{ 
    [Header("Stats Player")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackDamage;
    [SerializeField] private int criticalDamage;
    [SerializeField] private int shieldUses;
    [SerializeField] private int potions;
    [SerializeField] private bool isDefending;

    [Header("References")]
    [SerializeField] private Image barHealth;
    [SerializeField] private Button buttonHeal;
    [SerializeField] private Button buttonDefend;
    [SerializeField] private Enemy enemy;
    [SerializeField] private RectTransform pivotTextPopup;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI criticalText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private TextMeshProUGUI potionText;

    private int health;
    private bool canDefend;

    private Animator anim;
    private QuizManager quizManager;

    public bool CanDefend { get { return canDefend; }
        set {
                buttonDefend.interactable = value;
                canDefend = value;
            }
        }

    public bool hasShieldUses { get { return shieldUses > 0; } }
    public bool canUsePotions { get { return potions > 0 && health < maxHealth; } }
    public bool isDead { get { return health <= 0; } }

    private void Start()
    {
        anim = GetComponent<Animator>();

        health = maxHealth;

        GameObject go = GameObject.FindWithTag("QuizzManager");
        quizManager = go.GetComponent<QuizManager>();

        damageText.text = attackDamage.ToString();
        criticalText.text = string.Format("+{0}", criticalDamage);
        shieldText.text = shieldUses.ToString();
        potionText.text = potions.ToString();

        buttonHeal.interactable = canUsePotions;
    }

    private void UpdateHealth(int newHealth, TypeText typeText)
    {
        health = Mathf.Clamp(health + newHealth, 0, maxHealth);
        barHealth.fillAmount = (float)health / (float)maxHealth;

        TextPopup textPopup = TextPopupPool.Instance.RequestPopup();
        textPopup.transform.position = pivotTextPopup.position;

        textPopup.Setup(Mathf.Abs(newHealth), typeText);
    }

    public void TakeDamage(int dmg)
    {
        if (isDefending)
        {
            isDefending = false;
            return;
        }

        UpdateHealth(-dmg, TypeText.NORMAL);

        TriggerAnimation("HitDamage");
    }

    public void Attack()
    {
        bool isCritical = Random.value < quizManager.TimerTimeOut;

        if (isCritical)
            enemy.TakeDamage(attackDamage + criticalDamage, TypeText.CRITICAL);
        else
            enemy.TakeDamage(attackDamage, TypeText.NORMAL);
    }

    public void CheckDeadEnemy()
    {
        if (enemy.isDead)
        {
            enemy.TriggerAnimation("Dead");
        }
        else
        {
            quizManager.SetButtonsActive();
        }
    }

    public void Defend()
    {
        if (canDefend)
        {
            shieldUses--;
            shieldText.text = shieldUses.ToString();        

            buttonDefend.interactable = false;
            isDefending = true;
            TriggerAnimation("Defend");
        }
    }

    public void Heal()
    {
        if (potions > 0)
        {
            potions--;
            potionText.text = potions.ToString();

            UpdateHealth(10, TypeText.HEAL);
            buttonHeal.interactable = canUsePotions;
        }
    }

    public void GameOver()
    {
        quizManager.SetGameOver();
    }

    public void TriggerAnimation(string nameAnim)
    {
        anim.SetTrigger(nameAnim);
    }
}