using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    [SerializeField] Button _gameOverBtn;
    [SerializeField] Button _clearBtn;


    public void Onclick()
    {
        SoundManager.instance.SoundPlay(Sound.click);
        SceneManager.LoadScene("Title");
    }

    //public void Initialize()
   // {
     //   _gameOverBtn.onClick.AddListener(() => Onclick());
    //    _clearBtn.onClick.AddListener(() => Onclick());
    ///}
}
