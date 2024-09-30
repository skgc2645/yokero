using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    bool _pushSpace = false;
    ReactiveProperty<bool> _isClear ;
    Timer _initCountDownTimer;
    Timer _gameTimer;
    CancellationTokenSource _ct;


    //�萔
    static float COUNT_TIME = 3f;         //�J�E���g�_�E������b��
    static float GAME_TIME  = 30f;         //�J�E���g�_�E������b�� sec



    // Start is called before the first frame update
    void Start()
    {
        Initialize();


        Debug.Log("initial");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_pushSpace)
        {
            _pushSpace = true;
            _isClear.Value = false;
            _InitialTextView.Hide();
            StartGame();
        }


    }

    async void StartGame()
    {
        await  _initCountDownTimer.StartAsyncCountDown(_ct.Token);
        _isgame = true;
        CanonManager.instance.StartCanon(_ct.Token);
        _gameTimer.StartCountDown(_ct.Token);
        
    }



    void Initialize()
    {
        _isgame = false;
        _pushSpace = false;
        _ct = new CancellationTokenSource();
        _isClear = new ReactiveProperty<bool> (false);
        _initCountDownTimer = new Timer(COUNT_TIME);
        _gameTimer = new Timer(GAME_TIME);
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

    public void Quit()
    {
        _pushSpace = false;
        _ct.Cancel();
        Reset();
        SceneManager.LoadScene("Title");

    }



}
