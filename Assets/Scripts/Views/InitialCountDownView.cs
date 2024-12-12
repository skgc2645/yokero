using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class InitialCountDownView : MonoBehaviour
{
    [SerializeField] GameObject _textObject;

    //property

    //member
    TextMeshProUGUI _text;

    //定数
    Vector3 INIT_TEXT_SCALE = new Vector3(4f, 4f, 4f);
    Vector3 END_TEXT_SCALE  = new Vector3(2f, 2f, 2f);



    public void Initialize()
    {
        gameObject.SetActive(false);
        _text = _textObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string value)
    {
        gameObject.SetActive(true);
        //カウントダウン
        if (value != "0")
        {
            _textObject.transform.localScale = INIT_TEXT_SCALE;
            _text.text = value;
            _textObject.transform.DOScale(END_TEXT_SCALE, 0.5f);
        }
        //Go
        else
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_textObject.transform.DOScale(INIT_TEXT_SCALE,1f));
            sequence.Join(_text.DOFade(endValue: 0f, duration: 1f));
            _textObject.transform.localScale = END_TEXT_SCALE;
            _text.text = "Go";
            sequence.OnComplete(() =>
            {
                _textObject.gameObject.SetActive(false);
            });
        }
    }

}
