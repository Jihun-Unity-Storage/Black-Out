using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLightMouse : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동 속도

    void Update()
    {
        // 마우스 위치를 가져와서 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Z 축 값을 고정하거나 조절할 수 있습니다 (예: 0)
        mousePosition.z = 0;

        // 현재 오브젝트를 마우스 위치 방향으로 이동
        transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
    }
}
