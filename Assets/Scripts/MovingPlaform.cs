using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    public Transform pointA; // �� ���� ������Ʈ A

    [SerializeField]
    public Transform pointB; // �� ���� ������Ʈ B

    [SerializeField]
    private float moveSpeed = 5.0f; // �÷����� �̵� �ӵ�
    [SerializeField]
    private Transform currentTarget; // ���� �̵� ���� ��ǥ ��ġ

    private void Start()
    {
        // �ʱ� ��ġ�� A�� ����
        currentTarget = pointA;
    }

    private void Update()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
       // Debug.Log((Vector2.Distance(transform.position, currentTarget.position)));
        // ��ǥ ��ġ�� �����ϸ� ���� ��ǥ ��ġ�� ����
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            SwitchTarget();
        }
    }

    private void SwitchTarget()
    {
        // ���� ��ǥ ��ġ�� �ݴ�� ����
        currentTarget = (currentTarget == pointA) ? pointB : pointA;
    }
}
