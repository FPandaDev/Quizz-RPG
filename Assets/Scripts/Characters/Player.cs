using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{ 
    [Header("Stats Player")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float attackDamage;
    [SerializeField] private bool isDefending;

    [Header("References")]
    [SerializeField] private Image barHealth;
    [SerializeField] private Button buttonDefend;
    [SerializeField] private Enemy enemy;

    private float health;
    private bool canDefend;

    public bool CanDefend { get { return canDefend; }
        set {
                buttonDefend.interactable = true;
                canDefend = value;
            }
        }

    private Animator anim;
    private QuizManager quizManager;

    private void Start()
    {
        anim = GetComponent<Animator>();

        health = maxHealth;

        GameObject go = GameObject.FindWithTag("QuizzManager");
        quizManager = go.GetComponent<QuizManager>();
    }

    public void TakeDamage(float dmg)
    {
        if (isDefending)
        {
            isDefending = false;
            return;
        }

        health = Mathf.Clamp(health - dmg, 0, maxHealth);
        barHealth.fillAmount = health / maxHealth;

        TriggerAnimation("HitDamage");
    }

    public void Attack()
    {
        enemy.TakeDamage(attackDamage);
    }

    public void Defend()
    {
        if (canDefend)
        {
            buttonDefend.interactable = false;
            isDefending = true;
            TriggerAnimation("Defend");
        }
    }

    public void TriggerAnimation(string nameAnim)
    {
        anim.SetTrigger(nameAnim);
    }
}