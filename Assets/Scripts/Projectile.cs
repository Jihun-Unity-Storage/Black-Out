using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f; // 투사체 이동 속도
    public int damage = 10; // 투사체 피해량
    private Vector2 direction;
    private Player player;
    // 투사체에 방향을 설정하고 이동을 시작

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Destroy(gameObject, 10f);
    }
    public void Fire(Vector2 fireDirection)
    {
        direction = fireDirection;
        // 방향 벡터에 따라 투사체 이동
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

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
            Destroy(gameObject);
        }
        else if(!other.CompareTag("Monster"))
        {
            // 다른 충돌 대상에 닿았을 때도 투사체를 제거
            Destroy(gameObject);
        }
    }
}
