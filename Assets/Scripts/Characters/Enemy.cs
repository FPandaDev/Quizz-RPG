using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats Player")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float attackDamage;

    [Header("References")]
    [SerializeField] private Image barHealth;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Player player;

    private float health;

    private Animator anim;
    private QuizManager quizManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<Sprite>();

        anim.runtimeAnimatorController = GameManager.instance.LevelData.animator;
        sprite = GameManager.instance.LevelData.sprite;

        health = maxHealth;

        GameObject go = GameObject.FindWithTag("QuizzManager");
        quizManager = go.GetComponent<QuizManager>();
    }

    public void TakeDamage(float dmg)
    {
        health = Mathf.Clamp(health - dmg, 0, maxHealth);
        barHealth.fillAmount = health / maxHealth;

        TriggerAnimation("HitDamage");
    }

    public void Attack()
    {
        player.TakeDamage(attackDamage);
    }

    public void CompleteAttack()
    {
        //player.TriggerAnimation("DefendFailed");
    }

    public void TriggerAnimation(string nameAnim)
    {
        anim.SetTrigger(nameAnim);
    }
}
