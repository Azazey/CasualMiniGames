using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _sceneNumber;
    
    private void Start()
    {
        _button.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneChanger.SChanger.LoadScene(_sceneNumber);
    }
}
