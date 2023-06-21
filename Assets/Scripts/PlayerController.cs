using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{ 
    [Header("Stats Player")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float attackDamage;

    [Header("References")]
    [SerializeField] private Image barHealth;

    private float health;

    private Animator anim;
    private QuizzManager quizzManager;

    private void Start()
    {
        anim = GetComponent<Animator>();

        GameObject go = GameObject.FindWithTag("QuizzManager");
        quizzManager = go.GetComponent<QuizzManager>();
    }

    public void TakeDamage(float dmg)
    {
        health = Mathf.Clamp(health - dmg, 0, maxHealth);

        barHealth.fillAmount = health;
     
        anim.SetTrigger("HitDamage");
    }
}