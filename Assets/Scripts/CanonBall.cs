using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanonBall : MonoBehaviour
{
    
    public bool IsVisible  { get{ return _isVisible; } }
    bool _isVisible = false;

    Vector3[] RIGHTCANON_INITPOSES   = { new Vector3(10.5f, 3.65f, -0.2f),  new Vector3(10.5f, 0.65f, -0.2f),  new Vector3(10.5f, -2.35f, -0.2f) };
    Vector3[] LEFTCANON_INITPOSES    = { new Vector3(-9.5f, 3.65f, -0.2f),  new Vector3(-9.5f, 0.65f, -0.2f),  new Vector3(-9.5f, -2.35f, -0.2f) };
    Vector3[] TOPCANON_INITPOSES     = { new Vector3(-2.5f, 8.65f, -0.2f),  new Vector3(0.5f, 8.65f, -0.2f),   new Vector3(3.5f, 8.65f, -0.2f) };
    Vector3[] BOTTOMCANON_INITPOSES  = { new Vector3(-2.5f, -7.35f, -0.2f), new Vector3(0.5f , -7.35f, -0.2f), new Vector3(3.5f, -7.35f, -0.2f) };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetInitialPos(CanonSide side, int idx)
    {
        switch (side) 
        { 
            case CanonSide.Right:
                transform.position = RIGHTCANON_INITPOSES[idx];
                return;
            case CanonSide.Left:
                transform.position = LEFTCANON_INITPOSES[idx];
                return;
            case CanonSide.Top:
                transform.position = TOPCANON_INITPOSES[idx];
                return;
            case CanonSide.Bottom:
                transform.position = BOTTOMCANON_INITPOSES[idx];
                return;
        }
    }


    public void Move(CanonSide side, float speed, float time)
    {
        switch(side) 
        {
            case CanonSide.Left:
                MoveRight(speed, time);
                return;
            case CanonSide.Right:
                MoveLeft(speed, time);
                return;
            case CanonSide.Top:
                MoveDown(speed, time); 
                return;
            case CanonSide.Bottom:
                MoveUp(speed, time);
                return;
        }
    }
    public void MoveRight(float speed, float time)
    {
        transform.position = new Vector3(transform.position.x + time * speed, transform.position.y, transform.position.z);
        CheckIsVisible();
    }


    public void MoveLeft(float speed, float time)
    {
        transform.position = new Vector3(transform.position.x - time * speed, transform.position.y, transform.position.z);
        CheckIsVisible();
    }


    public void MoveUp(float speed, float time)
    {
        transform.position = new Vector3(transform.position.x , transform.position.y + time * speed, transform.position.z);
        CheckIsVisible();
    }


    public void MoveDown(float speed, float time)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - time * speed, transform.position.z);
        CheckIsVisible();
    }


    void CheckIsVisible()
    {
        if (transform.position.x > RIGHTCANON_INITPOSES[0].x
        || transform.position.x < LEFTCANON_INITPOSES[0].x
        || transform.position.y > TOPCANON_INITPOSES[0].y
        || transform.position.y < BOTTOMCANON_INITPOSES[0].y)
            _isVisible = false;
        else
            _isVisible = true;
    }
}
