using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LightSwitch_platform : MonoBehaviour
{
    public float height = 1.2f;
    public Image ChargeBar;
    //public Sprite Switch_pushed;
    public GameObject light_back;
    public bool isPushed = false;
    public Transform platformToMove; // �̵��� �÷���
    //public Transform startPoint; // �̵� ���� ��ġ
    public Transform endPoint;   // �̵� �� ��ġ
    public float speed = 2.0f;   // �̵� �ӵ�
    public float currentAlpha;
    private bool movingToEnd = true;
    private Vector3 targetPosition;
    public GameObject charge_effect;
    //public GameObject camera;
    //public Transform playerPos, InteractionPos;
    public bool isMove = false;
    private SpriteRenderer spriteRenderer;

    public bool isCharging;
    public float maxPower = 100.0f; // �ִ� ���� ��
    public float chargingRate = 5.0f; // �ʴ� ���� ������
    public float currentPower = 0.0f;


    private void Start()
    {
        isMove = false;
        //startPoint.position = platformToMove.position;
        targetPosition = endPoint.position; // ������ ���� �� ��ġ�� �̵�
        isPushed = false;
        spriteRenderer = light_back.GetComponent < SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void Update()
    {
        Vector3 _hpBarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        // ü�� ���� ��ġ ������Ʈ
        ChargeBar.GetComponent<RectTransform>().position = _hpBarpos;
        // �÷����� �̵��� ��ġ�� �̵�
        if (isPushed)
        {

            platformToMove.position = Vector3.MoveTowards(platformToMove.position, targetPosition, speed * Time.deltaTime);
        }
        if (isCharging)
        {
            ChargePower();
        }
        // ��ǥ ��ġ�� �����ϸ� �̵� ����
        //if (Vector3.Distance(platformToMove.position, targetPosition) < 0.01f)
        //{
        //    movingToEnd = !movingToEnd;
        //    targetPosition = movingToEnd ? endPoint.position : startPoint.position;
        //}
        ChargeBar.fillAmount = currentPower / maxPower;
        currentAlpha = currentPower / 100f;
        Color color = spriteRenderer.color;
        color.a = currentAlpha;
        spriteRenderer.color = color;
    }


    void ChargePower()
    {
        // ������ �����ϴ� ����
        if (currentPower < maxPower)
        {
            currentPower += chargingRate * Time.deltaTime;
        }else if(!isMove)
        {
            charge_effect.SetActive(true);
            float attackAnimationTime = 0.5f;
            Invoke("DeactivateAttackPrefab", attackAnimationTime);
            isPushed = true;
            isMove = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            isCharging = true;
        }
    }
    void DeactivateAttackPrefab()
    {
        charge_effect.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            isCharging = false;
        }
    }
}
