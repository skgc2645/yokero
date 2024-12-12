using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimerView : MonoBehaviour
{
    //member
    TextMeshProUGUI _text;


    public void Initialize(int time)
    {
        gameObject.SetActive(true);
        _text = GetComponentInChildren<TextMeshProUGUI>();
        string minutes = (time / 60).ToString();
        string seconds = (time % 60).ToString();
        _text.text = minutes + ":" + seconds;
    }


    public void SetText(int value)
    {
        string minutes = (value / 60).ToString();
        string seconds = (value % 60).ToString();
        _text.text = minutes + ":" + seconds;
    }


}
