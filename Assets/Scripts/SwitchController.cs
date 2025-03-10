using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchController : MonoBehaviour
{
    public GameObject targetObject; // 스위치로 제어할 대상 오브젝트
    private bool isSwitchedOn = false; // 스위치의 초기 상태

    private void Start()
    {
        if (targetObject != null)
        {
            // 초기 상태에 따라 대상 오브젝트의 활성/비활성 상태 설정
            targetObject.SetActive(isSwitchedOn);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light")) // 플레이어와 충돌한 경우
        {
            ToggleSwitch(); // 스위치 상태 변경
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light")) // 플레이어와 충돌이 끝난 경우
        {
            ToggleSwitch(); // 스위치 상태 변경
        }
    }

    private void ToggleSwitch()
    {
        isSwitchedOn = !isSwitchedOn; // 스위치 상태 반전

        // 대상 오브젝트의 활성/비활성 상태 변경
        if (targetObject != null)
        {
            targetObject.SetActive(isSwitchedOn);
        }
    }
}
