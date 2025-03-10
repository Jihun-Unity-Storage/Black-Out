using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterChasePlayer : MonoBehaviour
{
    private Transform player; // 플레이어의 Transform 컴포넌트
    public float chaseDistance = 10.0f; // 몬스터가 플레이어를 쫓아가는 거리
    public float moveSpeed = 5.0f; // 몬스터의 이동 속도
    [SerializeField]
    private bool isChasing = false;
    private Vector3 initialPosition; // 몬스터의 초기 위치
    private float raycastDistance = 1.0f; // 레이캐스트 거리
    public bool isCliff;
    private Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
        initialPosition = transform.position;
        player = GameObject.Find("Player").GetComponent<Transform>();
        isCliff = false;
    }

    void Update()
    {
        if(gameObject.GetComponent<Monster_GunJiup>().isMontserOn == false) return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 플레이어와 몬스터 사이의 거리가 chaseDistance 이하인 경우 쫓아감
        if (distanceToPlayer <= chaseDistance && isCliff == false)
        {
            ani.SetBool("Run", true);
            isChasing = true;
        }
        else
        {
            ani.SetBool("Run", false);
            isChasing = false;
        }

        // 플레이어 쫓아가는 로직
        if (isChasing)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        }
        else
        {
            float distanceToInitialPosition = Vector3.Distance(transform.position, initialPosition);
            if (distanceToInitialPosition > 1.0f)
            {
                Vector3 moveDirection = (initialPosition - transform.position).normalized;
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hello collision");
        if (collision.gameObject.CompareTag("CliffCollider"))
        {
            //Debug.Log("hello cliff");
            // 낭떠러지를 감지하면 멈춤
            isCliff = true;
            isChasing = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CliffCollider"))
        {
            //Debug.Log("hello cliff");
            
            isCliff = false ;
            
        }
    }
}
