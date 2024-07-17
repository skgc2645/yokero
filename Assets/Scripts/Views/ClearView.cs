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
        //hierarchyを変えてしまうとうまく動かない
        _text = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    
    public void Show(string lifeValue, string emoteValue)
    {
        _text.text = lifeValue + "\n" + emoteValue;
        gameObject.SetActive(true);
    }





}
