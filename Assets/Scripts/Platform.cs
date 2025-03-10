using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Platform : MonoBehaviour
{
    bool playerCheck;
    [SerializeField]
    private GameObject currentOneWayPlatform;

    [SerializeField] private BoxCollider2D playerCollider;

    private LayerMask playerLayer; // 플레이어 레이어 마스크

    private void Start()
    {
        // 플레이어 레이어 마스크 설정 (Player 레이어에 대한 마스크)
        playerLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private IEnumerator DisableCollision()
    {
        TilemapCollider2D platformCollider = currentOneWayPlatform.GetComponent<TilemapCollider2D>();
        Debug.Log(platformCollider);

        // 충돌 무시 설정, playerLayer를 통해 Player 레이어만 충돌을 무시
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);

        yield return new WaitForSeconds(0.5f);

        // 충돌 무시 해제
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }


}
