using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSwitch : MonoBehaviour
{

    public GameObject camera;
    public GameObject doorPos, playerPos;
    
    public Sprite onSwitch; // 새로운 스프라이트를 설정할 변수

    public bool isOn = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light") && !isOn)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = onSwitch;
            camera.transform.position = doorPos.transform.position;
            isOn = true;
            Invoke("CameraReset", 2f);
        }
    }

    void CameraReset()
    {
        camera.transform.position = playerPos.transform.position;
    }
}