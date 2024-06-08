using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public enum MoveDirection
{
    Right,
    Left,
    Up,
    Down
}

public enum PlayerEmote
{
    A,
    B,
    C
}


public class NewBehaviourScript : MonoBehaviour
{
    //member
    Animator _animator;

    int _curEmoteIdx;

    bool _isMoving = false;


    Vector3 INITIAL_POS = new Vector3(0.5f, 0.65f, -0.2f);
    Vector3 SCALE_LEFT  = new Vector3(0.23f, 0.23f, 0.23f);
    Vector3 SCALE_RIGHT = new Vector3(-0.23f, 0.23f, 0.23f);

    float   OFFSET      = 3f;
    float   LIMIT_WIDTH  = 4f;
    float   LIMIT_HIGHT = 4f;

    int PLAYER_EMOTE_NUM = Enum.GetValues(typeof(PlayerEmote)).Length;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        MoveBtnSettings();
        Initialize();

    }


    // Update is called once per frame
    void Update()
    {
        
    }


    void Initialize()
    {
        transform.position = INITIAL_POS;
    }

    void MoveBtnSettings()
    {
        InputHundler.instance.BtnLeft  .Where(x => x).Subscribe(x => { MoveLeft(); }).AddTo(this);
        InputHundler.instance.BtnRight .Where(x => x).Subscribe(x => { MoveRight(); }).AddTo(this);
        InputHundler.instance.BtnUp    .Where(x => x).Subscribe(x => { MoveUp(); }).AddTo(this);
        InputHundler.instance.BtnDown  .Where(x => x).Subscribe(x => { MoveDown(); }).AddTo(this);
        InputHundler.instance.BtnEmote .Where(x => x).Subscribe(x => { EmoteAnimation(); }).AddTo(this);
    }


    void MoveRight()
    {
        Vector3 nextPos = new Vector3(transform.position.x + OFFSET, transform.position.y, transform.position.z);
        if(Mathf.Abs(nextPos.x) < LIMIT_WIDTH && !_isMoving)
        {
            transform.DOMove(nextPos, 0.1f).SetEase(Ease.OutQuad).OnStart(() =>
            {
                _isMoving = true;
                MoveAnimation(MoveDirection.Right);
            }
            ).OnComplete(() =>
            {
                _isMoving = false;
                _animator.Play("PlayerIdle");
            });
        }
    }    
    
    void MoveLeft()
    {
        Vector3 nextPos = new Vector3(transform.position.x - OFFSET, transform.position.y, transform.position.z);
        if (Mathf.Abs(nextPos.x) < LIMIT_WIDTH && !_isMoving)
        {
            transform.DOMove(nextPos, 0.1f).SetEase(Ease.OutQuad).OnStart(() =>
            {
                _isMoving = true;
                MoveAnimation(MoveDirection.Left);
            }
            ).OnComplete(() =>
            {
                _isMoving = false;
                _animator.Play("PlayerIdle");
            });
        }
    }


    void MoveUp()
    {
        Vector3 nextPos = new Vector3(transform.position.x, transform.position.y + OFFSET, transform.position.z);
        if (Mathf.Abs(nextPos.y) < LIMIT_HIGHT && !_isMoving)
        {
            transform.DOMove(nextPos, 0.1f).SetEase(Ease.OutQuad).OnStart(() =>
            {
                _isMoving = true;
                MoveAnimation(MoveDirection.Up);
            }
            ).OnComplete(() =>
            {
                _isMoving = false;
                _animator.Play("PlayerIdle");
            });
        }

    }

    void MoveDown()
    {
        Vector3 nextPos = new Vector3(transform.position.x, transform.position.y - OFFSET, transform.position.z);
        if (Mathf.Abs(nextPos.y) < LIMIT_HIGHT && !_isMoving)
        {
            transform.DOMove(nextPos, 0.1f).SetEase(Ease.OutQuad).OnStart(() =>
            {
                _isMoving = true;
                MoveAnimation(MoveDirection.Down);
            }
            ).OnComplete(() =>
            {
                _isMoving = false;
                _animator.Play("PlayerIdle");
            });
        }
    }


    void MoveAnimation(MoveDirection md)
    {
        _animator.Play("PlayerMove");
        switch (md)
        {
            case MoveDirection.Right:
                transform.localScale = SCALE_RIGHT;
                return;
            case MoveDirection.Left:
                transform.localScale = SCALE_LEFT;
                return;
            case MoveDirection.Up:
                transform.localScale = SCALE_LEFT;
                return;
            case MoveDirection.Down:
                transform.localScale = SCALE_RIGHT;
                return;
        }
    }


    void EmoteAnimation()
    {
        UpdateEmoteIdx();
        switch (_curEmoteIdx) 
        {
            case (int)PlayerEmote.A:
                _animator.Play("PlayerEmoteA");
                return;
            case (int)PlayerEmote.B:
                _animator.Play("PlayerEmoteB");
                return;
            case (int)PlayerEmote.C:
                _animator.Play("PlayerEmoteC");
                return;
        }
    }

    void UpdateEmoteIdx()
    {
        while (true)
        {
            int n = UnityEngine.Random.Range(0, PLAYER_EMOTE_NUM);
            if (n != _curEmoteIdx)
            {
                _curEmoteIdx = n;
                break;
            }
        }
    }
}
