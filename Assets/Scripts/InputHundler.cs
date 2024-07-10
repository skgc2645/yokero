using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
public class InputHundler : SingletonMonoBehaviour<InputHundler>
{
    [SerializeField] KeyCode _leftBtn;
    [SerializeField] KeyCode _rightBtn;
    [SerializeField] KeyCode _upBtn;
    [SerializeField] KeyCode _downBtn;
    [SerializeField] KeyCode _EmoteBtn;


    public readonly ReactiveProperty<bool> BtnLeft  = new ReactiveProperty<bool>();
    public readonly ReactiveProperty<bool> BtnRight = new ReactiveProperty<bool>();
    public readonly ReactiveProperty<bool> BtnUp    = new ReactiveProperty<bool>();
    public readonly ReactiveProperty<bool> BtnDown  = new ReactiveProperty<bool>();
    public readonly ReactiveProperty<bool> BtnEmote  = new ReactiveProperty<bool>();
    
    
    public void Initialize()
    {
        BtnLeft.AddTo(this);
        BtnRight.AddTo(this);
        BtnUp.AddTo(this);
        BtnDown.AddTo(this);

        //BtnLeft.Where(x => x).Subscribe(x => { GameManager.instance.OnTrigger(); }).AddTo(this);
        //BtnRight.Where(x => x).Subscribe(x => { GameManager.instance.OnCtrlBtnA(); }).AddTo(this);
        //BtnUp.Where(x => x).Subscribe(x => { GameManager.instance.OnStartBtn(); }).AddTo(this);
        //BtnDown.Subscribe(x => { GameManager.instance.OnStickTilted(x); }).AddTo(this);
    }

    void Update()
    {
        BtnLeft.Value  = Input.GetKeyDown(_leftBtn);
        BtnRight.Value = Input.GetKeyDown(_rightBtn);
        BtnUp.Value    = Input.GetKeyDown(_upBtn);
        BtnDown.Value  = Input.GetKeyDown(_downBtn);
        BtnEmote.Value  = Input.GetKeyDown(_EmoteBtn);
    }
}
