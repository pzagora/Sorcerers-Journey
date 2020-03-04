﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            valueText.text = Math.Round(value, 1) + "  / " + MaxValue;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
