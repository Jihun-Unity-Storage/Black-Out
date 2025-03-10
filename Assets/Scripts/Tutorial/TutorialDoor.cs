using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor : MonoBehaviour
{
    public GameObject swtich;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && swtich.GetComponent<TutorialSwitch>().isOn)
        {
            TutorialManager.instance.StartCoroutine("TutorialEnd");
        }
    }
}
