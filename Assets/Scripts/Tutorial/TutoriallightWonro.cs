using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoriallightWonro : MonoBehaviour
{
    public GameObject textingBox1, player;
    public Transform adumPos_lightWonro;
    public Sprite playerSprite;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = adumPos_lightWonro.position;
            player.GetComponent<SpriteRenderer>().sprite = playerSprite;
            GameManager.isPlayerON = false;
            textingBox1.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
