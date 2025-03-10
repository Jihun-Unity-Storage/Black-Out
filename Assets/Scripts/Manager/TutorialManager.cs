using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject player;
    public GameObject playerCamera;
    [SerializeField] private GameObject MainCamera;
    public Transform cameraPos;
    public GameObject fadeInOutEffect;
    public GameObject loadingWindow;
    public Slider loadingSlider;
    
    [Header("대화창들")]
    public GameObject TextingBox1;
    public GameObject TextingBox2;
    public GameObject TextingBox3;
    public GameObject map2TextingBox1;
    public GameObject map2TextingBox2;
    public GameObject map2TextingBox3;
    public GameObject map2TextingBox4;
    public GameObject map2TextingBox5;

    [Header("페이즈1에 사용될 변수들")]
    public GameObject lightElder;
    public GameObject quest0, quest0spot;

    [Header("페이즈2에 사용될 변수들")] 
    public Transform topTransform;
    public GameObject topSpot;
    public GameObject quest1, quest2, quest3;
    public GameObject posIntop;

    [Header("페이즈3에 사용될 변수들")] 
    public GameObject monster1;
    public GameObject playerLight;
    public GameObject wonMonster1, wonMonster2, wonMonster3;
    public GameObject map2quest1, map2quest2, map2quest3;
    public Transform lightPos, DoorPos;
    public GameObject _switch;

    public static bool isClearTutoroal = false;
    
    private void Start()
    {
        GameManager.isPlayerON = false;
        lightElder.SetActive(false);
        topSpot.SetActive(false);
        MainCamera.transform.position = cameraPos.position;
    }
    
     IEnumerator TutorialPhase1GotoBitunro()
     {  
         Debug.Log("튜토리얼 페이즈 1 시작");
         GameManager.isPlayerON = true;
         quest0.SetActive(true);
         quest0spot.SetActive(true);
        while (!condition)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) )
            {
                quest0.GetComponent<Animator>().SetTrigger("Clear");
                Debug.Log("조건이 충족했습니다.");
                condition = true;
            }
            yield return null;
        } condition = false;
    }
    
     
    

    private bool condition;
     IEnumerator TutorialPhase1Corutine()
     { 
         GameManager.isPlayerON = false;
        lightElder.SetActive(true);
        while (!condition)
        {
            if (lightElder.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("LightElderWork") &&
                lightElder.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Debug.Log("조건이 충족했습니다.");
                Debug.Log("LightElder의 Work애니메이션이 종료 되었습니다.");
                TextingBox2.SetActive(true);
                condition = true;
            }
            Debug.Log("조건을 충족하지 못했습니다.");
            yield return null;
        } condition = false;
    }

     public IEnumerator CameraMoveToTop()
     {
         playerCamera.transform.position = topTransform.position;
         yield return new WaitForSeconds(2);
         playerCamera.transform.position = player.transform.position;
         TextingBox2.SetActive(false);
         TextingBox3.SetActive(true);
     }
   
    public IEnumerator TutorialPhase2()
     {
         TextingBox3.SetActive(false);
         GameManager.isPlayerON = true;
         quest1.SetActive(true);
         while (!condition)
         {
             if (Input.GetKeyDown(KeyCode.Space))
             {
                 quest1.GetComponent<Animator>().SetTrigger("Clear");
                 condition = true;
             }
             yield return null;
         } condition = false;
         yield return new WaitForSeconds(0.5f);
         quest1.SetActive(false);
         
         quest2.SetActive(true);
         while (!condition)
         {
             if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack2") &&
                 player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
             {
                 quest2.GetComponent<Animator>().SetTrigger("Clear");
                 condition = true;
             }
             yield return null;
         } condition = false;
         
         yield return new WaitForSeconds(0.5f);
         quest2.SetActive(false);
         
         quest3.SetActive(true);
         topSpot.SetActive(true);
     }

    public IEnumerator IntoTop()
    {
        quest3.SetActive(false);
        fadeInOutEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = posIntop.transform.position;
        loadingWindow.SetActive(true);
        while (!condition)
        {
            if (loadingSlider.value >= 1)
            {
                fadeInOutEffect.GetComponent<Animator>().SetTrigger("FadeOut");
                condition = true;
            }
            yield return null;
        } condition = false;
        // 탑안에서 튜토리얼 시작
        GameManager.isPlayerON = false;
        
        monster1.SetActive(true);
        monster1.GetComponent<Monster_GunJiup>().isMontserOn = false;
        
        wonMonster1.SetActive(false);
        wonMonster1.GetComponent<Monster_OneGuaRi>().isMontserOn = false;
        
        wonMonster2.SetActive(false);
        wonMonster2.GetComponent<Monster_OneGuaRi>().isMontserOn = false;
        
        wonMonster3.SetActive(false);
        wonMonster3.GetComponent<Monster_OneGuaRi>().isMontserOn = false;
        
        map2TextingBox1.SetActive(true);
    }

    public IEnumerator Map2TutorialPhase1()
    {
        GameManager.isPlayerON = true;
        map2quest1.SetActive(true);
        monster1.SetActive(true);
        monster1.GetComponent<Monster_GunJiup>().isMontserOn = true;
        while (!condition)
        {
            if (monster1.GetComponent<Monster>().isDead)
            {
                map2quest1.GetComponent<Animator>().SetTrigger("Clear");
                condition = true;
            }
            yield return null;
        } condition = false;
        map2TextingBox2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        map2quest1.SetActive(false);
    }
    
    public IEnumerator Map2TutorialPhase2()
    {
        playerLight.SetActive(true);
        playerLight.GetComponent<FollowMouse>().isOn = false;
        yield return new WaitForSeconds(1f);
        GameManager.isPlayerON = false;
        map2TextingBox3.SetActive(true);
    }
    
    public IEnumerator Map2TutorialPhase3()
    {
        GameManager.isPlayerON = true;
        map2quest2.SetActive(true);
        playerLight.GetComponent<FollowMouse>().isOn = true;
        wonMonster1.GetComponent<Monster_OneGuaRi>().isMontserOn = true;
        wonMonster1.SetActive(true);
        wonMonster2.GetComponent<Monster_OneGuaRi>().isMontserOn = true;
        wonMonster2.SetActive(true);
        wonMonster3.GetComponent<Monster_OneGuaRi>().isMontserOn = true;
        wonMonster3.SetActive(true);

        while (!condition)
        {
            if (wonMonster1.GetComponent<Monster>().isDead && 
                wonMonster2.GetComponent<Monster>().isDead && 
                wonMonster3.GetComponent<Monster>().isDead)
            {
                map2quest2.GetComponent<Animator>().SetTrigger("Clear");
                condition = true;
                yield return new WaitForSeconds(0.5f);
                map2quest2.SetActive(false);
            }
            yield return null;
        } condition = false;
        map2TextingBox4.SetActive(true);
        GameManager.isPlayerON = false;
        
    }
    
    public IEnumerator Map2TutorialPhase4()
    {
        GameManager.isPlayerON = true;
        map2quest3.SetActive(true);
        _switch.SetActive(true);
        while (!condition)
        {
            if (_switch.GetComponent<TutorialSwitch>().isOn)
            {
                map2quest3.GetComponent<Animator>().SetTrigger("Clear");
                isClearTutoroal = true;
                condition = true;
                yield return new WaitForSeconds(0.5f);
                map2quest3.SetActive(false);
            }
            yield return null;
        } condition = false;
        map2TextingBox5.SetActive(true);
        GameManager.isPlayerON = false;
    }

    public IEnumerator TutorialEnd()
    {
        fadeInOutEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isClearTutoroal = true;
        loadingWindow.SetActive(true);
        while (!condition)
        {
            if (loadingSlider.value >= 1)
            {
                SceneManager.LoadScene("Stage1");
                condition = true;
            }
            yield return null;
        }
    }

}
