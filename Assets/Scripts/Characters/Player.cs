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

    public bool CanDefend { get { return canDefend; }
        set {
                buttonDefend.interactable = value;
                canDefend = value;
            }
        }

    public bool HasShieldUses { get { return shieldUses > 0; } }
    public bool CanUsePotions { get { return potions > 0 && health < maxHealth; } }
    public bool IsDead { get { return health <= 0; } }

    void Start()
    {
        anim = GetComponent<Animator>();

        health = maxHealth;

        damageText.text = attackDamage.ToString();
        criticalText.text = $"+{criticalDamage}";
        shieldText.text = shieldUses.ToString();
        potionText.text = potions.ToString();

        buttonHeal.interactable = CanUsePotions;
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
        bool isCritical = Random.value < QuizManager.Instance.TimerTimeOut;

        if (isCritical)
            enemy.TakeDamage(attackDamage + criticalDamage, TypeText.CRITICAL);
        else
            enemy.TakeDamage(attackDamage, TypeText.NORMAL);
    }

    public void CheckDeadEnemy()
    {
        if (enemy.IsDead)
        {
            enemy.TriggerAnimation("Dead");
        }
        else
        {
            QuizManager.Instance.SetButtonsActive();
        }
    }

    public void OnClickDefend()
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

    public void OnClickHeal()
    {
        if (potions > 0)
        {
            potions--;
            potionText.text = potions.ToString();

            UpdateHealth(10, TypeText.HEAL);
            buttonHeal.interactable = CanUsePotions;
        }
    }

    public void GameOver()
    {
        QuizManager.Instance.SetGameOver();
    }

    public void TriggerAnimation(string nameAnim)
    {
        anim.SetTrigger(nameAnim);
    }
}