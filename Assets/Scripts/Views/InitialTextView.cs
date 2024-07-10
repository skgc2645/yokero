using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitialTextView : MonoBehaviour
{
    //property
    TextMeshProUGUI _text;

    public void Initialize()
    {
        _text = GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }
}
