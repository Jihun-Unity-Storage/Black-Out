using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public bool isMusuk = false;
    public bool isMusuktime = false;
    // 무적으로 만드는 변수
    public Text Player_hp_txt;
    public Image[] heartImages; // 하트 이미지 배열
    public Sprite fullHeart; // 꽉 찬 하트 스프라이트
    public Sprite halfHeart; // 반만 찬 하트 스프라이트
    public Sprite emptyHeart; // 빈 하트 스프라이트

    private int maxHearts = 5; // 전체 하트 개수

    private Animator ani;
    //private static Player instance = null;

    //void Awake()
    //{
    //    if (null == instance)
    //    {
    //        //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
    //        instance = this;

    //        //씬 전환이 되더라도 파괴되지 않게 한다.
    //        //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
    //        //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
    //        //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
    //        //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
    //        Destroy(this.gameObject);
    //    }
    //}

    ////게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    //public static Player Instance
    //{
    //    get
    //    {
    //        if (null == instance)
    //        {
    //            return null;
    //        }
    //        return instance;
    //    }
    //}

    public int Player_hp;
    public int Start_hp = 100;
    // Start is called before the first frame update
    void Start()
    {
        isMusuktime = false;
        ani = GetComponent<Animator>();
        Player_hp = Start_hp;
    }
    void Update()
    {
        //Player_hp_txt.text = Player_hp.ToString();
        if (isMusuk) Player_hp = Start_hp;
            
        if (Player_hp <= 0)
        {
            //Player_hp_txt.text = "으악 주것딲!";
        }
        UpdateHealthUI();
    }

    public void Hitted(int damage)
    {
        if (!isMusuktime)
        {

            ani.SetTrigger("Hitted");
            Player_hp -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            Hitted(25);
            StartCoroutine("LightHit");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            Debug.Log("쳐 안ㅁ자는중");
            StopCoroutine("LightHit");
        }
    }

    IEnumerator LightHit()
    {
        while (true)
        {
            Hitted(25);
            yield return new WaitForSeconds(2);
        }
    }

    private void UpdateHealthUI()
    {
        int fullHearts = Player_hp / 20; // 꽉 찬 하트 개수
        int halfHearts = (Player_hp % 20) / 10; // 반만 찬 하트 개수

        for (int i = 0; i < maxHearts; i++)
        {
            if (i < fullHearts)
            {
                heartImages[i].sprite = fullHeart;
            }
            else if (i == fullHearts && halfHearts > 0)
            {
                heartImages[i].sprite = halfHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }
}
