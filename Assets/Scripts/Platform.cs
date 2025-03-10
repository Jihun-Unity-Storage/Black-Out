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

    private LayerMask playerLayer; // �÷��̾� ���̾� ����ũ

    private void Start()
    {
        // �÷��̾� ���̾� ����ũ ���� (Player ���̾ ���� ����ũ)
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

        // �浹 ���� ����, playerLayer�� ���� Player ���̾ �浹�� ����
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);

        yield return new WaitForSeconds(0.5f);

        // �浹 ���� ����
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
