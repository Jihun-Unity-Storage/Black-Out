using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchTick : MonoBehaviour
{
    public bool isPermanent;
    public GameObject target;
    // 빛 오브젝트
    public GameObject warningObject;
    public GameObject targetLight;
    public Sprite onSprite, offSprite;
    
    public float ligthOnTime = 2f;
    public bool isTurnOn;

    private void Start()
    {
        if (isPermanent)
        {
            
        }
        else
        {

            StartCoroutine(StartBlink());
        }
    }

    private void Update()
    {
        if (isPermanent)
        {
            if (isTurnOn) gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
            else if (!isTurnOn) gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
        }
        else
        {

            if (isTurnOn) gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
            else if (!isTurnOn) gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
        }
        
            
        
    }

    IEnumerator StartBlink()
    {
        while (true)
        {
            for (int i = 0; i < 2; i++)
            {
                warningObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                warningObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
            isTurnOn = true;
            warningObject.SetActive(false);
            target.SetActive(true);
            yield return new WaitForSeconds(ligthOnTime);
            // 불 꺼짐 시간
            for (int i = 0; i < 2; i++)
            {
                target.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                target.SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
            target.SetActive(false);
            targetLight.SetActive(false);
            isTurnOn = false;
            yield return new WaitForSeconds(ligthOnTime);
        }
    }

    public void off()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
    }
    public void on()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
    }
}
