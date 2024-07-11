using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    [SerializeField] Button _gameOverBtn;
    [SerializeField] Button _clearBtn;


    void Onclick()
    {
        Debug.Log("hogehoge");
        SceneManager.LoadScene("Title");
    }

    public void Initialize()
    {
        _gameOverBtn.onClick.AddListener(() => Onclick());
        _clearBtn.onClick.AddListener(() => Onclick());
    }
}
