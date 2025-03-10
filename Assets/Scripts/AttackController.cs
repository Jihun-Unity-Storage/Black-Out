using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int comboCount;
    private bool isAttacking = false;
    public float comboAttackDuration;
    public float comboTimer;

    public GameObject attck1, attack2;

    public Rigidbody2D playerRigid;

    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        playerRigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (comboCount == 1)
        {
            attck1.SetActive(true);
            attack2.SetActive(false);
        }
        
        else if (comboCount == 2)
        {
            attck1.SetActive(false);
            attack2.SetActive(true);
        }
        else
        {
            attck1.SetActive(false);
            attack2.SetActive(false);
        }
        
        if (comboCount >= 1)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                PlayerController.isAttack = false;
                comboTimer = 0.37f;
                comboCount = 0;
            }
        }
        if (comboCount == 2)
        {
            //Debug.Log("ÄÞº¸ Á¾·á");
            Invoke("ComboEnd", 0.2f);
        }
        UpdateWeaponSprite();
    }

    void ComboEnd()
    {
        PlayerController.isAttack = false;
        comboTimer = 0;
        comboCount = 0;
        comboTimer = 0.37f;
    }
    public void StartCombo()
    {
        playerRigid.velocity = Vector2.zero;
        comboCount = 0;
    }
    
    public void ContinueCombo()
    {
        if (comboCount <= 1)
        {
            playerRigid.velocity = Vector2.zero;
            comboTimer = 0.37f;
            comboCount++;
        }
    }

    void UpdateWeaponSprite()
    {
        ani.SetInteger("attack", comboCount);
    }
    
    int GetComboCount()
    {
        return comboCount;
    }

    
}
