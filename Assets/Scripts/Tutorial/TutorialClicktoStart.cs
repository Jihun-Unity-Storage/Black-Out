using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClicktoStart : MonoBehaviour
{
    public GameObject player, playerani, paper, spot, quest;

    public static TutorialClicktoStart instance;

    private void Awake()
    {
        instance = this; 
    }

    public void ClicktoStart()
    {
        player.SetActive(true);
        player.transform.position = playerani.transform.position;
        playerani.SetActive(false);
        paper.SetActive(false);
        spot.SetActive(true);
        TutorialManager.instance.StartCoroutine("TutorialPhase1GotoBitunro");
        gameObject.SetActive(false);
    }
}
