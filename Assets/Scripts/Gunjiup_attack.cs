using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunjiup_attack : MonoBehaviour
{
    private Player player;
    public int damage = 10;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 투사체가 플레이어에게 충돌하면 피해를 입히고 투사체를 제거
        if (other.CompareTag("Player"))
        {
            // 피해를 입히는 로직을 구현하세요
            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(damage);
            //}
            Debug.Log("원거리 공격 처맞음");
            player.Hitted(damage);
        }
        
    }
}
