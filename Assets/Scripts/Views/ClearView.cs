using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class ClearView : MonoBehaviour
{
    [SerializeField] Button _clearBtn;
    //property
    
    TextMeshProUGUI _text;

    public void Initialize()
    {
        //hierarchy‚ð•Ï‚¦‚Ä‚µ‚Ü‚¤‚Æ‚¤‚Ü‚­“®‚©‚È‚¢
        _text = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
        _clearBtn.onClick.AddListener(Quit);
    }

    
    public void Show(string lifeValue, string emoteValue)
    {
        _text.text = lifeValue + "\n" + emoteValue;
        gameObject.SetActive(true);
    }

    void Quit()
    {
        SoundManager.instance.SoundPlay(Sound.click);
        GameFlow.instance.Quit();
    }




}
