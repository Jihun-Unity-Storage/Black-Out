using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject doubleCheckExit;
    // 게임을 진짜로 종료할것인지 한번더 확인하는 오브젝트
    [SerializeField] private string inGameSceneName;
    // 인게임 Scene 제목

    public GameObject tutorialCheckWindow;

    public GameObject btnArrow, btnMouse;
    public Slider sliderVolume;
    public GameObject menuPanel;

    public string[] strTips;
    //public Text Player_hp_txt;
    //private Player player;
    private void Start()
    {
        //player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        //Player_hp_txt.text = player.Player_hp.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuPanel.activeSelf) MenuClose();
            else MenuOpen();
        }
        if (btnArrow != null && btnMouse != null)
        {
            if (GameManager.lightType == "arrow")
            {
                btnArrow.GetComponent<Image>().color = Color.gray;
                btnMouse.GetComponent<Image>().color = Color.white;
            }
            else if (GameManager.lightType == "mouse")
            {
                btnArrow.GetComponent<Image>().color = Color.white;
                btnMouse.GetComponent<Image>().color = Color.gray;
            }
        }

        if (sliderVolume != null)
        {
            gameObject.GetComponent<AudioSource>().volume = sliderVolume.value;
        }
    }

    public void BtnSpeedQuick()
    {
        GameManager.TypingSpeedtype = 1;
    }
    
    public void BtnSpeedNormal()
    {
        GameManager.TypingSpeedtype = 2;
    }
    
    public void BtnSpeedSlow()
    {
        GameManager.TypingSpeedtype = 3;
    }
    
    public void GameStartBtn()
    {
        SceneManager.LoadScene(inGameSceneName);
    }

    public void GameExit()
    {
        GameManager.isTimeflow = false;
        doubleCheckExit.SetActive(true);
    }

    public void GameExitTrue()
    {
        Application.Quit(0);
    }

    public void GameExitFalse()
    {
        GameManager.isTimeflow = true;
        doubleCheckExit .SetActive(false);
    }

    public void TutorialStart()
    {
        SceneManager.LoadScene("TutorialScene");
        
    }

    public void Stage1Start()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void isTutorialWindow()
    {
        tutorialCheckWindow.SetActive(true);
    }
    
    public void Switch_Arrow()
    {
        GameManager.lightType = "arrow";
    }

    public void Switch_Mouse()
    {
        GameManager.lightType = "mouse";
    }

    public void MenuOpen()
    {
        menuPanel.SetActive(true);
    }

    public void MenuClose()
    {
        menuPanel.GetComponent<Animator>().SetTrigger("Close");
        Invoke("LateClose", 2);
    }

    void LateClose()
    {
        menuPanel.SetActive(false);
    }
}
