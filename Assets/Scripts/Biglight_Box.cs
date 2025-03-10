using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Biglight_Box : MonoBehaviour
{
    public Transform endPoint;   // �̵� �� ��ġ
    public float speed = 2.0f;   // �̵� �ӵ�
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = endPoint.position; 

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
    }
}
