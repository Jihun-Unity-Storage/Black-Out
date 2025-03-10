using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject healthBarPrefab; // 체력 바 프리팹
    private GameObject healthBar; // 생성된 체력 바 오브젝트
    private MonsterHealthBar healthBarScript; // 체력 바 스크립트
    private RectTransform healthBarRectTransform; // 체력 바의 RectTransform 컴포넌트
    public float maxHealth = 100.0f; // 몬스터의 최대 체력
    public float currentHealth; // 현재 체력
    private GameObject canvas;
    public float height = -1.2f;
    public bool isDead = false;
    
    
    private Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
        currentHealth = maxHealth;
        canvas = GameObject.Find("Canvas");
        // 몬스터가 스폰될 때 체력 바 생성
        healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBar.transform.SetParent(canvas.transform); // Canvas에 설정 (Canvas 이름에 맞게 수정)
        healthBarRectTransform = healthBar.GetComponent<RectTransform>();
        // 체력 바 스크립트에 몬스터 참조를 전달
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

    // 최대 체력을 반환
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
        // 몬스터 위치를 월드 좌표로 가져옴
        Vector3 _hpBarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        // 체력 바의 위치 업데이트
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
            Debug.Log("으악 공격 맞았다");
            Hitted(30);
        }
    }

    public void Dead()
    {
         gameObject.SetActive(false);
    }


}
