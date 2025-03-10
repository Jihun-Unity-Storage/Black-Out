using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Biglight_Box : MonoBehaviour
{
    public Transform endPoint;   // 이동 끝 위치
    public float speed = 2.0f;   // 이동 속도
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
