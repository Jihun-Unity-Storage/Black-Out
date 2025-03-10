using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScnee : MonoBehaviour
{
    public static EndingScnee instance;
    public GameObject first, second;
    public GameObject adum, leval;
    public GameObject badendingMap, badtext, hiddentext, hiddenGOld;

    public GameObject textingBoxDomang;

    private void Awake()
    {
        instance = this;
        first.SetActive(false);
        second.SetActive(false);
        badendingMap.SetActive(false);
        adum.SetActive(true);
    }
    public void EndingSelectStart()
    {
        first.SetActive(true);
    }
    public void Bad_Ending()
    {
        first.SetActive(false);
        BadEndingShowStart();
    }
    
    public void Doduk_Ending()
    {
        second.SetActive(false);
        hiddentext.SetActive(true);
    }
    
    public void Doduk_Ending2()
    {
        hiddenGOld.SetActive(false);
        adum.GetComponent<Animator>().SetTrigger("HiddenEnding");
    }
    
    public void Domang_Ending()
    {
        textingBoxDomang.SetActive(true);
        second.SetActive(false);
    }
    
    public void Domang_Ending2()
    {
        adum.GetComponent<Animator>().SetTrigger("Domang_Ending");
    }

    public void NextEnding()
    {
        first.SetActive(false);
        second.SetActive(true);
    }

    public void BadEndingShowStart()
    {
        leval.GetComponent<SpriteRenderer>().flipX = true;
        adum.GetComponent<Animator>().SetTrigger("BadEnding");
    }

    public void BadENdingShow2()
    {
        badendingMap.SetActive(true);
        Invoke("Bad3", 2.5f);
    }

    public void Bad3()
    {
        badtext.SetActive(true);
    }
}
