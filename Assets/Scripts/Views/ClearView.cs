using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearView : MonoBehaviour
{
    //property
    TextMeshProUGUI _text;

    public void Initialize()
    {
        //hierarchy‚ð•Ï‚¦‚Ä‚µ‚Ü‚¤‚Æ‚¤‚Ü‚­“®‚©‚È‚¢
        _text = transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    
    public void Show(string lifeValue, string emoteValue)
    {
        _text.text = lifeValue + "\n" + emoteValue;
        gameObject.SetActive(true);
    }





}
