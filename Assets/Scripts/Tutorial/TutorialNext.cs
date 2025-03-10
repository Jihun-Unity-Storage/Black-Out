using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNext : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            TutorialManager.instance.StartCoroutine("IntoTop");
        }
    }
}
