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
        Debug.Log("hogehoge");
        SceneManager.LoadScene("game");
    }

    private void Start()
    {
        _btn.onClick.AddListener(() => Onclick());
    }
}
