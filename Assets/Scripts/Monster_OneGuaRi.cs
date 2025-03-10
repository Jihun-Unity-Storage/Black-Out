using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_OneGuaRi : MonoBehaviour
{
    public float detectionRange = 10.0f; // 플레이어 감지 범위
    public GameObject projectilePrefab; // 투사체 프리팹
    public float attackCooldown = 5.0f; // 공격 쿨다운 시간
    public List<Transform> firePoints; // 투사체 발사 지점 리스트
    public List<Projectile> projectiles;
    private Transform player;
    private float attackTimer;
    private int currentFirePointIndex = 0;

    public bool isMontserOn; // 몬스터가 현재 가동중인지

    private Animator ani;

    private void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        attackTimer = 0.0f;
    }

    private void Update()
    {
        if (isMontserOn == false) return; // 만약 가동중이 아니라면 실행안함
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            
            // 플레이어를 감지하면 공격을 시작
            if (attackTimer <= 0)
            {
                ani.SetTrigger("Attack");
                //Attack();
                Attack_onegiok();
                attackTimer = attackCooldown;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    private void Attack()
    {
        Debug.Log("오줌발싸!");
        // 투사체를 생성하고 발사
        if (projectilePrefab != null)
        {
            Transform currentFirePoint = firePoints[currentFirePointIndex];
            GameObject projectile = Instantiate(projectilePrefab, currentFirePoint.position, Quaternion.identity);
            Projectile script = projectile.GetComponent<Projectile>();

            if (script != null)
            {
                // 투사체에 방향을 설정하여 플레이어 방향으로 발사
                Vector2 direction = (player.position - currentFirePoint.position).normalized;
                script.Fire(direction);
            }

            // 다음 발사 위치로 인덱스 이동
            currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Count;
        }
    }
    private void Attack_onegiok()
    {

        // 생성을 다 했으면 한번에 발사
        if (currentFirePointIndex == firePoints.Count - 1)
        {
            currentFirePointIndex = 0;
            //foreach문 이용해서 각각의 발사체 오브젝트 발사
            foreach (Projectile proj in projectiles)
            {
                //각각 발사체의 projectiles 의 fire함수 실행해서 발사
                Vector2 direction = (player.position - firePoints[currentFirePointIndex].position).normalized;
                if(proj != null) proj.Fire(direction);
            }
            projectiles.Clear();

        }else
        {
            Debug.Log("오줌발싸!");
            // 투사체를 생성
            if (projectilePrefab != null)
            {
                Transform currentFirePoint = firePoints[currentFirePointIndex];
                GameObject projectile = Instantiate(projectilePrefab, currentFirePoint.position, Quaternion.identity);
                Projectile script = projectile.GetComponent<Projectile>();
                projectiles.Add(script);
                // 다음 생성 위치로 인덱스 이동
                currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Count;


            }
        }
        

    }
}
