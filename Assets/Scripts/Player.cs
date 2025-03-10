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
    // �������� ����� ����
    public Text Player_hp_txt;
    public Image[] heartImages; // ��Ʈ �̹��� �迭
    public Sprite fullHeart; // �� �� ��Ʈ ��������Ʈ
    public Sprite halfHeart; // �ݸ� �� ��Ʈ ��������Ʈ
    public Sprite emptyHeart; // �� ��Ʈ ��������Ʈ

    private int maxHearts = 5; // ��ü ��Ʈ ����

    private Animator ani;
    //private static Player instance = null;

    //void Awake()
    //{
    //    if (null == instance)
    //    {
    //        //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
    //        instance = this;

    //        //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
    //        //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
    //        //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
    //        //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
    //        //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
    //        Destroy(this.gameObject);
    //    }
    //}

    ////���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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
            //Player_hp_txt.text = "���� �ְ͋M!";
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
            Debug.Log("�� �Ȥ��ڴ���");
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
        int fullHearts = Player_hp / 20; // �� �� ��Ʈ ����
        int halfHearts = (Player_hp % 20) / 10; // �ݸ� �� ��Ʈ ����

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
