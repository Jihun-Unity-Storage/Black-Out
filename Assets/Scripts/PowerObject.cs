using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerObject : MonoBehaviour
{
    public GameObject lightPrefab; // 소환할 네모 오브젝트 프리팹
    public float maxPower = 100.0f; // 최대 전력 양
    public float chargingRate = 5.0f; // 초당 전력 충전량
    public float currentPower = 0.0f;
    private bool isSpawning = false;
    private GameObject spawnedObject; // 생성된 네모 오브젝트


    public bool isCharging;
    
    void Update()
    {
        if (isCharging)
        {
            ChargePower();
        }
     //

        // 네모 오브젝트를 소환
        SpawnObject();
    }

    void ChargePower()
    {
        // 전력을 충전하는 로직
        if (currentPower < maxPower && !isSpawning)
        {
            currentPower += chargingRate * Time.deltaTime;
        }
    }
    private void Start()
    {
        spawnedObject = Instantiate(lightPrefab, transform.position, Quaternion.identity);
    }
    void SpawnObject()
    {
        // 전력이 충분하고 아직 소환 중이 아닌 경우
        if (currentPower > 0 &&!isCharging)
        {
            isSpawning = true;
            spawnedObject.SetActive(true);
            currentPower -= chargingRate * 2 * Time.deltaTime;
        }else if(currentPower <=0)
        {
            isSpawning = false;
            spawnedObject.SetActive(false);
        }
    }

    


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            isCharging = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            isCharging = false;
        }
    }
}
