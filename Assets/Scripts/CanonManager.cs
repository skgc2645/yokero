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
public class CanonManager : MonoBehaviour
{
    [SerializeField] CanonBall _canonBallPrefab;

    CancellationToken _ct;

    int CANONSIDE_LENGTH    = Enum.GetValues(typeof(CanonSide)).Length;
    int VERCANONPOS_LENGTH  = Enum.GetValues(typeof(VerCanonPos)).Length;
    int HORICANONPOS_LENGTH = Enum.GetValues(typeof(HoriCanonPos)).Length;

    // Start is called before the first frame update
    void Start()
    {
         _ct= default;
        CanonController();
    }


    // Update is called once per frame
    void Update()
    {

    }


    async void LaunchCanon(CancellationToken ct)
    {
        CanonBall canonBall = Instantiate(_canonBallPrefab);
        int canonside = UnityEngine.Random.Range(0, CANONSIDE_LENGTH);
        int canonidx  = UnityEngine.Random.Range(0, VERCANONPOS_LENGTH);
        canonBall.SetInitialPos((CanonSide)canonside, canonidx);
        while(true)
        {
            canonBall?.Move((CanonSide)canonside,20f,Time.deltaTime);
            if (!canonBall.IsVisible)
            {
                Destroy(canonBall.gameObject);
                break;
            }
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    async void CanonController()
    {
        while (true) 
        {
            LaunchCanon(_ct);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        }
    }





}
