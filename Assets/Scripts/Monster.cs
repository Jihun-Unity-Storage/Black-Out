using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject healthBarPrefab; // ü�� �� ������
    private GameObject healthBar; // ������ ü�� �� ������Ʈ
    private MonsterHealthBar healthBarScript; // ü�� �� ��ũ��Ʈ
    private RectTransform healthBarRectTransform; // ü�� ���� RectTransform ������Ʈ
    public float maxHealth = 100.0f; // ������ �ִ� ü��
    public float currentHealth; // ���� ü��
    private GameObject canvas;
    public float height = -1.2f;
    public bool isDead = false;
    
    
    private Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
        currentHealth = maxHealth;
        canvas = GameObject.Find("Canvas");
        // ���Ͱ� ������ �� ü�� �� ����
        healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBar.transform.SetParent(canvas.transform); // Canvas�� ���� (Canvas �̸��� �°� ����)
        healthBarRectTransform = healthBar.GetComponent<RectTransform>();
        // ü�� �� ��ũ��Ʈ�� ���� ������ ����
        healthBarScript = healthBar.GetComponent<MonsterHealthBar>();
        if (healthBarScript != null)
        {
            healthBarScript.SetMonster(this);
        }
    }

    public float GetCurrentHealth()
    {
        //Debug.Log(currentHealth);
        return currentHealth;
    }

    // �ִ� ü���� ��ȯ
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    private void Update()
    {
        ani.SetBool("Dead", isDead);
        if (currentHealth <= 0) isDead = true;
        if (isDead && gameObject.GetComponent<Monster_GunJiup>()) gameObject.GetComponent<Monster_GunJiup>().isMontserOn = false;
        else if (isDead && gameObject.GetComponent<Monster_OneGuaRi>()) gameObject.GetComponent<Monster_OneGuaRi>().isMontserOn = false;
        if(isDead) Invoke("Dead", 2f);
        // ���� ��ġ�� ���� ��ǥ�� ������
        Vector3 _hpBarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        // ü�� ���� ��ġ ������Ʈ
        healthBar.GetComponent<RectTransform>().position = _hpBarpos;
    }

    public void Hitted(float damage)
    {
        ani.SetTrigger("Hitted");
        currentHealth -= damage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Light")
        {
            //Debug.Log("Light collision detected");
            Hitted(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            Debug.Log("���� ���� �¾Ҵ�");
            Hitted(30);
        }
    }

    public void Dead()
    {
         gameObject.SetActive(false);
    }


}
