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
        // ����ü�� �÷��̾�� �浹�ϸ� ���ظ� ������ ����ü�� ����
        if (other.CompareTag("Player"))
        {
            // ���ظ� ������ ������ �����ϼ���
            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(damage);
            //}
            Debug.Log("���Ÿ� ���� ó����");
            player.Hitted(damage);
        }
        
    }
}
