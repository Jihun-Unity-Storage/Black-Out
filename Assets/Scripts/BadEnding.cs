using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEnding : MonoBehaviour
{
    public GameObject cm;
    public GameObject badEndingPos;
    private void OnEnable()
    {
        cm.transform.position = badEndingPos.transform.position;
        cm.GetComponent<Animator>().SetTrigger("Bad");
    }
}
