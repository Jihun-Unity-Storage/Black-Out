using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBlink : MonoBehaviour
{
    private GameObject light;
    public float blinktime;
    public float intensityd;
    
    private void Start()
    {
        light = gameObject;
        StartCoroutine("BlinkStart");
    }

    IEnumerator BlinkStart()
    {
        while (true)
        {
            light.GetComponent<Light2D>().intensity = 0f;
            yield return new WaitForSeconds(blinktime);
            light.GetComponent<Light2D>().intensity = intensityd;
            yield return new WaitForSeconds(blinktime);
        }
    }
}
