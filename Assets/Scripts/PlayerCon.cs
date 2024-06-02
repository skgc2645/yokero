using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //member

    Vector3 INITIAL_POS = new Vector3(0.5f, 0.65f, -0.1f);
    float   OFFSET      = 3f;
    float   LIMIT_WIDTH  = 4f;
    float   LIMIT_HIGHT = 4f;


    // Start is called before the first frame update
    void Start()
    {
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
        InputHundler.instance.BtnLeft .Where(x => x).Subscribe(x => { MoveLeft(); }).AddTo(this);
        InputHundler.instance.BtnRight.Where(x => x).Subscribe(x => { MoveRight(); }).AddTo(this);
        InputHundler.instance.BtnUp   .Where(x => x).Subscribe(x => { MoveUp(); }).AddTo(this);
        InputHundler.instance.BtnDown .Where(x => x).Subscribe(x => { MoveDown(); }).AddTo(this);
    }


    void MoveRight()
    {
        Vector3 nextPos = new Vector3(transform.position.x + OFFSET, transform.position.y, transform.position.z);
        if(Mathf.Abs(nextPos.x) < LIMIT_WIDTH)
        transform.position = nextPos;
    }    
    
    void MoveLeft()
    {
        Vector3 nextPos = new Vector3(transform.position.x - OFFSET, transform.position.y, transform.position.z);
        if (Mathf.Abs(nextPos.x) < LIMIT_WIDTH)
            transform.position = nextPos;
    }

    void MoveUp()
    {
        Vector3 nextPos = new Vector3(transform.position.x, transform.position.y + OFFSET, transform.position.z);
        if (Mathf.Abs(nextPos.y) < LIMIT_HIGHT)
            transform.position = nextPos;
    }

    void MoveDown()
    {
        Vector3 nextPos = new Vector3(transform.position.x, transform.position.y - OFFSET, transform.position.z);
        if (Mathf.Abs(nextPos.y) < LIMIT_HIGHT)
            transform.position = nextPos;
    }

}
