using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverView : MonoBehaviour
{
    [SerializeField] Button _quitBtn;


    Image img;
    //�萔
    const float PANEL_ALPHA = 160f;
    Vector3 BTN_POS = new Vector3(0f,-103.06f,-7.66f);
    Vector3 BTN_SCALE = new Vector3(1.63f, 1.63f, 1.63f);

    public void Initialize()
    {
        img = GetComponent<Image>();
        Hide();
        _quitBtn.onClick.AddListener(Quit);
    }



    public void Show()
    {
        gameObject.SetActive(true);
        Sequence seaquence = DOTween.Sequence();
        seaquence.Append(img.DOFade(endValue: PANEL_ALPHA / 256f, duration: 1.5f));
        seaquence.Play().OnComplete(() => { _quitBtn.gameObject.SetActive(true); });

    }


    public void Hide()
    {
        gameObject.SetActive(false);
        _quitBtn.gameObject.SetActive(false);
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
    }


    void Quit()
    {
        SoundManager.instance.SoundPlay(Sound.click);
        GameFlow.instance.Quit();
    }

}
