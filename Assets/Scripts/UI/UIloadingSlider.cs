using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIloadingSlider : MonoBehaviour
{
    public UIManager uiManager;
    public string tips;

    public Text txtTip;
    public Slider loadingSlider;

    public float minTime = 0.5f, maxTime = 0.9f;

    void OnEnable()
    {
        tips = uiManager.strTips[Random.RandomRange(0, uiManager.strTips.Length)];
        txtTip.text = tips;
        loadingSlider.value = 0;
        StartCoroutine("Loading");
    }

    private IEnumerator Loading()
    {
        while (loadingSlider.value != 1)
        {
            //Debug.Log(loadingSlider.value);
            loadingSlider.value += Random.RandomRange(0.01f, 0.05f);
            yield return new WaitForSeconds(Random.RandomRange(minTime, maxTime));
        } gameObject.SetActive(false);
    }
}
