using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UIPresenter : MonoBehaviour
{
    //models
    [SerializeField] PlayerCon _player;

    //views
    [SerializeField] LifeUIView _lifeUIView;
    [SerializeField] GameOverView _gameOverView;
    [SerializeField] InitialCountDownView _InitialCountDownView;
    [SerializeField] GameTimerView _gameTimerView;
    [SerializeField] ClearView _clearView;

    public void Initialize()
    {
       _player.HitPoint.SkipLatestValueOnSubscribe().Subscribe(x =>
       {
           if(x<5 && x>=0)
           {
               if (x == 0)
                   _gameOverView.Show();
               _lifeUIView.ChangeHeartSprite(x);

           }
       }).AddTo(this);

        GameFlow.instance.InitialCountDownTimer.CurCountDownTime.SkipLatestValueOnSubscribe().Subscribe(x =>
        {
            _InitialCountDownView.SetText(x.ToString());
        }).AddTo(this);

        GameFlow.instance.GameTimer.CurCountDownTime.SkipLatestValueOnSubscribe().Subscribe(x =>
        {
            _gameTimerView.SetText(x);//こっちはintを入れるのキモイ
        }).AddTo(this);

        GameFlow.instance.IsClear.SkipLatestValueOnSubscribe().Where(x => x).Subscribe(x =>
        {
            //クリアウィンドウを出す
            _clearView.Show(_player.HitPoint.Value.ToString(), _player.EmoteCount.ToString());
        }
        );
    }
}
