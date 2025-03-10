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
    public Transform platformToMove; // 이동할 플랫폼
    //public Transform startPoint; // 이동 시작 위치
    public Transform endPoint;   // 이동 끝 위치
    public float speed = 2.0f;   // 이동 속도
    public float currentAlpha;
    private bool movingToEnd = true;
    private Vector3 targetPosition;
    public GameObject charge_effect;
    //public GameObject camera;
    //public Transform playerPos, InteractionPos;
    public bool isMove = false;
    private SpriteRenderer spriteRenderer;

    public bool isCharging;
    public float maxPower = 100.0f; // 최대 전력 양
    public float chargingRate = 5.0f; // 초당 전력 충전량
    public float currentPower = 0.0f;


    private void Start()
    {
        isMove = false;
        //startPoint.position = platformToMove.position;
        targetPosition = endPoint.position; // 시작할 때는 끝 위치로 이동
        isPushed = false;
        spriteRenderer = light_back.GetComponent < SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer 컴포넌트를 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        Vector3 _hpBarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        // 체력 바의 위치 업데이트
        ChargeBar.GetComponent<RectTransform>().position = _hpBarpos;
        // 플랫폼을 이동할 위치로 이동
        if (isPushed)
        {

            platformToMove.position = Vector3.MoveTowards(platformToMove.position, targetPosition, speed * Time.deltaTime);
        }
        if (isCharging)
        {
            ChargePower();
        }
        // 목표 위치에 도달하면 이동 중지
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
        // 전력을 충전하는 로직
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
