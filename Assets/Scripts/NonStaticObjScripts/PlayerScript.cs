﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Character {

    public GameManagerScript gameManagerScript;
    public UIScript uiScript;
    public CharacterController2D controller;
    //public Animator Animator;
    public GameObject player;
    [SerializeField]
    private EdgeCollider2D BarkCollider;
    private PlayerScript playerScript;
    
    // private Rigidbody2D rigidbody;

    /*----------- PLAYER STATS -----------*/
    //protected float currentHealth, maxHealth;
    //protected float currentStamina, maxStamina;
    //protected float currentHunger, maxHunger;
    //protected float temperature;
   
    public float armor;
    public float insulation;

    public float healthRecoveryRate;
    public float staminaRecoveryRate;
    public float hungerRecoveryRate;



    /*------------------------------------*/


    public override void Start()
    {
        Debug.Log("PlayerScript start");
        // base.Start();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        uiScript = GameObject.Find("UI").GetComponent<UIScript>();
        player = GameObject.Find("GameManager").GetComponent<GameManagerScript>().player;
        playerScript = player.GetComponent<PlayerScript>();
        // rigidbody = GetComponent<Rigidbody2D>();

    }
    // checking on every frame
    private void Update()
    {


    }

    private void FixedUpdate()
    {
        if(currentHealth < maxHealth)
        {
            ChangeHealth(healthRecoveryRate);
        }
        if(currentStamina < maxStamina)
        {
            ChangeStamina(staminaRecoveryRate);
        }
        if(currentHunger > 0)
        {
            ChangeHunger(hungerRecoveryRate);
        }

    }


    public void ChangeHealth(float health)
    {
        currentHealth += health;

        if (currentHealth >= maxHealth) currentHealth = maxHealth;
        else if (currentHealth <= 0) currentHealth = 0.0f;

        uiScript.UpdateHealth((currentHealth / maxHealth) * 100.0f);
        if (currentHealth <= 0.0) { Die(); }
    }

    public void ChangeStamina(float stamina)
    {
        currentStamina += stamina;

        if (currentStamina >= maxStamina) currentStamina = maxStamina;
        else if (currentStamina <= 0) currentStamina = 0.0f;

        uiScript.UpdateStamina((currentStamina / maxStamina) * 100.0f);
    }

    public void ChangeHunger(float hunger)
    {
        currentHunger += hunger;

        if (currentHunger >= maxHunger) currentHunger = maxHunger;
        else if (currentHunger <= 0) currentHunger = 0.0f;

        uiScript.UpdateHunger((currentHunger / maxHunger) * 100.0f);
    }


    public void Die()
    {

    }

    public void MeleeAttack()
    {
        BarkCollider.enabled = !BarkCollider.enabled;
    }


    public void TakeDamage1(float damage)
    {
        currentHealth -= damage;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0) 
        { 
            currentHealth = 0.0f; 
        }
        uiScript.UpdateHealth((currentHealth / maxHealth) * 100.0f);
        if (currentHealth <= 0.0) 
        {
            Die();
        }
    }

    public override bool IsDead
    {
        get
        {
            return currentHealth <= 0.0;
        }
    }

    public override IEnumerator TakeDamage(float damage)
    {
        yield return null;
    }
}
