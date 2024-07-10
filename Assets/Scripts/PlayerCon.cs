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


public class PlayerCon : MonoBehaviour
{
    //property
    public IReadOnlyReactiveProperty<int> HitPoint { get { return _hitPoint; } }
    public int EmoteCount {get{return _emoteCount; }}
    //member
    Animator _animator;
    int _curEmoteIdx;
    bool _isMoving = false;
    int _emoteCount = 0;
    readonly ReactiveProperty<int> _hitPoint = new ReactiveProperty<int>(PLAYER_LIFE_MAX);
    //íËêî
    const float   OFFSET      = 3f;
    const float LIMIT_WIDTH  = 4f;
    const float   LIMIT_HIGHT = 4f;
    int PLAYER_EMOTE_NUM = Enum.GetValues(typeof(PlayerEmote)).Length;
    static int PLAYER_LIFE_MAX = 5;
    Vector3 INITIAL_POS = new Vector3(0.5f, 0.65f, -0.2f);
    Vector3 SCALE_LEFT = new Vector3(0.23f, 0.23f, 0.23f);
    Vector3 SCALE_RIGHT = new Vector3(-0.23f, 0.23f, 0.23f);




    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
    }


    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        MoveBtnSettings();
        transform.position = INITIAL_POS;
        _emoteCount = 0;
        _isMoving = false;
    }

    void MoveBtnSettings()
    {
        InputHundler.instance.BtnLeft  .Where(x => x).Subscribe(x => { Move(MoveDirection.Left); }).AddTo(this);
        InputHundler.instance.BtnRight .Where(x => x).Subscribe(x => { Move(MoveDirection.Right); }).AddTo(this);
        InputHundler.instance.BtnUp    .Where(x => x).Subscribe(x => { Move(MoveDirection.Up); }).AddTo(this);
        InputHundler.instance.BtnDown  .Where(x => x).Subscribe(x => { Move(MoveDirection.Down); }).AddTo(this);
        InputHundler.instance.BtnEmote .Where(x => x).Subscribe(x => { EmoteAnimation(); _emoteCount++; }).AddTo(this);
    }

    void Move(MoveDirection dir)
    {
        if (!GameFlow.instance.IsGame) return;
        switch (dir) 
        { 
            case MoveDirection.Left:
                MoveLeft(); break;
            case MoveDirection.Right:
                MoveRight(); break;
            case MoveDirection.Up:
                MoveUp(); break;
            case MoveDirection.Down:
                MoveDown(); break;
            default:
                Debug.LogError("input direction error");
                break;
        }
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Canon" && GameFlow.instance.IsGame)
        {
            _hitPoint.Value--;
            if(_hitPoint.Value == 0) GameFlow.instance.IsGame = false;
        }
    }
}
