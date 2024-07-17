using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;


public enum CanonSide
{
    Right,
    Left,
    Top,
    Bottom
}

public enum VerCanonPos
{
    Top,
    Center,
    Bottom
}
public enum HoriCanonPos
{
    Left,
    Center,
    Right
}
public class CanonManager : SingletonMonoBehaviour<CanonManager>
{
    //startDebug
    float time = 0f;
    float defaultSpeed = 5f;
    //endDebug
    [SerializeField] CanonBall _canonBallPrefab;
    [SerializeField] Transform _canonBallParentsTf;

    public float CanonSpeed { get { return _canonSpeed; } }

    int _canonBallCount = 0;
    float _canonSpeed;


    int CANONSIDE_LENGTH    = Enum.GetValues(typeof(CanonSide)).Length;
    int VERCANONPOS_LENGTH  = Enum.GetValues(typeof(VerCanonPos)).Length;
    int HORICANONPOS_LENGTH = Enum.GetValues(typeof(HoriCanonPos)).Length;
    
    
    float MAX_SPEED = 22f;
    float REST_TIME_MIN = 0.5f;
    float REST_TIME_MAX = 0.6f;
    int MAX_CANONBALL_NUM = 7;
    

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if(GameFlow.instance.IsGame)
            time += Time.deltaTime;
    }


    async void LaunchCanon(CancellationToken ct)
    {
        CanonBall canonBall = Instantiate(_canonBallPrefab);
        _canonBallCount++;
        canonBall.SetParent(_canonBallParentsTf);
        int canonside = UnityEngine.Random.Range(0, CANONSIDE_LENGTH);
        int canonidx  = UnityEngine.Random.Range(0, VERCANONPOS_LENGTH);
        canonBall.SetInitialPos((CanonSide)canonside, canonidx);
        if(defaultSpeed + time < MAX_SPEED)
            canonBall.Speed = defaultSpeed + time;
        else 
            canonBall.Speed = MAX_SPEED;
        
        while (true)
        {
            if (canonBall == null) break; 
            canonBall?.Move((CanonSide)canonside,Time.deltaTime);
            if (!canonBall.IsVisible)
            {
                Destroy(canonBall.gameObject);
                _canonBallCount--;
                break;
            }
            if (ct.IsCancellationRequested) return;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    public async void StartCanon(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested && GameFlow.instance) 
        {
            if(_canonBallCount <= MAX_CANONBALL_NUM )
            LaunchCanon(ct);
            float rest = UnityEngine.Random.Range(REST_TIME_MIN, REST_TIME_MAX);
            await UniTask.Delay(TimeSpan.FromSeconds(rest));
        }
    }

    void Reset()
    {
        time = 0;
    }



}
