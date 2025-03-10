using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_OneGuaRi : MonoBehaviour
{
    public float detectionRange = 10.0f; // �÷��̾� ���� ����
    public GameObject projectilePrefab; // ����ü ������
    public float attackCooldown = 5.0f; // ���� ��ٿ� �ð�
    public List<Transform> firePoints; // ����ü �߻� ���� ����Ʈ
    public List<Projectile> projectiles;
    private Transform player;
    private float attackTimer;
    private int currentFirePointIndex = 0;

    public bool isMontserOn; // ���Ͱ� ���� ����������

    private Animator ani;

    private void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        attackTimer = 0.0f;
    }

    private void Update()
    {
        if (isMontserOn == false) return; // ���� �������� �ƴ϶�� �������
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            
            // �÷��̾ �����ϸ� ������ ����
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
        Debug.Log("���ܹ߽�!");
        // ����ü�� �����ϰ� �߻�
        if (projectilePrefab != null)
        {
            Transform currentFirePoint = firePoints[currentFirePointIndex];
            GameObject projectile = Instantiate(projectilePrefab, currentFirePoint.position, Quaternion.identity);
            Projectile script = projectile.GetComponent<Projectile>();

            if (script != null)
            {
                // ����ü�� ������ �����Ͽ� �÷��̾� �������� �߻�
                Vector2 direction = (player.position - currentFirePoint.position).normalized;
                script.Fire(direction);
            }

            // ���� �߻� ��ġ�� �ε��� �̵�
            currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Count;
        }
    }
    private void Attack_onegiok()
    {

        // ������ �� ������ �ѹ��� �߻�
        if (currentFirePointIndex == firePoints.Count - 1)
        {
            currentFirePointIndex = 0;
            //foreach�� �̿��ؼ� ������ �߻�ü ������Ʈ �߻�
            foreach (Projectile proj in projectiles)
            {
                //���� �߻�ü�� projectiles �� fire�Լ� �����ؼ� �߻�
                Vector2 direction = (player.position - firePoints[currentFirePointIndex].position).normalized;
                if(proj != null) proj.Fire(direction);
            }
            projectiles.Clear();

        }else
        {
            Debug.Log("���ܹ߽�!");
            // ����ü�� ����
            if (projectilePrefab != null)
            {
                Transform currentFirePoint = firePoints[currentFirePointIndex];
                GameObject projectile = Instantiate(projectilePrefab, currentFirePoint.position, Quaternion.identity);
                Projectile script = projectile.GetComponent<Projectile>();
                projectiles.Add(script);
                // ���� ���� ��ġ�� �ε��� �̵�
                currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Count;


            }
        }
        

    }
}
