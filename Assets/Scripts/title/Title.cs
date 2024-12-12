using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] Button _btn;


    void Onclick()
    {
        SoundManager.instance.SoundPlay(Sound.click);
        Debug.Log("hoge");
        SceneManager.LoadScene("game");
    }

    private void Start()
    {
        SoundManager.instance.Initialize();
        _btn.onClick.AddListener(() => Onclick());
    }
}
