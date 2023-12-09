using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats Player")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackDamage;

    [Header("References")]
    [SerializeField] private Image barHealth;
    [SerializeField] private Image sprite;
    [SerializeField] private Player player;
    [SerializeField] private RectTransform pivotTextPopup;

    private int health;

    private Animator anim;

    public bool IsDead { get { return health <= 0; } }

    void Start()
    {
        anim = GetComponent<Animator>();
        //sprite = GetComponent<Sprite>();

        anim.runtimeAnimatorController = GameManager.instance.LevelData.animator;
        sprite.sprite = GameManager.instance.LevelData.sprite;

        health = maxHealth;
    }

    public void TakeDamage(int dmg, TypeText typeText)
    {
        health = Mathf.Clamp(health - dmg, 0, maxHealth);
        barHealth.fillAmount = (float)health / (float)maxHealth;

        TextPopup textPopup = TextPopupPool.Instance.RequestPopup();
        textPopup.transform.position = pivotTextPopup.position;

        textPopup.Setup(dmg, typeText);

        TriggerAnimation("HitDamage");
    }

    public void Attack()
    {
        player.TakeDamage(attackDamage);
    }

    public void CheckDeadPlayer()
    {      
        player.CanDefend = false;

        if (player.IsDead)
        {
            player.TriggerAnimation("Dead");
        }
        else
        {
            QuizManager.Instance.SetButtonsActive();
        }
    }

    public void GameOver()
    {
        QuizManager.Instance.SetGameOver();
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
