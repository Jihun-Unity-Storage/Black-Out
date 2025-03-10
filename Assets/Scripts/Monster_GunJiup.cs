using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_GunJiup : MonoBehaviour
{
    public GameObject Attack_prefeb;
    public Material chargingMaterial; 
    public float health = 100.0f;
    public float moveSpeed = 3.0f;
    public float attackChargeTime = 1.0f;
    public float attackCooldownTime = 3.0f;
    public float attackRange = 5.0f;
    public float jumpForce = 10.0f;
    public float player_distance = 0f;
    private Transform player;
    private Rigidbody2D rb;
    private bool isCharging = false;
    private bool isAttacking = false;
    private float attackChargeTimer = 0.0f;
    private float attackCooldownTimer = 0.0f;
    private float chargeStartTime = 0.0f;
   //private Color originalColor;
    //private SpriteRenderer renderer;
    
    public bool isMontserOn = false; // ���Ͱ� ���� ����������

    private Animator ani;
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        Attack_prefeb.gameObject.SetActive(false);

        //renderer = GetComponent <SpriteRenderer>();

        // ������Ʈ�� �ʱ� ���� ����
        //originalColor = renderer.material.color;
    }

    void Update()
    {
        if (isMontserOn == false) return; // ���� �������� �ƴ϶�� �������

        player_distance = Vector2.Distance(this.transform.position, player.position);
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (!isCharging)
            {
                ani.SetTrigger("Attack");
                StartCharging();
            }

            ChargeAttack();
        }
        else
        {
            EndCharging();
        }

        if (isAttacking)
        {
            attackCooldownTimer += Time.deltaTime;
            if (attackCooldownTimer >= attackCooldownTime)
            {
                isAttacking = false;
                attackCooldownTimer = 0.0f;
            }
        }

        if (isCharging)
        {
            attackChargeTimer += Time.deltaTime;
            UpdateChargeVisual();
            if (attackChargeTimer >= attackChargeTime)
            {
                Attack();
            }
        }
    }

    void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
    }

    void EndCharging()
    {
        isCharging = false;
        attackChargeTimer = 0.0f;
        //renderer.material.color = originalColor;
    }

    void ChargeAttack()
    {
        if (isCharging)
        {
            
        }
    }

    void UpdateChargeVisual()
    {
        // ���� �ð��� ���� ������ ��Ƽ����� ����
        float chargeProgress = Mathf.Clamp01(attackChargeTimer / attackChargeTime);
        //renderer.material.color = Color.Lerp(originalColor, Color.red, chargeProgress);
    }

    void Attack()
    {
        isCharging = false;
        isAttacking = true;
        attackChargeTimer = 0.0f;
        //renderer.material.color = originalColor;
        Attack_prefeb.SetActive(true);
        //Debug.Log("����");
        float attackAnimationTime = 0.5f;
        Invoke("DeactivateAttackPrefab", attackAnimationTime);
    }

    void DeactivateAttackPrefab()
    {
        Attack_prefeb.SetActive(false);
    }
}
