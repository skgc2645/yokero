using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] Button _quitBtn;


    Image img;
    //íËêî
    const float PANEL_ALPHA = 160f;
    public void Initialize()
    {
        img = GetComponent<Image>();
        Hide();
        //_quitBtn.onClick.AddListener(/*titleëJà⁄ÉÅÉ\ÉbÉhMVPÇ»ÇÁPÇ…Ç©Ç≠*/)
    }



    public void Show()
    {
        gameObject.SetActive(true);
        Sequence seaquence = DOTween.Sequence();
        seaquence.Append(img.DOFade(endValue: PANEL_ALPHA / 256f, duration: 1.5f));
    }


    public void Hide()
    {
        gameObject.SetActive(false);
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
    }
}
