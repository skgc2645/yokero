using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class Timer
{
    public ReactiveProperty<int> CurCountDownTime { get { return _intCountDownTime; } }

    //member
    float _curCountDownTime; //�^�C�}�[
    float _countTime;
    ReactiveProperty<int> _intCountDownTime; //UI�\���p�̐����^�̃^�C��
    UniTaskCompletionSource _awaitCountDown;
    


    public Timer(float countTime)
    {
        _countTime        = countTime;
        _curCountDownTime = _countTime;
        _intCountDownTime = new ReactiveProperty<int>((int)countTime+1);
        _awaitCountDown = new UniTaskCompletionSource();
    }


    void CountDownTimerReset()
    {
        _curCountDownTime = _countTime;
        _intCountDownTime.Value = (int)_curCountDownTime + 1;
    }


    public async UniTask StartAsyncCountDown(CancellationToken ct)
    {
        CountDownTimerReset();
        while (_curCountDownTime >= -1)
        {
            if (_intCountDownTime.Value != (int)Mathf.Ceil(_curCountDownTime))
            {
                _intCountDownTime.Value = (int)Mathf.Ceil(_curCountDownTime);
                Debug.Log("time:" + _intCountDownTime.Value);
            }
            _curCountDownTime -= Time.deltaTime;

            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        _awaitCountDown.TrySetResult();
        return;
    }


    public async void StartCountDown(CancellationToken ct)
    {
        CountDownTimerReset();
        while (!ct.IsCancellationRequested && _curCountDownTime >= -1 && GameFlow.instance && GameFlow.instance.IsGame)
        {
            if (_intCountDownTime.Value != (int)Mathf.Ceil(_curCountDownTime))
            {
                _intCountDownTime.Value = (int)Mathf.Ceil(_curCountDownTime);
                Debug.Log("time:" + _intCountDownTime.Value);
            }
            _curCountDownTime -= Time.deltaTime;
            
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        if(_intCountDownTime.Value == 0)
        {
            Debug.Log("clear");
            GameFlow.instance.IsClear.Value = true;
            GameFlow.instance.IsGame = false;
        }
        return;
    }
}
