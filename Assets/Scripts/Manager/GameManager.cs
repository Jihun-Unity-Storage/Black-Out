using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Debugging UI")]
    public Text debugTextPlayerONOFF;
    public static bool isTimeflow = true;
    // �Ͻ������� �ʿ��� ���� ,�ð��� �帣���� �� �帣���� Ȯ���Ѵ�
    public static bool isPlayerON = true;
    public static string lightType = "mouse";
    public static float volumeValue = 0.475f;
    public static int TypingSpeedtype;
    // 1 빠르게 2 중간 3 느리게
    public static float typingSpeed = 0.08f;

    public GameObject fadeOut;
    public GameObject textGameOver;

    private void Update()
    {
        if(TypingSpeedtype == 1) typingSpeed = 0.001f; 
        else if(TypingSpeedtype == 3) typingSpeed = 0.18f;
        else typingSpeed = 0.08f;
    }

    public void PLayerONOFF()
    {
        isPlayerON = !isPlayerON;
        debugTextPlayerONOFF.text = "PlayerON" + isPlayerON;
    }

    public IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(1.35f);
        fadeOut.SetActive(true);
        fadeOut.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        textGameOver.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartScene");
    }
}