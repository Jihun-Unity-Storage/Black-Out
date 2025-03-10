using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHead : MonoBehaviour
{
    public GameObject player; // 플레이어의 Transform을 할당합니다.

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.x < transform.position.x)
            {
                // 플레이어가 왼쪽에 있을 때 왼쪽을 바라보도록 스케일을 조정
                transform.localScale = new Vector3(gameObject.transform.localScale.y , gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            else if (player.transform.position.x > transform.position.x)
            {
                // 플레이어가 오른쪽에 있을 때 오른쪽을 바라보도록 스케일을 복원
                transform.localScale = new Vector3(-gameObject.transform.localScale.y , gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
    }
}
