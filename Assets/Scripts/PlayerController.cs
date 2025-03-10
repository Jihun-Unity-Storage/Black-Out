using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    private bool isGrounded = true;
    private bool isFacingRight = true;
    private int jumpsRemaining = 1;
    private float lastAttackTime;
    public float comboAttackWindow; // 콤보 공격 윈도우 시간
    public static bool isAttack; // 공격하고있는지 감지

    public LayerMask groundLayer; // 땅을 감지할 레이어 마스크
    public Transform groundCheckTransform; // 땅을 감지할 레이캐스트 시작 위치
    public float groundCheckDistance = 0.1f; // 레이캐스트 거리
    
    private AttackController attackController;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D col2D;

    public bool isPlayerDontMove;
    // 스턴 걸리게 하는 역할
    public bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackController = GetComponent < AttackController>();
        col2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // 맞았을때 스턴
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hitted") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) isPlayerDontMove = false;
        else isPlayerDontMove = true;
        
        // 죽음 
        if (gameObject.GetComponent<Player>().Player_hp <= 0 && !isDead)
        {
            animator.SetBool("Dead", true);
            animator.SetTrigger("Dead 0");
            GameManager.instance.StartCoroutine("PlayerDead");
            isDead = true;
        }

        if (isDead) isPlayerDontMove = true;
        
        AnimationPlay();
        // 이동 로직
        if (!GameManager.isPlayerON) rb.velocity = Vector2.zero;
        float horizontalInput = Input.GetAxis("Horizontal");
        if(isAttack == false && GameManager.isPlayerON && isPlayerDontMove) rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        

        // 점프
        if (((isGrounded || jumpsRemaining > 0) && GameManager.isPlayerON && isPlayerDontMove) && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded)
            {
                jumpsRemaining--;
            }

            rb.velocity = new Vector2(rb.velocity.x, 0); // 현재 수직 속도를 초기화
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Ctrl 키를 눌렀을 때 공격 스크립트를 호출
        if (Input.GetKeyDown(KeyCode.LeftControl) && GameManager.isPlayerON && isPlayerDontMove)
        {
            if (Time.time - lastAttackTime <= comboAttackWindow)
            {
                attackController.ContinueCombo();
            }
            else
            {
                attackController.StartCombo();
            }
            lastAttackTime = Time.time;
        }

        // 레이캐스트를 사용하여 땅을 감지
        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        //Debug.DrawRay(col2D.bounds.center, Vector2.down * 0.02f, Color.red);

        if (raycastHit.collider != null)
        {
            //Debug.Log("floor gdgd");
            // 바닥에 닿으면 점프 횟수 초기화
            jumpsRemaining = 1;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }


    void AnimationPlay()
    {
        if (!GameManager.isPlayerON || !isPlayerDontMove)
        {
            animator.SetBool("Run", false);
            return;
        }
        Vector2 velocity = rb.velocity;
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            animator.SetBool("Run", true);
        }
        else if (horizontalInput < 0)
        {
            animator.SetBool("Run", true);
        }
        else if (horizontalInput == 0)
        {
            animator.SetBool("Run", false);
        }
        
        if (velocity.y > 0)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Landing", false);
        }
        else if (velocity.y < 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Landing", true);
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Landing", false);

        }
        
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
    }
}
