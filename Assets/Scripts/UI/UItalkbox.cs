using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UItalkbox : MonoBehaviour
{
    private GameObject nowBox;
    public string[] strText;
    public string strTextName;
    public int boxID;
    
    private Text txtDetail;
    private Text txtName;
    private GameObject btnNext;
    
    public int strMaxCount;
    public int strCount = 0;

    private void Awake()
    {
        txtName = GameObject.Find("TextName").GetComponent<Text>();
        txtDetail = GameObject.Find("Text").GetComponent<Text>();
        btnNext = GameObject.Find("NextTextingBtn");
    }

    private void Start()
    {
        strMaxCount = strText.Length;
        nowBox = gameObject;
        if (strTextName == null) txtName.text = "NULL";
        else txtName.text = strTextName;
        TextingStart();
    }

    public void TextingStart()
    {
        if(strText.Length == 0) Debug.LogError("UItalkBox에 작성할 텍스트를 집어넣으세요");
        StartCoroutine(Typing());
    }
    
    IEnumerator Typing()
    {
        int i;
        btnNext.SetActive(false);
        for (i = 0; i <= strText[strCount].Length; i++)
        {
            txtDetail.text = strText[strCount].Substring(0, i);
            yield return new WaitForSeconds(GameManager.typingSpeed);
        }
        StopCoroutine(Typing());
        btnNext.SetActive(true);
    }

    public void NextTexting()
    {
        if (strCount + 1 == strMaxCount)
        {
            Debug.Log("UItalkbox 오브젝트의 str 마지막 문장입니다.");
            TextingEnding();
        } else {
            strCount += 1;
            StartCoroutine(Typing());
        }
    }

    void TextingEnding()
    {
        switch (boxID) // box에 따른 다른 이벤트 발생
        {
            case -2:
                TutorialClicktoStart.instance.ClicktoStart();
                gameObject.SetActive(false);
                break;
            case -1:
                gameObject.SetActive(false);
                break;
            case 0:  // 어둠이 대사
                TutorialManager.instance.StartCoroutine("TutorialPhase1Corutine");
                gameObject.SetActive(false);
                break;
            case 1: // 빛의 원로 대사
                TutorialManager.instance.StartCoroutine("CameraMoveToTop");
                gameObject.SetActive(false);
                break;
            case 2 :
                TutorialManager.instance.StartCoroutine("TutorialPhase2");
                gameObject.SetActive(false);
                break;
            case 3:
                TutorialManager.instance.StartCoroutine("Map2TutorialPhase1");
                gameObject.SetActive(false);
                break;
            case 4:
                TutorialManager.instance.StartCoroutine("Map2TutorialPhase2");
                gameObject.SetActive(false);
                break;
            case 5:
                TutorialManager.instance.StartCoroutine("Map2TutorialPhase3");
                gameObject.SetActive(false);
                break;
            case 6:
                TutorialManager.instance.StartCoroutine("Map2TutorialPhase4");
                gameObject.SetActive(false);
                break;
            case 7:
                GameManager.isPlayerON = true;
                gameObject.SetActive(false);
                break;
            
            case 10:
                // 엔딩
            
                EndingScnee.instance.EndingSelectStart();
                gameObject.SetActive(false); 
                
                break;
            
            case 11:
                EndingScnee.instance.BadEndingShowStart();
                gameObject.SetActive(false);
                break;
            case 12:
                gameObject.SetActive(false);
                EndingScnee.instance.BadENdingShow2();
                break;
            case 13:
                gameObject.SetActive(false);
                EndingScnee.instance.Domang_Ending2();
                break;
            
            case 14:
                gameObject.SetActive(false);
                EndingScnee.instance.Doduk_Ending2();
                break;
        }
    }
}
