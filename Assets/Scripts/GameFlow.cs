using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameFlow : SingletonMonoBehaviour<GameFlow>
{
    [SerializeField] PlayerCon _player;
    [SerializeField] UIPresenter _UIPresenter;
    [SerializeField] LifeUIView _lifeUIView;
    [SerializeField] InputHundler _inputHundler;
    [SerializeField] GameOverView _gameOverView;
    [SerializeField] InitialCountDownView _initialCountDownView;
    [SerializeField] GameTimerView _gameTimerView;
    [SerializeField] ClearView       _clearView;
    [SerializeField] InitialTextView _InitialTextView;
    //[SerializeField] QuitGame _quitGame;

     
    //property
    public Timer InitialCountDownTimer { get { return _initCountDownTimer; } }
    public Timer GameTimer { get { return _gameTimer; } }
    public bool IsGame { get { return _isgame; } set { _isgame = value; } }
    public ReactiveProperty<bool> IsClear { get { return _isClear; } set { _isClear.Value = value.Value; } }

    //member
    bool _isgame = false;
    ReactiveProperty<bool> _isClear ;
    Timer _initCountDownTimer = new Timer(COUNT_TIME);
    Timer _gameTimer = new Timer(GAME_TIME);

    //定数
    static float COUNT_TIME = 3f;         //カウントダウンする秒数
    static float GAME_TIME  = 30f;         //カウントダウンする秒数 sec



    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        Debug.Log("initial");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isgame)
        {
            _isgame = true;
            _isClear.Value = false;
            _InitialTextView.Hide();
            StartGame();
        }


    }

    async void StartGame()
    {
        await  _initCountDownTimer.StartAsyncCountDown();
        _isgame = true;
        CanonManager.instance.StartCanon();
        _gameTimer.StartCountDown();
        
    }



    void Initialize()
    {
        _isgame = false;
        _isClear = new ReactiveProperty<bool> (false);
        _inputHundler.Initialize();
        _player.Initialize();
        _lifeUIView.Initialize();
        _UIPresenter.Initialize();
        _gameOverView.Initialize();
        _initialCountDownView.Initialize();
        _gameTimerView.Initialize((int)GAME_TIME);
        _clearView.Initialize();
        _InitialTextView.Initialize();
        //_quitGame.Initialize();
    }


    void Reset()
    {

    }



}
