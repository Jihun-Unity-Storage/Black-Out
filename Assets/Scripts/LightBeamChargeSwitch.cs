using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LightBeamChargeSwitch : MonoBehaviour
{

    public float height = 1.2f;
    public Image ChargeBar;
    //public Sprite Switch_pushed;
    public GameObject light_back;
    public bool isPushed = false;
    public float currentAlpha;
    private bool movingToEnd = true;
    private Vector3 targetPosition;
    public GameObject charge_effect;
    public SpriteRenderer Light_Square_spriterd;
    public Sprite lightoff;
    public Sprite lighton;

    public GameObject Light_Beam;
    private SpriteRenderer spriteRenderer;

    public bool isCharging;
    public float maxPower = 100.0f; // 최대 전력 양
    public float chargingRate = 5.0f; // 초당 전력 충전량
    public float currentPower = 0.0f;

    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Light_Beam.gameObject.SetActive(false);
        Light_Square_spriterd.sprite = lightoff;
        spriteRenderer = light_back.GetComponent<SpriteRenderer>();
        isOn = false;
        isPushed = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 _hpBarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        // 체력 바의 위치 업데이트
        ChargeBar.GetComponent<RectTransform>().position = _hpBarpos;
        // 플랫폼을 이동할 위치로 이동
        if (isPushed)
        {

            
        }
        if (isCharging)
        {
            ChargePower();
        }
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
        }
        else if (!isOn)
        {
            charge_effect.SetActive(true);
            float attackAnimationTime = 0.5f;
            Invoke("DeactivateAttackPrefab", attackAnimationTime);
            isPushed = true;
            isOn = true;
            Light_Beam.SetActive(true);
            Light_Square_spriterd.sprite = lighton;
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
